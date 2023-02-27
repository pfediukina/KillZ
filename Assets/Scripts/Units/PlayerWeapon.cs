using Fusion;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] private BaseWeapon[] weapon;
    public BaseWeapon CurrentWeapon { get;  set; }

    [Networked] private int _weaponType { get; set; }

    private void Start()
    {
        if (HasInputAuthority)
        {
            int rand = Random.Range(0, weapon.Length);
            _weaponType = rand;
            RPC_CreateWeapon(_weaponType);
        }
        else
            CreateWeapon(_weaponType);
    }

    [Rpc]
    public void RPC_CreateWeapon(int ID)
    {
        CreateWeapon(ID);
    }

    private void CreateWeapon(int ID)
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
        CurrentWeapon = Instantiate(weapon[ID], transform);
        CurrentWeapon.transform.position = transform.position;
    }
}