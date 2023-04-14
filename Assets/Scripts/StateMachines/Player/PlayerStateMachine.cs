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

        // Public variable for the Camera that will be set in code
        public Transform MainCameraTransform { get; private set; }

        // Public variables representing values
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }

        // Built-in methods
        void Start()
        {
            // Confirms that the main camera exists, then sets the MainCameraTransform value to be equal to the
            // transform of the main camera in Unity
            if (Camera.main != null) MainCameraTransform = Camera.main.transform;
            
            // Switches state when PlayerStateMachine is called
            SwitchState(new PlayerFreeLookState(this));
        }
    }
}