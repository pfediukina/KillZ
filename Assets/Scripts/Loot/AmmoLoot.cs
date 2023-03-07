using Fusion;
using UnityEngine;

public class AmmoLoot : LootItem
{
    [Rpc]
    public override void RPC_PickUp(Player player)
    {
        if (player.Weapon == null) return;
        player.Weapon.Ammo += (int)Value;
    }
}