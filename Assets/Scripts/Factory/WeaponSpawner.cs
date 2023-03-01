using Fusion;
using UnityEngine;

public class WeaponSpawner : NetworkBehaviour
{
    [SerializeField] private BaseWeapon[] _weapons;

    public BaseWeapon GivePlayerWeapon(Transform weaponPlace)
    {
        int rand = Random.Range(0, _weapons.Length);
        BaseWeapon weapon = Runner.Spawn(_weapons[rand], weaponPlace.position);
        weapon.transform.parent = weaponPlace;
        return weapon;
    }
}