using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

namespace StateMachines.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            // subscribes the OnCancel method when this button is pressed.
            StateMachine.InputReader.OnToggleTargetEvent += OnCancel;
        }

        public override void Tick(float deltaTime)
        {
            Debug.Log(StateMachine.Targeter.CurrentTarget.name);
        }

        public override void Exit()
        {
            // Unsubscribes the method when exiting the state
            StateMachine.InputReader.OnToggleTargetEvent -= OnCancel;
        }

        private void OnCancel()
        {
            StateMachine.Targeter.Cancel();

            // Switches the state back to the FreeLookState
            StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
        }
    }
}