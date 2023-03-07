using Fusion;
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

    public Transform WeaponPlace;

    public BaseWeapon Weapon
    {
        get
        {
            if (_weapon != null)
                return _weapon;
            else
            {
                _weapon = WeaponPlace.GetComponentInChildren<BaseWeapon>();
            }
            return _weapon;
        }
        set
        {
            _weapon = value;
            _weapon.transform.parent = WeaponPlace;
        }
    }
    private BaseWeapon _weapon;

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
