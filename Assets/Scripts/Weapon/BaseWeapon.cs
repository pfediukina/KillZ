
using Fusion;
using System;
using System.Collections;
using UnityEngine;

public class BaseWeapon : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Bullet _bulletPref;
    [SerializeField] private int _maxAmmo;

    public Action<int, int> OnAmmoChanged;
    
    public float Reload;

    public int Ammo { get => _ammo; 
        set
        {
            _ammo = value;
            AmmoChanged();
        }
    }
    private int _ammo;

    [Networked(OnChanged = nameof(OnOwnerChange))] public NetworkId ID { get; set; }

    public SpriteRenderer GetSprite() => _sprite;
    public bool _canShoot = true;

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
            var shot = Runner.Spawn(_bulletPref, transform.position);
            shot.MoveTo(dir);
            shot.Owner = Runner.FindObject(ID).GetComponent<Player>();
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
        yield return new WaitForSeconds(Reload);
        _canShoot = true;
    }
}
