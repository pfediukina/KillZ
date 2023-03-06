using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HitState : IState
{
    private Unit _unit;
    private Rigidbody2D _rb;
    private Animator _anim;
    private int _animationID = Animator.StringToHash("Hit");

    private float _hitTimer = 0;
    private float _hitDuration = 0.4f;


    public HitState(Unit owner) 
    {
        _unit= owner;
        _rb = _unit.GetComponent<Rigidbody2D>();
        _anim = _unit.GetComponentInChildren<Animator>();
    }

    public void Enter()
    {
        _rb.velocity = Vector3.zero;
        _rb.velocity = Vector3.up;
        RPCList.RPC_ChangeAnimationID(_anim, _animationID);
        _hitTimer = 0;
    }

    public void Exit()
    {
        //throw new NotImplementedException();
    }

    public void Update()
    {
        _hitTimer += _unit.Runner.DeltaTime;
        if(_hitTimer >= _hitDuration)
        {
            _unit.States.SetState<EnemyMoveState>();
        }
        //throw new NotImplementedException();
    }


}