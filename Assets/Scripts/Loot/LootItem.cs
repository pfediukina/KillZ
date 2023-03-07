using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform))]
public class LootItem : NetworkBehaviour, ILootable
{
    public LootType Bonus => _bonus;
    public float Value => _value;

    [SerializeField] private LootType _bonus;
    [SerializeField] private float _value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RPC_PickUp(collision.gameObject.GetComponent<Player>());
            Runner.Despawn(Object);
        }
    }

    [Rpc]
    public virtual void RPC_PickUp(Player player) { }

    public void PickUp(Player player)
    {
        //throw new System.NotImplementedException();
    }
}

public enum LootType
{
    Ammo,
    Heal,
    Bomb,
}
