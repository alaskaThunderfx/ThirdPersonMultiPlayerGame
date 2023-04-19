using UnityEngine;

namespace StateMachines.Player
{
    public abstract class PlayerBaseState : State
    {
        // Private variables
        // protected variables can only be accessed by classes that inherit this class
        protected readonly PlayerStateMachine StateMachine;

        // Public methods
        // This is a Constructor method
        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected void Move(Vector3 motion, float deltaTime)
        {
            StateMachine.CharacterController.Move((motion + StateMachine.ForceReceiver.Movement) * deltaTime);
        }
    }
}