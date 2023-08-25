using System;
using Data.BaseClasses;
using Misc.Manager.Items;
using UnityEngine;

namespace Data.Actors.Player
{
    [Serializable]
    public class PlayerVariables 
    {
        #region DebugItems

        [Header("Debug Items")]
        [SerializeField] private BaseItem debugSword;
        [SerializeField] private BaseItem debugArmor;
        [SerializeField] private BaseItem debugUsableItem;
        
        #endregion
        
        #region Variables
        // External Classes References
        [Header("External Classes")]
        [SerializeField] private PlayerInputManager playerInputManager;
        [SerializeField] private ItemCollector itemCollector;
        
        // Player Input Variables
        // Variables to store player movement input values
        private float _currentMovementInput;
        private bool _isMovementButtonPressed;
        
        private bool _isJumping;
        private bool _isGrounded;
        private float _jumpTimeCounter;
        #endregion

        public BaseItem DebugSword
        {
            get => debugSword;
            set => debugSword = value;
        }

        public BaseItem DebugArmor
        {
            get => debugArmor;
            set => debugArmor = value;
        }

        public BaseItem DebugUsableItem
        {
            get => debugUsableItem;
            set => debugUsableItem = value;
        }

        public PlayerInputManager PlayerInputManager
        {
            get => playerInputManager;
            set => playerInputManager = value;
        }

        public ItemCollector Collector
        {
            get => itemCollector;
            set => itemCollector = value;
        }

        public float CurrentMovementInput
        {
            get => _currentMovementInput;
            set => _currentMovementInput = value;
        }

        public bool IsMovementButtonPressed
        {
            get => _isMovementButtonPressed;
            set => _isMovementButtonPressed = value;
        }

        public bool IsJumping
        {
            get => _isJumping;
            set => _isJumping = value;
        }

        public bool IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }

        public float JumpTimeCounter
        {
            get => _jumpTimeCounter;
            set => _jumpTimeCounter = value;
        }
    }
}