using System;
using System.Collections.Generic;
using Data.BaseClasses.Scriptable;
using Data.Global;
using UnityEngine;

namespace Data.BaseClasses
{
    /// <summary>
    /// Represents a base actor in the game. Contains actor data, stats, inventory, and methods for modifying actor stats and inventory.
    /// </summary>
    public abstract class BaseActor : BaseEntity
    {
        [SerializeField] private ScriptableActor actorData;

        [SerializeField] protected internal ActorVariables actorVariables = new ActorVariables();

        [SerializeField] private ActorStats stats = new ActorStats();

        [SerializeField] private BaseActorContainer actorInventory = new BaseActorContainer();

        protected internal ActorStatsModifier ActorStatsModifier = new ActorStatsModifier();

        public ScriptableActor ActorData
        {
            get => actorData;
            set => actorData = value;
        }

        public ActorStats Stats
        {
            get => stats;
            set => stats = value;
        }

        public BaseActorContainer ActorInventory
        {
            get => actorInventory;
            set => actorInventory = value;
        }
        
        protected void InitializeActorStats(BaseActor baseActor) // As each scriptable stat holds a base value we dont need to set the value to the actor initial value we need to set the current stat value for each actor instead.
        {
            
            BaseStat.InitializeStat(Stats.ActorLevel, baseActor.ActorData.initialLevel);
            BaseStat.InitializeStat(Stats.HealthData, baseActor.ActorData.initialHealth);
            BaseStat.InitializeStat(Stats.StaminaData, baseActor.ActorData.initialStamina);
            BaseStat.InitializeStat(Stats.MagicData, baseActor.ActorData.initialMagic);
        }
    }
    
    /// <summary>
    /// Represents the statistics and attributes of an actor.
    /// </summary>
    [Serializable]
    public class ActorStats
    {
        #region Actor Stats

        [SerializeField] private HealthStat healthData;
        [SerializeField] private StaminaStat staminaData;
        [SerializeField] private MagicStat magicData;
        [SerializeField] private WeightStat actorWeight;
        [SerializeField] private BaseStat actorArmor;
        [SerializeField] private BaseStat actorDamage;
        [SerializeField] private LevelStat actorLevel;

        #region Getters Setters
        public HealthStat HealthData
        {
            get => healthData;
            set => healthData = value;
        }

        public StaminaStat StaminaData
        {
            get => staminaData;
            set => staminaData = value;
        }

        public MagicStat MagicData
        {
            get => magicData;
            set => magicData = value;
        }

        public WeightStat ActorWeight
        {
            get => actorWeight;
            set => actorWeight = value;
        }

        public BaseStat ActorArmor
        {
            get => actorArmor;
            set => actorArmor = value;
        }

        public BaseStat ActorDamage
        {
            get => actorDamage;
            set => actorDamage = value;
        }

        public LevelStat ActorLevel
        {
            get => actorLevel;
            set => actorLevel = value;
        }
        
        #endregion
        
        #endregion
        


        public void PrintActorStats(BaseActor actor)
        {
            Debug.Log("----- Actor Stats -----");
    
            var health = actor.Stats.HealthData;
            var healthValue = health.GetStatValue(health);
            Debug.Log("Health: " + healthValue + " (Base: " + health.BaseValue + ")");

            var stamina = actor.Stats.StaminaData;
            var staminaValue = stamina.GetStatValue(stamina);
            Debug.Log("Stamina: " + staminaValue + " (Base: " + stamina.BaseValue + ")");

            var magic = actor.Stats.MagicData;
            var magicValue = magic.GetStatValue(magic);
            Debug.Log("Magic: " + magicValue + " (Base: " + magic.BaseValue + ")");

            var armor = actor.Stats.ActorArmor;
            var armorValue = armor.GetStatValue(armor);
            Debug.Log("Armor: " + armorValue + " (Base: " + armor.BaseValue + ")");
    
            var damage = actor.Stats.ActorDamage;
            var damageValue = damage.GetStatValue(damage);
            Debug.Log("Damage: " + damageValue + " (Base: " + damage.BaseValue + ")");
    
            var level = actor.Stats.ActorLevel;
            var levelValue = level.GetStatValue(level);
            Debug.Log("Level: " + levelValue + " (Base: " + level.BaseValue + ")");
    
            var carryWeight = actor.Stats.ActorWeight;
            var carryWeightValue = carryWeight.GetStatValue(carryWeight);
            Debug.Log("Carry Weight: " + carryWeightValue + " (Base: " + carryWeight.BaseValue + ")");
    
            Debug.Log("----- Equipped Items -----");
            foreach (var item in actor.ActorInventory.GetEquippedItems())
            {
                Debug.Log("Equipped: " + item.name);
                if (item is BaseWeapon weapon)
                {
                    Debug.Log("Weapon Damage: " + weapon.DamageStat.GetStatValue(weapon.DamageStat));
                }
                else if (item is BaseArmor armorItem)
                {
                    Debug.Log("Armor Value: " + armorItem.ArmorStat.GetStatValue(armorItem.ArmorStat));
                }
            }

            Debug.Log("----- End -----");
        }
    }
    
