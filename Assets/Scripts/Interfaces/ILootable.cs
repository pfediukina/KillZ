using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ILootable
{
    public LootType Bonus { get; }
    public float Value { get; }

    public void PickUp(Player player);
}
