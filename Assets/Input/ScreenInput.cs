using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenInput : IPlayerInput
{
    private PlayerActions _actions;

    public ScreenInput(PlayerActions actions)
    {
        _actions = actions;
    }

    public bool GetDirectionAndInvoke(Action ifMoved, out Vector2 dir)
    {
        Vector2 direction;
        direction = _actions.Screen.Movement.ReadValue<Vector2>();
        dir = direction;
        if (direction != Vector2.zero)
        {
            ifMoved?.Invoke();
            return true;
        }
        return false;
    }
}

