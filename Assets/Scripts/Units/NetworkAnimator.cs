using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAnimator : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;

    [Networked] public float WeaponZRotation { get; set; }

    private SpriteRenderer _weaponSprite;

    public void CalculateAndRotateWeapon(Vector2 mousePos, SpriteRenderer weapon)
    {
        if (weapon == null) return;
        if (_weaponSprite == null) _weaponSprite = weapon;
        if (HasInputAuthority)
        {
            Vector2 weaponPos = Camera.main.WorldToScreenPoint(weapon.transform.position);
            WeaponZRotation = Mathf.Atan2(mousePos.y - weaponPos.y, mousePos.x - weaponPos.x) * Mathf.Rad2Deg + (_weaponSprite.flipX ? 180 : 0);
            RPC_RotateWeapon(WeaponZRotation);
        }
        else
            RotateWeapon(WeaponZRotation);
    }

    [Rpc]
    public void RPC_ChangeAnimationID(int id)
    {
        _animator.CrossFade(id, 0);
    }

    [Rpc]
    public void RPC_Flip(bool flip)
    {
        _sprite.flipX = flip;
        if(_weaponSprite != null)
            _weaponSprite.flipX = flip;
    }

    [Rpc]
    public void RPC_RotateWeapon(float rot)
    {
        RotateWeapon(rot);
    }

    private void RotateWeapon(float rot)
    {
        if (_weaponSprite == null) return;
        _weaponSprite.transform.eulerAngles = Vector3.forward * rot;
        if (_weaponSprite.flipX) _weaponSprite.flipY = true;
        else _weaponSprite.flipY = false;
    }

}