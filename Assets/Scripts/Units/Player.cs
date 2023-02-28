using Cinemachine;
using Fusion;
using JetBrains.Annotations;
using System;
using UnityEngine;

//[RequireComponent(typeof(PlayerInput))]

public class Player : Unit
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private NetworkAnimator _anim;

    public PlayerUI UI
    {
        get
        {
            if (_ui != null)
                return _ui;
            else
                _ui = FindObjectOfType<PlayerUI>();
            return _ui;

        }
    }
    private PlayerUI _ui;

    public Action OnPlayerPressedMenu;

    protected override void Awake()
    {
        base.Awake();
        _input.OnBackPressed += PressedMenu;
        _input.OnViewChanged += ctx => _anim.CalculateAndRotateWeapon(ctx, Object);
    }

    private void Start()
    {
        if (HasInputAuthority)
        {
            _camera.Priority = 10;
        }
    }

    private void PressedMenu()
    {
        if (HasInputAuthority)
        {
            UI.SwitchPlayerMenu();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<NetworkInputData>(Object.InputAuthority, out var data))
        {
            if (data.Direction != Vector2.zero && States.CurrentState is not MoveState)
            {
                States.SetState<MoveState>();
            }
            else if (data.Direction == Vector2.zero && States.CurrentState is not IdleState)
            {
                States.SetState<IdleState>();
            }
        }
    }
}