using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class KeyboardInput : IPlayerInput
{
    private PlayerActions _actions;

    public KeyboardInput(PlayerActions actions)  
    {
        _actions = actions;
    }

    public bool GetDirectionAndInvoke(Action<Vector2> ifMoved)
    {
        Vector2 direction;
        direction = _actions.Keyboard.Movement.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            ifMoved?.Invoke(direction);
            return true;
        }
        return false;
    }
}