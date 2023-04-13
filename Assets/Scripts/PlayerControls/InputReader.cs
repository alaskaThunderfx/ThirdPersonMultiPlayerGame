using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControls
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        // Events
        public event Action OnJumpEvent;
        public event Action OnDodgeEvent;

        // Private variables
        private Controls Controls;

        // Built-in methods
        void Start()
        {
            Controls = new Controls();
            Controls.Player.SetCallbacks(this);

            Controls.Player.Enable();
        }

        void OnDestroy()
        {
            Controls.Player.Disable();
        }

        // Public methods
        public void OnJump(InputAction.CallbackContext context)
        {
            // Checks to see if the button has been pressed, returns if not
            if (!context.performed) return;
            
            // Checks that OnJumpEvent is not null, then invokes it
            OnJumpEvent?.Invoke();
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            // Checks to see if the button has been pressed, returns if not
            if (!context.performed) return;
            
            // Checks that OnDodgeEvent is not null, then invokes it
            OnDodgeEvent?.Invoke();
        }
    }
}