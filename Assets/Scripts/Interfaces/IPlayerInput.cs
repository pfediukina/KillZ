using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{
    public bool GetDirectionAndInvoke(out Vector2 dir);
}
