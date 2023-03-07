﻿using Fusion;
using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public abstract class Unit : NetworkBehaviour, ITakeDamage
{
    [SerializeField] protected UnitInfo _info;

    public NetworkHealth Health 
    {   
        get
        {
            if (_health != null)
                return _health;
            else
                _health = GetComponent<NetworkHealth>();
            return _health;
        }
    }
    private NetworkHealth _health;

    public bool IsDead => States.CurrentState is DeadState ? true : false;
    public StateMachine States { get; private set; }
    public virtual UnitInfo Info => _info;

    protected virtual void Awake()
    {
        States = GetComponent<StateMachine>();
        States.Initialize(this);
    }

    public virtual void TakeDamage(Unit from, int damage) { }
}
