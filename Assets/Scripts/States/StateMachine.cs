using UnityEngine;

namespace States
{
    public class StateMachine : MonoBehaviour
    {
        
        // Private variables 
        private State CurrentState;

        // Built-in methods
        void Start()
        {
        }

        void Update()
        {
            // Checks whether the CurrentState variable has a value or not, and if it does, it proceeds to Tick in the
            // state that it currently is in while being framerate independent with Time.deltaTime
            CurrentState?.Tick(Time.deltaTime);
        }
        
        // Private methods
        void SwitchState(State newState)
        {
            // Checks whether the CurrentState variable has a value, if it does, it exits the current state
            CurrentState?.Exit();
            // Updates the CurrentState to the value of the newState being passed in
            CurrentState = newState;
            // Checks whether CurrentState has a value, and if it does, it enters in to the new value of CurrentState
            CurrentState?.Enter();
        }
    }
}