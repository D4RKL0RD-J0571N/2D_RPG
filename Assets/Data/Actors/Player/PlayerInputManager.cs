using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Data.Actors.Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        #region Variables

        private bool _actionButtonPressed =false;
        
        public event Action<bool> JumpStateChanged;

        public event Action<float> MovementStateChanged;

        public event Action<bool> OnActionButtonPressed;
        
        #endregion
        
        #region Input Methods
        
        /// <summary>
        /// Callback handler function for movement input.
        /// The logic for handling movement input is implemented in the Concrete State File.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnMovementInput(InputAction.CallbackContext context)
        {
            float inputValue = context.ReadValue<Vector2>().x;

            if (context.performed && inputValue != 0f)
            {
                MovementStateChanged?.Invoke(inputValue);
            }
            if (context.canceled && inputValue == 0f)
            {
                MovementStateChanged?.Invoke(inputValue);
            }
        }

        /// <summary>
        /// Callback handler function for jump input.
        /// The logic for handling jump input is implemented in the Concrete State File.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpStateChanged?.Invoke(true); // Jump button is pressed
            }
            if (context.canceled)
            {
                JumpStateChanged?.Invoke(false); // Jump button is released
            }
        }

        public void OnActionInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _actionButtonPressed = !_actionButtonPressed;
                
                if (_actionButtonPressed) // Pressed the button e
                {
                    OnActionButtonPressed?.Invoke(true);
                }
                // else // Pressed the button again
                // {
                //     ActionButtonPressed?.Invoke(false);
                // }
                
            }
        }
        #endregion
    }
}