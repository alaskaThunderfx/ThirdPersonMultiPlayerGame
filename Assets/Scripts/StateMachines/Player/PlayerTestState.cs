using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        private float Timer;
        
        public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            Debug.Log("Enter");
        }

        public override void Tick(float deltaTime)
        {
            Debug.Log("Tick");
            Timer += deltaTime;
            Debug.Log(5 - Timer);
            if (Timer >= 5)
            {
                StateMachine.SwitchState(new PlayerTestState(StateMachine));
            }
        }

        public override void Exit()
        {
            Debug.Log("Exit");
        }
    }
}

