

using Fusion;
using System.Collections;
using UnityEngine;

public class MoveState : IState
{
    public Vector2 Direction;

    private int _animationID = Animator.StringToHash("Move");
    private Animator _animator;
    private SpriteRenderer _sprite;
    private Unit _unit;

    public MoveState(Unit unit) 
    {
        _animator = unit.GetComponentInChildren<Animator>();
        _sprite = _animator.GetComponent<SpriteRenderer>();
        _unit = unit;
    }

    public void Enter()
    {
        if (_unit.Runner != null)
        {
            RPCList.RPC_ChangeAnimationID(_animator, _animationID);
        }
    }

    public void Exit() { }

    public void Update()
    {
        if (!_unit.Runner.TryGetInputForPlayer<NetworkInputData>(_unit.Object.InputAuthority, out var Data)) return;
        if (Data.Direction.x != 0) RPCList.RPC_FlipSprite(_sprite, Data.Direction.x > 0 ? false : true);
        _unit.transform.Translate(Data.Direction * 5 * _unit.Runner.DeltaTime);
    }
}