    /// <summary>
    /// Modifies actor stats based on different effects.
    /// </summary>
    public class ActorStatsModifier
    {
        // Current value of an stat will be the base value of the stat +- the modifiers = ( actor skill level +- actor current effects( Temporary effects / Permanent effects ) ) = final stat value

        public void ModifyActorLevel(BaseActor actor, float value)
        {
            actor.Stats.ActorLevel.SetCurrentStatValue(value);
        }
        
        public void ModifyActorHealth(BaseActor actor, float value)
        {
          actor.Stats.HealthData.SetCurrentStatValue(value);
        }

        public void ModifyActorStamina(BaseActor actor, float value)
        {
            actor.Stats.StaminaData.SetCurrentStatValue(value);
        }

        public void ModifyActorMagic(BaseActor actor, float value)
        {
            actor.Stats.StaminaData.SetCurrentStatValue(value);
        }

        public void ModifyActorArmor(BaseActor actor, float value) // Probably the best use of this method is in the "Equip" method of the inventory.
        {
            actor.Stats.ActorArmor.SetCurrentStatValue(value);
        }
        public void ModifyActorDamage(BaseActor actor, float value) // Probably the best use of this method is in the "Equip" method of the inventory.
        {
            actor.Stats.ActorDamage.SetCurrentStatValue(value);
        }

        public void ModifyActorCarryWeight(BaseActor actor, float value)
        {
            actor.Stats.ActorWeight.SetCurrentStatValue(value);
        }

    }

    /// <summary>
    /// Contains various variables and settings associated with an actor.
    /// </summary>
    [Serializable]
    public class ActorVariables
    {
        #region Variables

        // Sprites used for different player states
        [Header("Sprites")] [SerializeField] protected internal Sprite spriteIdle;
        [SerializeField] protected internal Sprite spriteMovingRight;
        [SerializeField] protected internal Sprite spriteMovingLeft;
        [SerializeField] protected internal Sprite spriteJumping;

        // Movement and Physics Variables
        [Header("Values")] [SerializeField, Tooltip("Movement speed of the player.")]
        protected internal float moveSpeed = 12f;

        [SerializeField, Tooltip("Force applied when jumping.")]
        protected internal float jumpForce = 15f;

        [SerializeField, Tooltip("Maximum duration for a jump.")]
        protected internal float maxJumpTime = 0.2f;

        [SerializeField, Tooltip("Multiplier applied to gravity when falling to increase falling speed.")]
        protected internal float fallGravityMultiplier = 6f;

        // Collision-related Variables
        [Header("Collisions")] [SerializeField, Tooltip("The layer mask for detecting ground with raycasts.")]
        protected internal LayerMask groundLayer;

        // Slope detection settings
        [Header("Slope Detection")] [SerializeField, Tooltip("The length of the ground raycasts.")]
        protected internal float groundRaycastLength = 2.5f;

        [SerializeField, Tooltip("The length of the slope raycasts.")]
        protected internal float slopeRaycastLength = 2.2f;

        [SerializeField, Tooltip("The angle at which the slope raycasts are cast.")]
        protected internal float slopeRaycastAngle = 65f;

        // Debug Variables
        [Header("Debug")] [SerializeField, Tooltip("Enable debug visualization for the ground raycast.")]
        protected internal bool showRaycastDebug;

        [SerializeField, Tooltip("If true, activate debug logs for item placement and respawning.")]
        protected internal bool activateDebugLogs;

        // Game Object Components References
        protected internal Rigidbody2D Rb;
        protected internal SpriteRenderer Spr;

        #endregion
    }
    
