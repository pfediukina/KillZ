using Assets.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action<Vector2> OnMovePerfomed;

    private PlayerActions _actions;
    private IPlayerInput _keyboard;
    private IPlayerInput _screen;

    private void Awake()
    {
        if (_actions == null)
        {
            _actions = new PlayerActions();
            SetupInput();
        }
    }

    private void OnEnable()
    {
        if (_actions != null) _actions.Enable();
        if (_keyboard != null) _keyboard.OnPlayerInput += ctx => OnMovePerfomed?.Invoke(ctx);
        if (_screen != null) _screen.OnPlayerInput += ctx => OnMovePerfomed?.Invoke(ctx);
    }

    private void OnDisable()
    {
        if (_actions != null) _actions.Disable();
        if (_keyboard != null) _keyboard.OnPlayerInput -= ctx => OnMovePerfomed?.Invoke(ctx);
        if (_screen != null) _screen.OnPlayerInput -= ctx => OnMovePerfomed?.Invoke(ctx);
    }

    private void Update()
    {
        _keyboard.GetDirectionAndInvoke();
        _screen.GetDirectionAndInvoke();
    }

    private void SetupInput()
    {
        _keyboard = new KeyboardInput(_actions);
        _screen = new ScreenInput(_actions);
    }
}