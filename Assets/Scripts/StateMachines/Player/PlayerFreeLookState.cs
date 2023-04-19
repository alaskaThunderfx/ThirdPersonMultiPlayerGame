using System;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

namespace StateMachines.Player
{
    [Serializable]
    public class PlayerFreeLookState : PlayerBaseState
    {
        // Private variables

        // Assigns the int value from the FreeLookSpeed variable at the beginning of the game running and it will not 
        // change again
        private static readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

        // Variable that dictates the speed with which the animation transition between each other. Can be tweaked in the
        // editor
        [SerializeField] private float animatorDampTime = 0.1f;

        // Constructor method
        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        // Interface methods

        public override void Enter()
        {
            // Subscribes the OnTarget method when this state is enetered
            StateMachine.InputReader.OnToggleTargetEvent += OnTarget;
        }

        public override void Tick(float deltaTime)
        {
            // Establish the movement variable for use with transform.Translate
            Vector3 movement = CalculateMovement();

            // This uses the CharacterControllers built in Move method. It will move the character in the direction
            // based on the values it gets from the movement variable, the speed of which is dictated by the
            // configurable FreeLookMovementSpeed value set in the Editor (Player prefab). All of which is is multiplied
            // by deltaTime to keep the speed frame rate independent.
            StateMachine.CharacterController.Move(movement * (StateMachine.FreeLookMovementSpeed * deltaTime));

            // Checks to see if any values representing movement are being sent by the InputReader
            if (StateMachine.InputReader.MovementValue == Vector2.zero)
            {
                // If it is not, the FreeLookSpeed variable in the Animator will be set to zero, which indicates that 
                // the idle animation should be playing
                StateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, animatorDampTime, deltaTime);
                // Breaks the Tick loop here
                return;
            }

            // If there are movement values being sent from the InputReader, it changes the value of the FreeLookSpeed
            // Animator variable to 1, which indicates that the running animation should be playing
            StateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, animatorDampTime, deltaTime);

            FaceMovementDirection(movement, deltaTime);
        }


        public override void Exit()
        {
            // Unsubscribes the OnTarget event
            StateMachine.InputReader.OnToggleTargetEvent -= OnTarget;
        }

        // Private methods
        
        // Method that should toggle the targeting state
        private void OnTarget()
        {
            if (!StateMachine.Targeter.SelectTarget()) return;
            
            // Switches the states from this one to the TargetingState
            StateMachine.SwitchState(new PlayerTargetingState(StateMachine));
        }

        // Method that handles calculating the movement based on factors within the body of this method.
        private Vector3 CalculateMovement()
        {
            // Finds the forward and right direction in relation to the main camera
            Vector3 forward = StateMachine.MainCameraTransform.forward;
            Vector3 right = StateMachine.MainCameraTransform.right;

            // Sets their Y values to 0 as we only care about where forward is and whether it is looking left/right
            forward.y = 0;
            right.y = 0;

            // Sets the magnitude of each of these to 1
            forward.Normalize();
            right.Normalize();

            // Returns the adjusted movement values now in relation to where the camera is facing vs. just in the world
            // space
            return forward * StateMachine.InputReader.MovementValue.y +
                   right * StateMachine.InputReader.MovementValue.x;
        }

        // Method that corrects the rotation of the player
        private void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            // When the character does move, the built-in method Quaternion.LookRotation() will face the character in
            // the direction it is being moved, as dictated by the movement variable. 
            StateMachine.transform.rotation = Quaternion.Lerp(
                StateMachine.transform.rotation,
                Quaternion.LookRotation(movement),
                deltaTime * StateMachine.RotationDamping);
        }
    }
}