using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Zombie : Unit, ISpawnObject
{
    [SerializeField] private Animator _anim;
    [SerializeField] private NetworkRigidbody2D _networkRB;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private int _score = 1;

    public int Score => _score;
    private BaseFactory<Zombie> _ownedFactory;
    private HashSet<Bullet> _bullets = new HashSet<Bullet>();

    protected override void Awake()
    {
        base.Awake();
        States.AddState(new EnemyMoveState(this));
        States.AddState(new HitState(this));
        tag = "Zombie";
    }

    private void OnEnable()
    {
        _bullets.Clear();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out var bullet))
        {
            if (_bullets.Contains(bullet)) return;
            else if (States.CurrentState is DeadState) return;
            else if(bullet.Owner is Zombie) return;
            else if(Vector3.Distance(collision.transform.position, transform.position) > 0.5f) return;
            _bullets.Add(bullet);

            Health.CurrentHealth--;

            if(Health.CurrentHealth <= 0)
                (bullet.Owner as Player).Score += _score;

            Runner.Despawn(bullet.Object);
            States.SetState<HitState>();
        }
    }

    public void DespawnObject()
    {
        if (States.CurrentState is not DeadState)
        {
            States.SetState<DeadState>();
        }

        if(gameObject.activeSelf)
            StartCoroutine(FactoryDespawnDelay(3));
    }

    public void SpawnObject(NetworkBehaviour factory, Vector3 pos)
    {
        _ownedFactory = (BaseFactory<Zombie>)factory;

        _networkRB.TeleportToPosition(pos);
        Health.ResetHealth();
        States.SetState<EnemyMoveState>();
        StartCoroutine(SpawnSprite());
        
    }

    private IEnumerator FactoryDespawnDelay(float time)
    {
        yield return new WaitForSeconds(time);
        if (_ownedFactory != null)
        {
            _sprite.SetActive(false);
            _ownedFactory.DespawnObject(this);
        }
    }

    private IEnumerator SpawnSprite()
    {
        yield return new WaitForSeconds(0.2f);
        _sprite.SetActive(true);
    }
}