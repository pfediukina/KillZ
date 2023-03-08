using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class BombLoot : LootItem, IDamaging
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private SpriteRenderer _circleSprite;

    public Unit From { get ; set; }
    public int Damage { get; set; }

    private void Start()
    {
        _circleSprite.transform.localScale = Vector3.one * 1.4f / 3.5f * Value;
        Damage = 10000;
    }
    [Rpc]
    public override void RPC_PickUp(Player player)
    {
        if (!HasStateAuthority) return;
        var colliders = Physics2D.OverlapCircleAll(transform.position, Value, _enemyLayer);
        foreach(var collider in colliders)
        {
            if (collider.TryGetComponent<Enemy>(out var enemy))
            {
                HitEnemy(player, enemy, Damage);
            }
        }
        
    }

    public void HitEnemy(Unit from, Unit to, int damage)
    {
        if(to.IsDead) return;
        to.TakeDamage(from, damage);
    }
}