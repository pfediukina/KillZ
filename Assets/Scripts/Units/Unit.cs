using System;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] private UnitInfo _info;
    public virtual float MoveSpeed => _info.StartSpeed;
    public virtual StateMachine States { get; protected set; }

    public abstract void Move(Vector2 direction);
}
