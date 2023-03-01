using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAnimator : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;

    [Networked] public float WeaponZRotation { get; set; }

    public SpriteRenderer WeaponSprite;

    public void CalculateAndRotateWeapon(Vector2 mousePos, SpriteRenderer weapon)
    {
        if (weapon == null) return;
        if (WeaponSprite == null) WeaponSprite = weapon;
        if (HasInputAuthority)
        {
            Vector2 weaponPos = Camera.main.WorldToScreenPoint(weapon.transform.position);
            WeaponZRotation = Mathf.Atan2(mousePos.y - weaponPos.y, mousePos.x - weaponPos.x) * Mathf.Rad2Deg + (WeaponSprite.flipX ? 180 : 0);
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
        if(WeaponSprite != null)
            WeaponSprite.flipX = flip;
    }

    [Rpc]
    public void RPC_RotateWeapon(float rot)
    {
        RotateWeapon(rot);
    }

    private void RotateWeapon(float rot)
    {
        if (WeaponSprite == null) return;
        WeaponSprite.transform.eulerAngles = Vector3.forward * rot;
        if (WeaponSprite.flipX) WeaponSprite.flipY = true;
        else WeaponSprite.flipY = false;
    }

}