using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Enemy : Unit
{
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
}