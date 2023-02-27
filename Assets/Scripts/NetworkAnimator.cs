using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAnimator : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private PlayerWeapon _weapon;

    private SpriteRenderer _weaponSprite;

    private void Update()
    {
        if (_weaponSprite == null && _weapon.CurrentWeapon != null)
            _weaponSprite = _weapon.CurrentWeapon.GetSprite();
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
}