using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Input
{
    internal class KeyboardInput : IPlayerInput
    {
        public event IPlayerInput.PlayerInputHandler OnPlayerInput;

        private PlayerActions _actions;

        public KeyboardInput(PlayerActions actions)  
        {
            _actions = actions;
        }

        public void GetDirectionAndInvoke()
        {
            Vector2 direction;
            direction = _actions.Keyboard.Movement.ReadValue<Vector2>();
            if(direction != Vector2.zero)
                OnPlayerInput?.Invoke(direction);
        }
    }
}
