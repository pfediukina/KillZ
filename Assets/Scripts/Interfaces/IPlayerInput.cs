using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{
    public delegate void PlayerInputHandler(Vector2 direction);
    public event PlayerInputHandler OnPlayerInput;

    public Vector2 GetDirectionAndInvoke();
}
