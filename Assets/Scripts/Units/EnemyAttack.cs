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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(_enemy.IsDead || !_enemy.gameObject.activeSelf) return;
        if (collision.tag == "Player")
        {
            _enemy.Weapon.Shoot(collision.transform);
        }
    }
}