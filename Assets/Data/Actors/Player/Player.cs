using System.Collections;
using Data.BaseClasses;
using Misc;
using UnityEngine;

namespace Data.Actors.Player
{
    public class Player : BaseActor 
    {
        public static Player Instance { get; private set; }
        
        #region External References
        [SerializeField] private PlayerVariables playerVariables;
        [SerializeField] private PlayerMovementLogic playerMovementLogic;
        [SerializeField] private PlayerSpriteLogic playerSpriteLogic;
        #endregion
        
        public PlayerVariables Variables
        {
            get => playerVariables;
            set => playerVariables = value;
        }
        
        #region Event Methods
        private void Awake()
        {
            // Subscribe to jump events
            Variables.PlayerInputManager.JumpStateChanged += OnJumpStateChanged;
            Variables.PlayerInputManager.MovementStateChanged += OnMovementStateChange;
            // _itemCollector.OnItemCollected += HandleItemCollected;
            InitializeActorStats(this);
        }
        
        private void Start()
        {
            actorVariables.Rb = GetComponent<Rigidbody2D>();
            actorVariables.Spr = GetComponent<SpriteRenderer>();
            
            ActorStatsModifier.ModifyActorHealth(this, 100f);
            ActorStatsModifier.ModifyActorArmor(this, 100f);
            
            // StartCoroutine(EquipUseUnequipCoroutine());
        }

        private void FixedUpdate()
        {
            if (Variables.IsJumping)
            {
                playerMovementLogic.HandleJump(Variables, actorVariables);
            }
            playerMovementLogic.HandleFallGravity(Variables, actorVariables);
        }
        
        private void Update()
        {
            playerMovementLogic.HandleMovement(Variables, actorVariables);
                playerSpriteLogic.HandleSpriteTransition(Variables, actorVariables);
        }
        
        private void LateUpdate()
        {
            Variables.IsGrounded = this.IsGroundedWithRaycast(actorVariables.groundLayer, actorVariables.groundRaycastLength, 
                actorVariables.slopeRaycastAngle, actorVariables.slopeRaycastLength);
            
        }
        
        private void OnDestroy()
        {
            Variables.PlayerInputManager.JumpStateChanged -= OnJumpStateChanged;
            Variables.PlayerInputManager.MovementStateChanged -= OnMovementStateChange;
            // _itemCollector.OnItemCollected -= HandleItemCollected;
        }
        #endregion

        #region OtherMethods
        private void OnDrawGizmos()
        {
            this.DrawGizmosForRaycast(actorVariables.showRaycastDebug, actorVariables.groundRaycastLength, actorVariables.slopeRaycastAngle, actorVariables.slopeRaycastLength);
        }
        
        private void OnMovementStateChange(float inputValue)
        {
            Variables.CurrentMovementInput = inputValue;
        }
        
        private void OnJumpStateChanged(bool isJumping)
        {
            if (isJumping)
            {
                if (Variables.IsGrounded)
                {
                    Variables.IsJumping = true;
                    Variables.JumpTimeCounter = actorVariables.maxJumpTime;
                }
            }
            else
            {
                Variables.IsJumping = false;
            }
        }
        #endregion
        
        private IEnumerator EquipUseUnequipCoroutine()
        {
            
            while (true)
            {
                Stats.PrintActorStats(this);
                yield return new WaitForSeconds(3f); // Wait for 10 
                
                ActorInventory.AddItem(Variables.DebugArmor);
                ActorInventory.AddItem(Variables.DebugSword);
                ActorInventory.AddItem(Variables.DebugUsableItem);
                
                yield return new WaitForSeconds(3f);
                
                // Equip items
                ActorInventory.EquipItem(this, Variables.DebugArmor);
                ActorInventory.EquipItem(this, Variables.DebugSword);

                // Use an item
                ActorInventory.UseItem(this, Variables.DebugUsableItem);  

                yield return new WaitForSeconds(2f); // Wait for 10 s
                
                Stats.PrintActorStats(this);
                yield return new WaitForSeconds(3f); // Wait for 10 s

                // Unequip items
                ActorInventory.UnequipItem(this, Variables.DebugArmor);
                ActorInventory.UnequipItem(this, Variables.DebugSword);

                yield return new WaitForSeconds(2f); // Wait for 10 s
                
                ActorInventory.RemoveItem(Variables.DebugArmor);
                ActorInventory.RemoveItem(Variables.DebugSword);
                ActorInventory.RemoveItem(Variables.DebugUsableItem);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}