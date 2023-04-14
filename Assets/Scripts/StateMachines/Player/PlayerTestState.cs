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
            
            // Move the object that this script is attached to by the values set in the movement variable and multiplied
            // by deltaTime to make it frame rate independent.
            StateMachine.transform.Translate(movement * deltaTime);
        }

        public override void Exit()
        {
        }
    }
}