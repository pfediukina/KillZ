using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SkeletonAttack : ZombieAttack
{
    [SerializeField] private Bullet _prefab;

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out var player) && _canAttack)
        {
            //RPCList.RPC_Fire(_zombie, collision.transform.position, _prefab);
            var shot = Runner.Spawn(_prefab, transform.position);
            shot.Speed = 2f;
            shot.MoveTo(player.transform.position);
            shot.Owner = _zombie;
            StartCoroutine(AttackResetTimer());
        }
    }
}