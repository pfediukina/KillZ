﻿
using Fusion;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseWeapon : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Bullet _bulletPref;
    [SerializeField] private Transform _bulletSpawn;

    [SerializeField] protected WeaponInfo _info;

    [HideInInspector][Networked(OnChanged = nameof(OnOwnerChange))] public NetworkId ID { get; set; }
    [HideInInspector] public bool _canShoot = true;

    public Action<int, int> OnAmmoChanged;

    public SpriteRenderer GetSprite() => _sprite;
    public int Ammo { get => _ammo; 
        set
        {
            _ammo = value;
            AmmoChanged();
        }
    }

    private int _ammo;
    private Unit _unit;


    private void Start()
    {
        Ammo = _info.MaxAmmo;
    }

    public void AmmoChanged()
    {
        _ammo = Mathf.Clamp(_ammo, 0, _info.MaxAmmo);
        OnAmmoChanged?.Invoke(_ammo, _info.MaxAmmo);
    }

    public static void OnOwnerChange(Changed<BaseWeapon> changed)
    {
        var player = changed.Behaviour.Runner.FindObject(changed.Behaviour.ID).GetComponent<Player>();
        changed.Behaviour.transform.parent = player.WeaponPlace;
        changed.Behaviour.transform.localPosition = Vector3.zero;
        changed.Behaviour._unit = player;
        player.GetComponent<NetworkAnimator>().WeaponSprite = changed.Behaviour.GetSprite();

        if(player.HasInputAuthority)
        {
            changed.Behaviour.OnAmmoChanged += player.UI.ChangePlayerAmmo;
            changed.Behaviour._unit = player;
        }
        changed.Behaviour.OnAmmoChanged?.Invoke(changed.Behaviour.Ammo, changed.Behaviour._info.MaxAmmo);
    }

    public virtual void Shoot(Vector3 direction)
    {
        if (_canShoot && Ammo > 0)
        {
            direction.z = 0;
            direction.Normalize();
            direction.z = _bulletSpawn.position.z;

            RPC_Fire(direction);
            if (_info.HasAmmo) Ammo--;
            StartCoroutine(FireReload());
        }
    }

    public virtual void Shoot(Transform transf)
    {
        if (_canShoot && Ammo > 0)
        {
            Vector3 pos = GetShootDirection(transf.position);
            RPC_Fire(pos);
            if (_info.HasAmmo) Ammo--;
            StartCoroutine(FireReload());
        }
    }

    [Rpc]
    private void RPC_Fire(Vector3 dir)
    {
        if(HasStateAuthority)
        {
            var shot = Runner.Spawn(_bulletPref, _bulletSpawn.position);
            shot.InitBullet(_unit, _info.Damage, dir, _info.EnemyTag, _info.AttackDistance, _info.IsExplosive);
            //shot.MoveTo(dir);
            //shot.Owner = Runner.FindObject(ID).GetComponent<Player>();
        }
    }

    public Vector3 GetShootDirection(Vector3 pos)
    {
        var result = pos - _bulletSpawn.position;
        result.z = 0;
        result.Normalize();
        result.z = _bulletSpawn.position.z; 
        return result;
    }

    private IEnumerator FireReload()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_info.AttackRate);
        _canShoot = true;
    }
}
