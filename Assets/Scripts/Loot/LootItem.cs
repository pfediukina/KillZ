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

    
    [SerializeField] private BombCollision _bomb;

    private void Awake()
    {
        if (_bomb != null) _bomb.SetColliderRadius(Value);
    }

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
            case BonusType.Bomb:
                PickUpBomb(player);
                break;
            case BonusType.Ammo:
                PickUpAmmo(player);
                break;
        }
        if(Object != null) Runner.Despawn(Object);
    }

    private void PickUpHeal(Player player)
    {
        player.Health.CurrentHealth += (int)Value;
    }

    private void PickUpAmmo(Player player)
    {
        player.Weapon.Ammo += (int)Value;
    }

    private void PickUpBomb(Player player)
    {
        _bomb.ActivateBomb(player);   
    }
}

public enum BonusType
{
    Heal,
    Bomb,
    Ammo
}