    /// <summary>
    /// Represents an actor's inventory container, inheriting from the base container class.
    /// </summary>
    [Serializable]
    public class BaseActorContainer : BaseContainer
    {
        public event Action<InventoryChange, BaseItem> OnInventoryChanged; 
        
        public bool InventoryContainsItem(BaseItem item)
        {
            return ItemList.Contains(item);
        }

        public List<BaseItem> GetEquippedItems()
        {
            var equippedItems = new List<BaseItem>();

            foreach (var item in ItemList)
            {
                if (item is BaseEquippable baseEquippable && baseEquippable.isEquipped)
                {
                    equippedItems.Add(item);
                }
            }

            return equippedItems;
        }
        
        public void EquipItem(BaseActor actor, BaseItem item)
        {
            if (InventoryContainsItem(item))
            {
                if (item is BaseWeapon baseWeapon)
                {
                    if (baseWeapon.WeaponData.isEquipable)
                    {
                        baseWeapon.isEquipped = true;
                        if (baseWeapon.isEquipped)
                        {
                            var damageValue = baseWeapon.DamageStat.GetStatValue(baseWeapon.DamageStat);
                            actor.Stats.ActorDamage.SetCurrentStatValue(damageValue);
                        }
                    }
                }
                if (item is BaseArmor baseArmor)
                {
                    if (baseArmor.ArmorData.isEquipable)
                    {
                        baseArmor.isEquipped = true;
                        if (baseArmor.isEquipped)
                        {
                            var armorValue = baseArmor.ArmorStat.GetStatValue(baseArmor.ArmorStat);
                            actor.Stats.ActorArmor.SetCurrentStatValue(armorValue);
                        }
                    }
                }
                OnInventoryChanged?.Invoke(InventoryChange.Equip, item);
            }
        }

        public void UnequipItem(BaseActor actor, BaseItem item)
        {
            if (InventoryContainsItem(item))
            {
                if (item is BaseWeapon baseWeapon)
                {
                    if (baseWeapon.WeaponData.isEquipable)
                    {
                        if (baseWeapon.isEquipped)
                        {
                            var baseValue = actor.Stats.ActorDamage.BaseValue;
                            var damageValue = baseWeapon.DamageStat.GetStatValue(baseWeapon.DamageStat);
                            actor.Stats.ActorDamage.SetCurrentStatValue( baseValue - damageValue);
                        }
                        baseWeapon.isEquipped = false;
                    }
                }
                if (item is BaseArmor baseArmor)
                {
                    if (baseArmor.ArmorData.isEquipable)
                    {
                        if (baseArmor.isEquipped)
                        {
                            var armorValue = baseArmor.ArmorStat.GetStatValue(baseArmor.ArmorStat);
                            var baseValue = actor.Stats.ActorArmor.BaseValue;
                            actor.Stats.ActorArmor.SetCurrentStatValue( baseValue - armorValue);
                        }
                        baseArmor.isEquipped = false;
                    }
                }
                OnInventoryChanged?.Invoke(InventoryChange.Unequip, item);
            }
        }

        public void UseItem(BaseActor actor, BaseItem item) // As for Alpha and Debugging purposes each usable item will add health to the actor
        {
            if (InventoryContainsItem(item))
            {
                if (item is BaseConsumableItem usableItem)
                {
                    if (usableItem.ConsumableData.isUsable)
                    {
                        if (usableItem.ItemCount >= 1)
                        {
                            var itemValue = usableItem.ConsumableStat.GetStatValue(usableItem.ConsumableStat);
                            var actorHealth = actor.Stats.HealthData;
                            actorHealth.SetCurrentStatValue(itemValue);
                        }
                        RemoveItem(item);
                    }
                }

                if (item is BaseWeapon baseWeapon)
                {
                    if (baseWeapon.isEquipped)
                    {
                        // Logic when using weapons increase weapon skill per example
                    }
                    // 
                }
                
                OnInventoryChanged?.Invoke(InventoryChange.Use, item);
            }
        }
        
        public enum InventoryChange
        {
            Equip,
            Unequip,
            Use
        }
        
        public class  ActorUtility
        {
            public static BaseActor GetActorFromRaycast(Vector2 position)
            {
                RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
                if (hit.collider != null)
                {
                    BaseActor actor = hit.collider.GetComponent<BaseActor>();
                    return actor;
                }
                return null;
            }
        }
    }
}