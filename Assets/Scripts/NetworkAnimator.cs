using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAnimator : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private PlayerWeapon _weapon;

    [Networked] public float WeaponZRotation { get; set; }

    private SpriteRenderer _weaponSprite;

    private void Update()
    {
        if (_weaponSprite == null && _weapon.CurrentWeapon != null)
            _weaponSprite = _weapon.CurrentWeapon.GetSprite();
    }

    public void CalculateAndRotateWeapon(Vector2 mousePos, NetworkObject player)
    {
        if (!player.HasInputAuthority) return;
        Vector2 weaponPos = Camera.main.WorldToScreenPoint(_weapon.transform.position);
        WeaponZRotation = Mathf.Atan2(mousePos.y - weaponPos.y, mousePos.x - weaponPos.x) * Mathf.Rad2Deg;
    }

    public override void FixedUpdateNetwork()
    {
        if (_weaponSprite != null)
            _weaponSprite.transform.eulerAngles = Vector3.forward * WeaponZRotation;
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