using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HealLoot : LootItem
{
    [Rpc]
    public override void RPC_PickUp(Player player)
    {
        player.Health.CurrentHealth += (int)Value;
    }
}