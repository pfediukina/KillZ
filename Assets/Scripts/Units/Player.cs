using Fusion;
using UnityEngine;

//[RequireComponent(typeof(PlayerInput))]

public class Player : Unit
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Networked(OnChanged = nameof(OnFlip))] private NetworkBool FlipSrpite { get; set; }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<NetworkInputData>(Object.InputAuthority, out var data))
        {
            if(States.CurrentState is not MoveState)
                States.SetState<MoveState>();
            //transform.Translate(data.Direction * _info.StartSpeed * Runner.DeltaTime);
            //if (data.Direction.x != 0) FlipSrpite = data.Direction.x > 0 ? false : true;
        }
    }

    //need static or SO MUCH errors
    private static void OnFlip(Changed<Player> changed)
    {
        changed.Behaviour._spriteRenderer.flipX = changed.Behaviour.FlipSrpite;
    }
}