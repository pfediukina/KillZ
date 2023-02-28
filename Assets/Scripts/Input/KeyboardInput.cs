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

    public Vector2 GetDirection()
    {
        return _actions.Keyboard.Movement.ReadValue<Vector2>();
    }
}