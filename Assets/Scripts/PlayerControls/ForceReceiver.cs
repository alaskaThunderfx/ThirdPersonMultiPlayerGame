using UnityEngine;

namespace PlayerControls
{
    public class ForceReceiver : MonoBehaviour
    {
        // Gives access to the CharacterController
        [SerializeField] public CharacterController characterController;
        
        // Variable meant to represent the vertical velocity
        private float VerticalVelocity;

        // Meant to be accessed by other states so that whatever movement maybe be happening, if the states change, the
        // character will continue the movement from the previous state
        public Vector3 Movement => Vector3.up * VerticalVelocity;

        private void Update()
        {
            // Checks to see if the vertical velocity is less than 0 and if the character is also on the ground
            if (VerticalVelocity < 0f && characterController.isGrounded)
            {
                // if it is, the vertical velocity is set to the current gravity on the Y axis and multiplies it by
                // Time.deltaTime to make sure that the value is framerate independent
                VerticalVelocity = Physics.gravity.y * Time.deltaTime;
            }
            // if the vertical velocity is above 0 and the character is not currently on the ground, the vertical velocity
            // is increased by the gravity value on the Y axis and is multiplied by Time.deltaTime to keep it framrate
            // independent
            else
            {
                VerticalVelocity += Physics.gravity.y * Time.deltaTime;
            }
        }
    }
}

