using Unity.Mathematics;
using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        // Interface methods

        public override void Enter()
        {
        }

        public override void Tick(float deltaTime)
        {
            // Establish the movement variable for use with transform.Translate
            Vector3 movement = new Vector3();

            // Set the X value of movement to the X value being read by the InputReader
            movement.x = StateMachine.InputReader.MovementValue.x;
            // Set the Y value of movement to 0, as we want the player to only be able to move on the ground
            movement.y = 0;
            // Set the Z value of movement to the Y value being read by the InputReader
            movement.z = StateMachine.InputReader.MovementValue.y;

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
                StateMachine.Animator.SetFloat("FreeLookSpeed", 0, 0.1f, deltaTime);
                // Breaks the Tick loop here
                return;
            }
            
            // If there are movement values being sent from the InputReader, it changes the value of the FreeLookSpeed
            // Animator variable to 1, which indicates that the running animation should be playing
            StateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);

            // When the character does move, the built-in method Quaternion.LookRotation() will face the character in
            // the direction it is being moved, as dictated by the movement variable. 
            StateMachine.transform.rotation = Quaternion.LookRotation(movement);
        }

        public override void Exit()
        {
        }
    }
}