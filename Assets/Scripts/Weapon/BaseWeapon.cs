using Fusion;
using System;
using System.Collections;
using UnityEngine;

public class BaseWeapon : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _reload = 0.3f;
    [SerializeField] private Bullet _bulletPref;


    public SpriteRenderer GetSprite() => _sprite;
    public bool _canShoot = true;

    public virtual void Shoot(Vector2 mousePos)
    {
        if(_canShoot)
        {
            Debug.Log("Shoot");
            var v = GetShootDirection(mousePos);
            RPC_Fire(v);
            StartCoroutine(FireReload());
        }
    }

    private Vector3 GetShootDirection(Vector2 mousePos)
    {
        var v = Camera.main.ScreenToWorldPoint(mousePos);
        v.z = transform.position.z;
        return v;
    }

    private void RPC_Fire(Vector3 dir)
    {
        var b = Runner.Spawn(_bulletPref);
        b.transform.position = transform.position;
        b.MoveTo(dir);
        b.StartMove = true;
    }

    private IEnumerator FireReload()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_reload);
        _canShoot = true;
    }
}
