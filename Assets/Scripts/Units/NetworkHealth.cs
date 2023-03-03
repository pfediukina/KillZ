using Fusion;
using System;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class NetworkHealth : NetworkBehaviour
{
    [SerializeField] private int _maxHealth;
    private Unit _owner;

    [Networked(OnChanged = nameof(HealthChanged))] public int CurrentHealth { get; set; }
    public Action<int, int> OnHealthChanged;

    public void Start()
    {
        _owner = GetComponent<Unit>();
        _maxHealth = _owner.Info.MaxHealth;
        CurrentHealth = _maxHealth;
    }

    public void ResetHealth()
    {
        CurrentHealth = _maxHealth;
    }

    public static void HealthChanged(Changed<NetworkHealth> changed)
    {
        if(changed.Behaviour != null)
        {
            changed.Behaviour.CurrentHealth = Mathf.Clamp(changed.Behaviour.CurrentHealth, 0, changed.Behaviour._maxHealth);
            if(changed.Behaviour.CurrentHealth == 0 && changed.Behaviour._owner.States.CurrentState is not DeadState)
            {
                changed.Behaviour._owner.States.SetState<DeadState>();
            }
            if(changed.Behaviour.HasInputAuthority)
            {
                changed.Behaviour.OnHealthChanged?.Invoke(changed.Behaviour.CurrentHealth, changed.Behaviour._maxHealth);
            }
        }
    }
}
