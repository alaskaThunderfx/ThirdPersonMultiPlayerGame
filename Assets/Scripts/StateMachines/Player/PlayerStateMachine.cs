using PlayerControls;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        // Public variables
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }

        // Built-in methods
        void Start()
        {
            // Switches state when PlayerStateMachine is called
            SwitchState(new PlayerTestState(this));
        }
    }
}