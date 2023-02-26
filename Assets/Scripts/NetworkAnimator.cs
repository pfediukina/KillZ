using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAnimator : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;

    [Rpc]
    public void RPC_ChangeAnimationID(int id)
    {
        _animator.CrossFade(id, 0);
    }

    [Rpc]
    public void RPC_Flip(bool flip)
    {
        _sprite.flipX = flip;
    }
}