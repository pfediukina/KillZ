using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class RPCList
{
    [Rpc]   
    public static void RPC_UnitMove(Unit unit, Vector3 dir)
    {
        unit.transform.Translate(dir * unit.Info.StartSpeed * unit.Runner.DeltaTime);
    }

    [Rpc]
    public static void RPC_ChangeAnimationID(Animator animator, int id)
    {
        animator.CrossFade(id, 0);
    }

    [Rpc]
    public static void RPC_FlipSprite(SpriteRenderer sprite, bool flip)
    {
        sprite.flipX = flip;
    }

    [Rpc]
    public static void RPC_RotateWeapon(SpriteRenderer sprite, float rot)
    {
        sprite.transform.eulerAngles = Vector3.forward * rot;
        if (sprite.flipX) sprite.flipY = true;
        else sprite.flipY = false;
    }

    [Rpc]
    public static void RPC_Fire(Unit owner, Vector3 dir, Bullet prefab, float speed)
    {
        if (owner.HasStateAuthority)
        {
            var shot = owner.Runner.Spawn(prefab, owner.transform.position);
            shot.MoveTo(dir);
            shot.Speed = speed;
            shot.Owner = owner;
        }
    }
}
