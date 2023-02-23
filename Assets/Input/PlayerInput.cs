using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerActions _actions;

    private void Awake()
    {
        if (_actions == null)
        {
            _actions = new PlayerActions();
        }
    }

    private void OnEnable()
    {
        if (_actions != null) _actions.Enable();
    }

    private void OnDisable()
    {
        if (_actions != null) _actions.Disable();
    }

    public Vector2 ReturnDirection()
    {
        if (_actions == null) return Vector2.zero;
        return _actions.Keyboard.Movement.ReadValue<Vector2>();
    }
}