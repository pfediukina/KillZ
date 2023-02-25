

using Fusion;
using System.Collections;
using UnityEngine;

public class MoveState : IState
{
    public NetworkInputData Data { get; set; }

    //private Animator _animator;
    private Unit _unit;
    private NetworkTransform _transform;

    public MoveState(Unit unit) 
    {
        //_animator = unit.GetComponentInChildren<Animator>();
        _unit = unit;
        _transform = _unit.GetComponent<NetworkTransform>();
    }


    public void Enter()
    {
        //if (data.Direction.x != 0) FlipSrpite = data.Direction.x > 0 ? false : true;
    }

    public void Exit() { }

    public void Update()
    {
        _unit.transform.Translate(Data.Direction * 5 * _unit.Runner.DeltaTime);
    }
}