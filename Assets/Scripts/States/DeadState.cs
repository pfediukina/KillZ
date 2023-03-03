using UnityEngine;

public class DeadState : IState
{    
    private Unit _unit;
    private int _animationID = Animator.StringToHash("Dead");
    private Collider2D _collider;
    private NetworkAnimator _animator;

    public DeadState(Unit unit)
    {
        _animator = unit.GetComponent<NetworkAnimator>();
        _collider = unit.GetComponent<Collider2D>();
        _unit = unit;
    }

    public void Enter()
    {
        _collider.enabled = false;
        _unit.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (_unit.Runner != null)
        {
            _animator.RPC_ChangeAnimationID(_animationID);
        }
        if (_unit is not Player)
        {
            (_unit as ISpawnObject).DespawnObject();
        }
    }

    public void Exit()
    {
        _collider.enabled = true;
    }

    public void Update()
    {
        //_unit.transform.position = DeathPos;
    }
}