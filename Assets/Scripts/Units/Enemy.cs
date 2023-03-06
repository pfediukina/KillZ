using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Enemy : Unit, ISpawnObject
{
    [SerializeField] private Animator _anim;
    [SerializeField] private NetworkRigidbody2D _networkRB;
    [SerializeField] private GameObject _sprite;

    private BaseFactory<Enemy> _ownedFactory;

    protected override void Awake()
    {
        base.Awake();
        States.AddState(new EnemyMoveState(this));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            if (States.CurrentState is DeadState) return;

            var bullet = collision.GetComponent<Bullet>();
            Health.CurrentHealth--;
            Runner.Despawn(bullet.Object);
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

    public IEnumerator FactoryDespawnDelay(float time)
    {
        yield return new WaitForSeconds(time);
        if (_ownedFactory != null)
        {
            _sprite.SetActive(false);
            _ownedFactory.DespawnObject(this);
        }
    }

    public void SpawnObject(NetworkBehaviour factory, Vector3 pos)
    {
        _networkRB.TeleportToPosition(pos);
        States.SetState<EnemyMoveState>();
        Health.ResetHealth();
        _ownedFactory = (BaseFactory<Enemy>)factory;
        StartCoroutine(SpawnSprite());
        
    }
    public IEnumerator SpawnSprite()
    {
        yield return new WaitForSeconds(0.2f);
        _sprite.SetActive(true);
    }
}