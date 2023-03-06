using UnityEngine;

public class DeadState : IState
{    
    private Unit _unit;
    private int _animationID = Animator.StringToHash("Dead");
    private Collider2D _collider;
    private Animator _animator;

    public DeadState(Unit unit)
    {
        _animator = unit.GetComponentInChildren<Animator>();
        _collider = unit.GetComponent<Collider2D>();
        _unit = unit;
    }

    public void Enter()
    {
        _collider.enabled = false;
        _unit.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (_unit.Runner != null)
        {
            RPCList.RPC_ChangeAnimationID(_animator, _animationID);
        }
        if (_unit is not Player)
        {
            (_unit as ISpawnObject).DespawnObject();
        }
        else if(_unit is Player)
        {
            (_unit as Player).Weapon.gameObject.SetActive(false);
            (_unit as Player).UI.GameText.ShowGameText(GameText.Dead);
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