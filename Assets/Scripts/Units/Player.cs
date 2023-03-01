using Cinemachine;
using Fusion;
using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEngine;

//[RequireComponent(typeof(PlayerInput))]

public class Player : Unit
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private NetworkAnimator _anim;

    public Transform WeaponPlace;

    public BaseWeapon Weapon { get; set; }

    public PlayerInput Input
    {
        get
        {
            if (_input != null)
                return _input;
            else
                _input = GetComponent<PlayerInput>();
            return _input;

        }
    }
    private PlayerInput _input;

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
        States.AddState(new MoveState(this));

        Input.OnAttackPressed += ctx =>
        {
            if (Weapon != null)
            {
                Weapon.Shoot(ctx);
            }
        };

        Input.OnBackPressed += PressedMenu;
        Input.OnViewChanged += ctx => { if (Weapon != null) _anim.CalculateAndRotateWeapon(ctx, Weapon.GetSprite()); };
        Input.OnViewChanged += UI.FollowPoint;
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