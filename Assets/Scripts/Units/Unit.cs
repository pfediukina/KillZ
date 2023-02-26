using Fusion;
using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public abstract class Unit : NetworkBehaviour
{
    [SerializeField] protected UnitInfo _info;

    public virtual StateMachine States { get; private set; }
    public virtual UnitInfo Info => _info;

    protected virtual void Awake()
    {
        States = GetComponent<StateMachine>();
        States.Initialize(this);
    }
}
