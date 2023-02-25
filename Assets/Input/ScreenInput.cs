using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ScreenInput //: IPlayerInput
{
    private PlayerActions _actions;

    public ScreenInput(PlayerActions actions)
    {
        _actions = actions;
    }


    public bool GetDirectionAndInvoke(out Vector2 dir)
    {
        Vector2 direction;
        direction = _actions.Screen.Movement.ReadValue<Vector2>();
        dir = direction;
        if (direction != Vector2.zero) return true;
        return false;
    }
}

