using UnityEngine;

public class EnemyMoveState : IState
{
    public NetworkInputData Data { get; set; }

    private Animator _animator;
    private Unit _unit;
    private Rigidbody2D _rb;
    private Transform _follow;
    private int _animationID = Animator.StringToHash("Move");

    public EnemyMoveState(Unit unit) 
    {
        _animator = unit.GetComponentInChildren<Animator>();
        _unit = unit;
        _rb = unit.GetComponent<Rigidbody2D>();
    }

    public void Enter() 
    {
        if (_unit.Runner != null)
        {
            RPCList.RPC_ChangeAnimationID(_animator, _animationID);
        }
    }

    public void Exit() { }

    public void Update()
    {
        GetNearestPlayer();
        if (_follow != null)
        {
            Vector2 vel = (_follow.position - _unit.transform.position).normalized * _unit.Info.StartSpeed * _unit.Runner.DeltaTime;
            _rb.velocity = vel;
        }
    }

    private void GetNearestPlayer()
    {
        if (_unit.States.CurrentState is DeadState) return;
        if (Launcher.Chars.Count == 0) return;

        foreach (var pTransform in Launcher.Chars)
        {
            var player = pTransform.GetComponent<Player>();
            if (player.States.CurrentState is DeadState) continue;

            if (_follow == null) _follow = pTransform;
            if (Vector3.Distance(_follow.position, _unit.transform.position) > Vector3.Distance(pTransform.position, _unit.transform.position))
                _follow = pTransform;
        }
    }
}