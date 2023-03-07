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
    private NetworkAnimator _animator;
    private int _animationID = Animator.StringToHash("Hit");

    private float _hitTimer = 0;
    private float _hitDuration = 0.4f;


    public HitState(Unit unit)
    {
        _unit = unit;
        _rb = _unit.GetComponent<Rigidbody2D>();
        _animator = unit.GetComponent<NetworkAnimator>();
    }

    public void Enter()
    {
        _rb.velocity = Vector3.zero;
        _rb.velocity = Vector3.up;
        if (_unit.Runner != null)
        {
            _animator.RPC_ChangeAnimationID(_animationID);
        }
        _hitTimer = 0;
    }

    public void Exit() { }

    public void Update()
    {
        _hitTimer += _unit.Runner.DeltaTime;
        if (_hitTimer >= _hitDuration)
        {
            _unit.States.SetState<EnemyMoveState>();
        }
    }
}