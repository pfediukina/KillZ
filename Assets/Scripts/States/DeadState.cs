using UnityEngine;

public class DeadState : IState
{
    private Unit _unit;
    public Vector3 DeathPos;

    public DeadState(Unit unit)
    {
        _unit = unit;
    }

    public void Enter()
    {
        _unit.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //throw new NotImplementedException();
    }

    public void Exit()
    {
       //throw new NotImplementedException();
    }

    public void Update()
    {
        //_unit.transform.position = DeathPos;
    }
}