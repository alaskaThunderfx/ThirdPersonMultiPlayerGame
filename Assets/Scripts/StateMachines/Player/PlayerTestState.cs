using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        private float Timer;

        public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        // Interface methods

        public override void Enter()
        {
            Debug.Log("Enter");
            StateMachine.InputReader.OnJumpEvent += OnJump;
        }

        public override void Tick(float deltaTime)
        {
            Timer += deltaTime;

            Debug.Log(Timer);
        }

        public override void Exit()
        {
            Debug.Log("Exit");
            StateMachine.InputReader.OnJumpEvent -= OnJump;
        }

        // Private methods

        private void OnJump()
        {
            StateMachine.SwitchState(new PlayerTestState(StateMachine));
        }
    }
}