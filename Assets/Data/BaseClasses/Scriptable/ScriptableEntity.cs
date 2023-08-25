using Misc;
using Misc.CustomEditors;
using UnityEngine;

namespace Data.BaseClasses.Scriptable
{
    public class ScriptableEntity : ScriptableObject
    {
        [SerializeField]
        protected internal RecordType entityType;
        [SerializeField]
        protected internal Hex formID;
        
        
        private void OnValidate()
        {
            if (formID != Hex.EmptyHex)
            {
                if (EntityEditor.FormIDMap.TryGetValue(formID, out var existingEntity) && existingEntity != this)
                {
                    Debug.LogWarning($"FormID conflict detected. Existing entity: {existingEntity.name}");
                }
                else
                {
                    EntityEditor.FormIDMap[formID] = this;
                }
            }
        }
        

        
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    #region Enums Flags
    /// <summary>
    /// Enum of records to flag the type of data
    /// </summary>
    public enum RecordType
    {
        ActionsAndStates, // AACT - Actions - States
        Activator,        // ACTI - Activators - Entities that can be triggered like an NPC or other actors
        AddonNode,        // ADDN - Addon node - Entities that may have child objects like static objects or non-interactable objects, like the smoke of a chimney
        Ammunition,       // AMMO - Ammunition - Ammunition for guns, bows, crossbows, and other weapons that use ammo Item
        AnimatedObject,   // ANIO - Animated Object - Objects that can be animated, maybe by action or trigger
        ArmatureAddon,    // ARMA - Armature Addons - Representation in the body; may not be necessary
        Armor,            // ARMO - Armor - One entry for each armor type Item
        AssociationTypes, // ASTP - Association Types - Flags for entity types of relationships, such as family, boss to employee, business partners, favor targets, conspirators, and others.
        ActorValueInfo,   // AVIF - Actor Value Information - The different types of stats an actor can have.
        Book,             // BOOK - Book - Books in the game Item
        Cell,             // CELL - Cell - World Cell Location
        Class,            // CLAS - Class - Character Classes
        Climate,          // CLMT - Climate - Holds the different weather types
        Container,        // CONT - Container - Containers / Storages
        CombatStyle,      // CSTY - Combat Style - Holds each type of possible combat style for each possible actor
        DialogTopic,      // DIAL - Dialog Topic - Groups the dialogues
        DialogBranch,     // DLRB - Dialog Branches - Each branch of each dialogue
        DialogViewer,     // DLVW - Dialog Viewer - Representation of dialogue options
        DefaultObjectManager, // DOBJ - Default Object Manager - Manages default objects
        Door,             // DOOR - Door - Doors
        EncounterZone,    // ECZN - Encounter Zones - Stores all the zones of locations
        Enchantment,      // ENCH - Object Effect - Enchantments in objects
        EffectShader,     // EFSH - Effect Shader - Shaders of effects
        EquipType,        // EQUP - Equip Type - Stores all possible equip types; for example, flags items as "Can be equipped - right hand"
        Explosion,        // EXPL - Explosion - Explosions in the game
        Faction,          // FACT - Faction - Factions in the game
        Flora,            // FLOR - Flora - For things like mushrooms, animals, birds, trees, chicken nests; may be for placeables Item
        FormIDList,       // FLST - FormID List - Groups entities of the same class to relate entities to others
        Food,             // FOOD - Food - Ingestible items
        Global,           // GLOB - Global - Global data
        DialogueInfo,     // INFO - Dialogue Info - Dialogue-related information
        Ingredient,       // INGR - Ingredient - Ingredients in the game Item
        Key,              // KEYM - Key - As for game objects keys Item
        Keywords,         // KYWD - Keywords -
        Location,         // LCTN - Location
        Light,            // LIGH - Light - Light entities
        LeveledItem,      // LVLI - Leveled Item - Leveled list entries for different versions of the same item that can be found in the world, including in containers and NPC inventories
        LeveledNpc,       // LVLN - Leveled NPC - Leveled list entries for different versions of the same NPC that can be found in the world, such as generic NPCs
        LeveledSpell,     // LVSP - Leveled Spell - Leveled list entries for different versions of items that are enchanted with the same spell
        MaterialType,     // MATT - Material Type - Materials Item
        Message,          // MESG - Messages - UI texts, such as "Press space bar to jump"
        MagicEffect,      // MGEF - Magic Effect - Magic effects; the default could be "scriptEffect" with 0, null, and none values
        MovementType,     // MOVT - Movement Type - The movement type of each entity
        MovableStatic,    // MSTT - Movable Static - Static but movable game objects, like banners or some special effects
        Miscellaneous,    // MISC - Miscellaneous Items - Items like lockpicks torches and other non equipables resources or items like baskets or static items
        NonPlayerCharacter, // NPC_ - Non-Player Character - Actors that the player may interact with or control
        Outfit,           // OTFT - Outfit - Non-armor equippables Item
        Packages,         // PACK - Packages - AI behaviors for each entity, from greetings to the player to reactions to player actions, eating, sitting, sleeping, fleeing from location, following, activating, etc.
        Perk,             // PERK - Perk - Abilities; each single perk from a tree of abilities
        Projectile,       // PROJ - Projectile - Projectiles, such as the action of throwing fireballs or arrows // Do not confuse it with ammo
        QuestRecord,      // QUST - Quest Record - Quest-related data
        Race,             // RACE - Race - Differentiates each kind of living being, like dogRace, elkRace, bearRace, defaultRace with null and 0 values, and many more
        Region,           // REGN - Region - Each region in a world space
        Relationship,     // RELA - Relationship - The relationship of each entity with the player
        VisualEffect,     // RFCT - Visual Effect - Each visual effect in the game
        Scene,            // SCEN - Scene - Each scene in a quest or similar; describes what happens in each state of the scene
        Scroll,           // SCRL - Scroll - Each scroll in the game Item
        StoryManagerBranchNode, // SMBN - Story Manager Branch Node - Manages branches of the game's story
        StoryManagerEventNode,  // SMEN - Story Manager Event Node - Manages events in the game's story
        StoryManagerQuestNode,  // SMQN - Story Manager Quest Node - Manages quests in the game's story
        Sound,            // SOUN - Sound - Sound entities
        Spell,            // SPEL - Spell - Usable magic spells, such as fireballs and other activatable powers like invisibility and abilities
        Static,           // STAT - Static - Static entities like a house, world markers, and other immovable entities
        Tree,             // TREE - Tree - Trees, bushes, and other plant-related entities
        TextureSet,       // TXST - Texture Set - Textures
        Weapon,           // WEAP - Weapon - Weapons in the game Items
        WorldSpace        // WRLD - World Space - Parent of locations; world spaces can be in other world spaces
    }
    #endregion
}