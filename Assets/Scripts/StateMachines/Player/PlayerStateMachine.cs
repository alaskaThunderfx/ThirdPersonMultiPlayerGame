using PlayerControls;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        // Public variables for Character prefab components
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        
        // Public variables representing values
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }

        // Built-in methods
        void Start()
        {
            // Switches state when PlayerStateMachine is called
            SwitchState(new PlayerTestState(this));
        }
    }
}