using Fusion;
using UnityEngine;

public class WeaponSpawner : NetworkBehaviour
{
    [SerializeField] private BaseWeapon[] _weapons;

    public void GivePlayerWeapon(Player player)
    {
        int rand = Random.Range(0, _weapons.Length);
        BaseWeapon weapon = Runner.Spawn(_weapons[rand]);
        weapon.ID = player.Object.Id; 
    }
}