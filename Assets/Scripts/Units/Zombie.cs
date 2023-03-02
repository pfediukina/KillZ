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

    private BaseFactory<Zombie> _ownedFactory;

    protected override void Awake()
    {
        base.Awake();
        States.AddState(new EnemyMoveState(this));
    }
    private void FixedUpdate()
    {
        FollowNearestPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            var bullet = collision.GetComponent<Bullet>();
            _health.CurrentHealth--;
            Runner.Despawn(bullet.Object);
        }
    }

    public void FollowNearestPlayer()
    {
        if (States.CurrentState is DeadState) return;
        if (Launcher.Chars.Count == 0) return;

        var state = States.GetState<EnemyMoveState>();
        if (state == null) return;

        Transform nearest = Launcher.Chars[0];
        foreach (var transf in Launcher.Chars)
        {
            if (Vector3.Distance(nearest.position, transform.position) > Vector3.Distance(transf.position, transform.position))
            {
                nearest = transf;
            }
        }
        state.Follow = nearest;
        if(States.CurrentState is not EnemyMoveState)
            States.SetState<EnemyMoveState>();
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
        _health.ResetHealth();
        _ownedFactory = (BaseFactory<Zombie>)factory;
        StartCoroutine(SpawnSprite());
        
    }
    public IEnumerator SpawnSprite()
    {
        yield return new WaitForSeconds(0.2f);
        _sprite.SetActive(true);
    }
}