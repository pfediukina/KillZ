

using Fusion;
using System.Collections;
using UnityEngine;

public class MoveState : IState
{
    public NetworkInputData Data { get; set; }

    private int _animationID = Animator.StringToHash("Move");
    private NetworkAnimator _animator;
    private Unit _unit;

    public MoveState(Unit unit) 
    {
        _animator = unit.GetComponent<NetworkAnimator>();
        _unit = unit;
    }

    public void Enter()
    {
        if (_unit.Runner != null)
        {
            _animator.RPC_ChangeAnimationID(_animationID);
        }
    }

    public void Exit() { }

    public void Update()
    {
        if (Data.Direction.x != 0) _animator.RPC_Flip(Data.Direction.x > 0 ? false : true);
        _unit.transform.Translate(Data.Direction * 5 * _unit.Runner.DeltaTime);
    }
}