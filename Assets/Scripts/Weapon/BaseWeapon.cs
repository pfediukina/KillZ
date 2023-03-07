
using Fusion;
using System;
using System.Collections;
using UnityEngine;

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
        player.GetComponent<NetworkAnimator>().WeaponSprite = changed.Behaviour.GetSprite();

        if(player.HasInputAuthority)
        {
            changed.Behaviour.OnAmmoChanged += player.UI.ChangePlayerAmmo;
            changed.Behaviour._unit = player;
        }
        changed.Behaviour.OnAmmoChanged?.Invoke(changed.Behaviour.Ammo, changed.Behaviour._info.MaxAmmo);

    }

    public virtual void Shoot(Vector2 mousePos)
    {
        if(_canShoot && Ammo > 0)
        {
            var v = GetShootDirection(mousePos);
            RPC_Fire(v);
            Ammo--;
            StartCoroutine(FireReload());
        }
    }

    [Rpc]
    private void RPC_Fire(Vector3 dir)
    {
        if(HasStateAuthority)
        {
            var shot = Runner.Spawn(_bulletPref, _bulletSpawn.position);
            shot.InitBullet(_unit, _info.Damage, dir, _info.EnemyTag);
            //shot.MoveTo(dir);
            //shot.Owner = Runner.FindObject(ID).GetComponent<Player>();
        }
    }

    private Vector3 GetShootDirection(Vector2 mousePos)
    {
        var mouse = Camera.main.ScreenToWorldPoint(mousePos);
        var result = mouse - _bulletSpawn.position;

        result.z = 0;
        result.Normalize();
        result *= _info.AttackDistance;
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
