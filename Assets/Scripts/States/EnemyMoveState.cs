using UnityEngine;

public class EnemyMoveState : IState
{
    public NetworkInputData Data { get; set; }

    private NetworkAnimator _animator;
    private Unit _unit;
    private Rigidbody2D _rb;
    public Transform Follow;
    private int _animationID = Animator.StringToHash("Move");

    public EnemyMoveState(Unit unit) 
    {
        _animator = unit.GetComponent<NetworkAnimator>();
        _unit = unit;
        _rb = unit.GetComponent<Rigidbody2D>();
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
        if (Follow != null)
        {
            Vector2 vel = (Follow.position - _unit.transform.position).normalized * _unit.Info.StartSpeed * _unit.Runner.DeltaTime;
            _rb.velocity = vel;
        }
    }
}