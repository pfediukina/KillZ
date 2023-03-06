using Fusion;

public class AmmoLoot : LootItem
{
    [Rpc]
    public override void RPC_PickUp(Player player)
    {
        player.CurrentWeapon.Ammo += (int)Value;
    }
}