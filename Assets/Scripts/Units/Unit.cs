using Fusion;
using System;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public abstract class Unit : NetworkBehaviour
{
    [SerializeField] protected UnitInfo _info;
    public virtual float MoveSpeed => _info.StartSpeed;
    public virtual StateMachine States { get; protected set; }
}
