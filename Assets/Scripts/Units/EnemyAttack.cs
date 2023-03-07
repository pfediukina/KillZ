using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAttack : NetworkBehaviour
{
    [SerializeField]private Enemy _enemy;

    private bool _canAttack = true;
    private float _attackDelay = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if(collision.TryGetComponent<Player>(out var player) && _canAttack)
        //{
        //    if(_enemy.States.CurrentState is not DeadState)
        //        player.Health.CurrentHealth--;

        //    StartCoroutine(AttackResetTimer());
        //}
    }

    private IEnumerator AttackResetTimer()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackDelay);
        _canAttack = true;
    }
}