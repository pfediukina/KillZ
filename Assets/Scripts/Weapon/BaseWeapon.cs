
using Fusion;
using System;
using System.Collections;
using UnityEngine;

public class BaseWeapon : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Bullet _bulletPref;
    [SerializeField] private float _attackDelay;
    [SerializeField] private int _maxAmmo;

    public Action<int, int> OnAmmoChanged;
    public int Ammo { get => _ammo; 
        set
        {
            _ammo = value;
            AmmoChanged();
        }
    }
    private int _ammo;

    [HideInInspector][Networked(OnChanged = nameof(OnOwnerChange))] public NetworkId ID { get; set; }
    [HideInInspector] public bool _canShoot = true;

    public SpriteRenderer GetSprite() => _sprite;

    private void Start()
    {
        Ammo = _maxAmmo;
    }

    public void AmmoChanged()
    {
        _ammo = Mathf.Clamp(_ammo, 0, _maxAmmo);
        OnAmmoChanged?.Invoke(_ammo, _maxAmmo);
    }

    public static void OnOwnerChange(Changed<BaseWeapon> changed)
    {
        var player = changed.Behaviour.Runner.FindObject(changed.Behaviour.ID).GetComponent<Player>();
        changed.Behaviour.transform.parent = player.WeaponPlace;
        changed.Behaviour.transform.localPosition = Vector3.zero;
        player.GetComponent<NetworkAnimator>().WeaponSprite = changed.Behaviour.GetSprite();

        if(player.HasInputAuthority)
            changed.Behaviour.OnAmmoChanged += player.UI.ChangePlayerAmmo;
        changed.Behaviour.OnAmmoChanged?.Invoke(changed.Behaviour.Ammo, changed.Behaviour._maxAmmo);
    }

    public virtual void Shoot(Vector2 mousePos)
    {
        if(_canShoot && Ammo > 0)
        {
            var v = GetShootDirection(mousePos);
            RPCList.RPC_Fire(Runner.FindObject(ID).GetComponent<Player>(), v, _bulletPref, 4f);
            Ammo--;
            StartCoroutine(FireReload());
        }
    }

    private Vector3 GetShootDirection(Vector2 mousePos)
    {
        var v = Camera.main.ScreenToWorldPoint(mousePos);
        v.z = transform.position.z;
        return v;
    }

    private IEnumerator FireReload()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_attackDelay);
        _canShoot = true;
    }
}
