using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootItem : NetworkBehaviour, ILootable
{
    public BonusType Bonus => _bonus;
    public float Value => _value;

    [SerializeField] private BonusType _bonus;
    [SerializeField] private float _value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PickUp(collision.gameObject.GetComponent<Player>());
        }
    }

    public void PickUp(Player player)
    {
        switch (Bonus)
        {
            case BonusType.Heal:
                PickUpHeal(player);
                break;
            case BonusType.Ammo:
                PickUpAmmo(player);
                break;
        }
        Runner.Despawn(Object);
    }

    private void PickUpHeal(Player player)
    {
        player.Health.CurrentHealth += (int)Value;
    }

    private void PickUpAmmo(Player player)
    {
        player.CurrentWeapon.Ammo += (int)Value;
    }
}

public enum BonusType
{
    Heal,
    //AttackSpeed,
    Ammo
}
