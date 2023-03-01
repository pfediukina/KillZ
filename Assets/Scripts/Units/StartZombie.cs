using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class StartZombie : Unit, ISpawnObject
{
    [SerializeField] private Animator _anim;

    protected override void Awake()
    {
        base.Awake();
        States.AddState(new EnemyMoveState(this));
    }
    private void FixedUpdate()
    {
        FollowNearestPlayer();
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

    public void DespawnObject(Action OnDespawned)
    {
        States.GetState<DeadState>().DeathPos = transform.position;
        States.SetState<DeadState>();
        RPC_DeathAnimation("Dead");
        StartCoroutine(FactoryDespawn(OnDespawned));
    }

    private IEnumerator FactoryDespawn(Action OnDespawned)
    {
        yield return new WaitForSeconds(5);
        OnDespawned?.Invoke();
    }

    [Rpc]
    private void RPC_DeathAnimation(string name)
    {
        _anim.CrossFade(name, 0);
    }

    public void SpawnObject()
    {
        RPC_DeathAnimation("Move");
    }
}