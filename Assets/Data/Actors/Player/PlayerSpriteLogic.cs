using System;
using Data.BaseClasses;

namespace Data.Actors.Player
{
    [Serializable]
    public class PlayerSpriteLogic
    {
        #region Sprite Logic
        
        /// <summary>
        /// Handles the player's Sprite transition.
        /// </summary>
        public void HandleSpriteTransition(PlayerVariables playerVariables, ActorVariables actorVariables)
        {
            // Update sprite based on movement input
            if (playerVariables.CurrentMovementInput > 0f)
            {
                actorVariables.Spr.sprite = actorVariables.spriteMovingRight;
            }
            else if (playerVariables.CurrentMovementInput < 0f)
            {
                actorVariables.Spr.sprite = actorVariables.spriteMovingLeft;
            }
            else
            {
                actorVariables.Spr.sprite = actorVariables.spriteIdle;
            }
            if (playerVariables.IsJumping)
            {
                actorVariables.Spr.sprite = actorVariables.spriteJumping;
            }
        }
        
        #endregion
    }
}