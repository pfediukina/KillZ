using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    protected override void Awake()
    {
        base.Awake();
        States.AddState(new EnemyMoveState(this));
    }

    public void FollowTarget(Transform target)
    {
        if (target == null) return;
        var state = States.GetState<EnemyMoveState>();
        if(state == null) return;
        state.Follow = target;
        States.SetState<EnemyMoveState>();
    }
}