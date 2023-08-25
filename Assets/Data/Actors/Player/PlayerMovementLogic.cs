using System;
using Data.BaseClasses;
using UnityEngine;

namespace Data.Actors.Player
{
    [Serializable]
    public class PlayerMovementLogic
    {
         #region Movement Logic
        /// <summary>
        /// Handles the player's walking movement logic.
        /// </summary>
        public void HandleMovement(PlayerVariables playerVariables, ActorVariables actorVariables)
        {
            // Handle walking movement logic, similar to the grounded state
            float horizontalSpeed = playerVariables.CurrentMovementInput * actorVariables.moveSpeed;
            actorVariables.Rb.velocity = new Vector2(horizontalSpeed, actorVariables.Rb.velocity.y);
        }
        
        #endregion
        
        #region Jump Logic
        /// <summary>
        /// Handles the behavior of the jump on the release of the jump button. 
        /// </summary>
        public void HandleJump(PlayerVariables playerVariables, ActorVariables actorVariables)
        {
            if (playerVariables.JumpTimeCounter > 0)
            {
                actorVariables.Rb.velocity = new Vector2(actorVariables.Rb.velocity.x, actorVariables.jumpForce);
                playerVariables.JumpTimeCounter  -= Time.deltaTime;
            }
        }
        #endregion
        
        #region Gravity Logic
        /// <summary>
        /// Handles the gravity on falling multiplying the gravity to fall faster.
        /// </summary>
        public void HandleFallGravity(PlayerVariables playerVariables, ActorVariables actorVariables)
        {
            if (!playerVariables.IsGrounded && actorVariables.Rb.velocity.y < 0)
            {
                // Apply gravity multiplier to fall faster
                actorVariables.Rb.velocity += Vector2.up * (Physics2D.gravity.y * (actorVariables.fallGravityMultiplier - 1f) * Time.deltaTime);
            }
        }
        #endregion
    }
}