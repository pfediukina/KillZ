using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class BombLoot : LootItem
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private SpriteRenderer _circleSprite;

    private void Start()
    {
        _circleSprite.transform.localScale = Vector3.one * 1.4f / 3.5f * Value;
    }
    [Rpc]
    public override void RPC_PickUp(Player player)
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, Value, _enemyLayer);
        foreach(var collider in colliders)
        {
            if (collider.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.DespawnObject();
            }
        }
        
    }
}