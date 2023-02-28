

using Fusion;
using System.Collections;
using UnityEngine;

public class EnemyMoveState : IState
{
    public NetworkInputData Data { get; set; }

    private Unit _unit;
    public Transform Follow;

    public EnemyMoveState(Unit unit) 
    {
        //_animator = unit.GetComponent<NetworkAnimator>();
        _unit = unit;
    }

    public void Enter() { }

    public void Exit() { }

    public void Update()
    {
        if (Follow != null)
        {
            //Debug.Log("Follow");
            _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, Follow.position, _unit.Runner.DeltaTime);
        }
    }
}