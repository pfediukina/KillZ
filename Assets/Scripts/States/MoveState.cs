using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MoveState : IState
{
    public NetworkInputData Data { get; set; }

    //private Animator _animator;
    private Transform _transform;

    public MoveState(Unit unit) 
    {
        //_animator = unit.GetComponentInChildren<Animator>();
        _transform = unit.transform;
    }


    public void Enter()
    {
        _transform.Translate(Data.Direction * 5);
        //if (data.Direction.x != 0) FlipSrpite = data.Direction.x > 0 ? false : true;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}