using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ZombieAttack : NetworkBehaviour
{
    [SerializeField] protected Zombie _zombie;
    protected bool _canAttack = true;
    protected float _attackDelay = 1;

    private void OnEnable()
    {
        _canAttack = true;
    }

    protected virtual void Update()
    {
        if (_zombie.States.CurrentState is DeadState && _canAttack) _canAttack = false;
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out var player) && _canAttack)
        {
            player.Health.CurrentHealth--;
            StartCoroutine(AttackResetTimer());
        }
    }

    protected IEnumerator AttackResetTimer()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackDelay);
        _canAttack = true;
    }
}