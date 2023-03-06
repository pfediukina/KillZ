using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class IdleState : IState
{    private Animator _animator;
    private Unit _unit;
    private int _animationID = Animator.StringToHash("Idle");

    public IdleState(Unit unit) 
    { 
        _animator = unit.GetComponentInChildren<Animator>();
        _unit = unit;
    }

    public void Enter()
    {
        if (_unit.Runner != null)
        {
            RPCList.RPC_ChangeAnimationID(_animator, _animationID);
        }
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}