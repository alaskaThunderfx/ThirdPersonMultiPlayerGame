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

            // Ends Tick loop if the character is not moving
            if (StateMachine.InputReader.MovementValue == Vector2.zero) return;

            // When the character does move, the built-in method Quaternion.LookRotation() will face the character in
            // the direction it is being moved, as dictated by the movement variable. 
            StateMachine.transform.rotation = Quaternion.LookRotation(movement);
        }

        public override void Exit()
        {
        }
    }
}