using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class IdleState : IState
{
    public NetworkInputData Data { get; set; }

    private NetworkAnimator _animator;
    private Unit _unit;
    private int _animationID = Animator.StringToHash("Idle");

    public IdleState(Unit unit) 
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

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}