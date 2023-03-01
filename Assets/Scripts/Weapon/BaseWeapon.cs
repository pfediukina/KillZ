using Fusion;
using System;
using System.Collections;
using UnityEngine;

public class BaseWeapon : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _reload = 0.3f;
    [SerializeField] private Bullet _bulletPref;

    [Networked(OnChanged = nameof(OnOwnerChange))] public NetworkId ID { get; set; }
    public SpriteRenderer GetSprite() => _sprite;
    public bool _canShoot = true;

    public static void OnOwnerChange(Changed<BaseWeapon> changed)
    {
        var player = changed.Behaviour.Runner.FindObject(changed.Behaviour.ID).GetComponent<Player>();
        changed.Behaviour.transform.parent = player.WeaponPlace;
        changed.Behaviour.transform.localPosition = Vector3.zero;
        player.GetComponent<NetworkAnimator>().WeaponSprite = changed.Behaviour.GetSprite();

    }

    public virtual void Shoot(Vector2 mousePos)
    {
        if(_canShoot)
        {
            var v = GetShootDirection(mousePos);
            RPC_Fire(v);
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
        yield return new WaitForSeconds(_reload);
        _canShoot = true;
    }
}
