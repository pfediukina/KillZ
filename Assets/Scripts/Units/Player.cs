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
    public PlayerUI UI { get; set; }

    public Action OnPlayerPressedMenu;

    protected override void Awake()
    {
        base.Awake();
        _input.OnBackPressed += PressedMenu;
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
            if (UI != null)
                UI.SwitchPlayerMenu();
            else
            {
                Debug.Log("Null ui");
            }
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