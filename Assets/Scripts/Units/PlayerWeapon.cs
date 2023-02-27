using Fusion;
using UnityEngine;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] private BaseWeapon[] weapon;

    public BaseWeapon CurrentWeapon { get; private set; }

    private void Start()
    {
        RPC_CreateWeapon();
    }

    [Rpc]
    private void RPC_CreateWeapon()
    {
        int rand = Random.Range(0, weapon.Length);
        CurrentWeapon = Instantiate(weapon[rand], transform);
        CurrentWeapon.transform.position = transform.position;
        CurrentWeapon.transform.position += Vector3.back * 1;
    }
}