﻿using UnityEngine;

public class EnemyMoveState : IState
{
    public NetworkInputData Data { get; set; }

    private Unit _unit;
    private Rigidbody2D _rb;
    public Transform Follow;

    public EnemyMoveState(Unit unit) 
    {
        //_animator = unit.GetComponent<NetworkAnimator>();
        _unit = unit;
        _rb = unit.GetComponent<Rigidbody2D>();
    }

    public void Enter() { }

    public void Exit() { }

    public void Update()
    {
        if (Follow != null)
        {
            Vector2 vel = (Follow.position - _unit.transform.position).normalized * _unit.Info.StartSpeed * _unit.Runner.DeltaTime;
            _rb.velocity = vel;
        }
    }
}