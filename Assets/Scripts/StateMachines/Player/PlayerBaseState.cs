namespace StateMachines.Player
{
    public abstract class PlayerBaseState : State
    {
        // Private variables
        // protected variables can only be accessed by classes that inherit this class
        protected PlayerStateMachine StateMachine;

        // Public methods
        // This is a Constructor method
        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}

