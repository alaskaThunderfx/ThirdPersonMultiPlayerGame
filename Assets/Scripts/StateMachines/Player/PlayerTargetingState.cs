using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

namespace StateMachines.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        // Private variables

        // Hashes the TargetingBlendTree animation
        private static readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            // Plays the TargetingBlendTree animation when this state is entered
            StateMachine.Animator.Play(TargetingBlendTreeHash);

            // subscribes the OnCancel method when this button is pressed.
            StateMachine.InputReader.OnToggleTargetEvent += OnCancel;
        }

        public override void Tick(float deltaTime)
        {
            if (StateMachine.Targeter.CurrentTarget != null) return;
            StateMachine.SwitchState(new PlayerFreeLookState(StateMachine));
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