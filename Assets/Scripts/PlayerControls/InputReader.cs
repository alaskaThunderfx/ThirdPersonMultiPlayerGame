using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace PlayerControls
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        // Public variables
        public Vector2 MovementValue { get; private set; }

        // Events
        public event Action OnJumpEvent;
        public event Action OnDodgeEvent;
        public event Action OnToggleTargetEvent;


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

        public void OnMove(InputAction.CallbackContext context)
        {
            // Just assigns the MovementValue value to be whatever values are received via the context variable
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            // This method is here as a requirement from the Control object in Unity. However, the actual control of 
            // looking is handled by Cinemachine, so this body will remain blank
        }

        public void OnToggleTarget(InputAction.CallbackContext context)
        {
            // My goal is to have the same button enter and exit targeting state. If the player is not in a targeting
            // state, the TargetCancelTarget button should put them into the state, and if the player is in the state
            // then it should take them out of it.
            if (!context.performed) return;

            OnToggleTargetEvent?.Invoke();
        }
    }
}