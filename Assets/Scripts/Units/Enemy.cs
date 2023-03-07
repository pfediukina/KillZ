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
        tag = "Enemy";
        States.AddState(new EnemyMoveState(this));
        States.AddState(new HitState(this));
    }

    public override void TakeDamage(Unit from, int damage)
    {
        if (IsDead) return;
        States.SetState<HitState>();
        Health.CurrentHealth -= damage;
        if (from is Player && Health.CurrentHealth <= 0)
            (from as Player).Score += Info.Score;
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