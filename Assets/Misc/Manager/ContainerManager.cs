using Data.Actors.Player;
using Data.BaseClasses;
using Misc.UI;
using UnityEngine;

namespace Misc.Manager
{
    public class ContainerManager : MonoBehaviour
    {
        [SerializeField] private BaseActor player;
        
        
        private void Awake()
        {
            Player.Instance.ActorInventory.OnContainerChanged += HandleEntityContainer;
            Player.Instance.ActorInventory.OnInventoryChanged += HandleActorInventory;
            
            
            ObjectDetector.Instance.OnEntityInteract += HandleEntityContainer;
            
            // Input
            UserInterfaceInput.Instance.OnInventoryButtonPress += ToggleInventory;
            
        }
        
        private void OnDestroy()
        {
            player.ActorInventory.OnContainerChanged -= HandleEntityContainer;
            player.ActorInventory.OnInventoryChanged -= HandleActorInventory;
            UserInterfaceInput.Instance.OnInventoryButtonPress += ToggleInventory;
        }
        
        
        
        private void ToggleInventory(bool isOpen)
        {
            if (isOpen)
            {
                HandlePlayerInventory();

            }
        }
        
        // Debug method to access the player inventory, for debugging purposes only
        private void HandlePlayerInventory() 
        {   
            foreach (BaseItem item in player.ActorInventory.ItemList)
            {
                UIContainerManager.Instance.HandleAddItem(item);
            }
        }
        
        
        // We check the item list of each entity, and we iterate trough the list, we check if the item is already in
        // the dictionary and we add the item this way we could handle leveled list or hard coded filled inventories 
        
        private void HandleEntityContainer(BaseEntity entity) // Any derived entity that inherit the ItemList
        {   
            if (entity is BaseActor actor)
            {
                foreach (BaseItem item in actor.ActorInventory.ItemList)
                {
                    UIContainerManager.Instance.HandleAddItem(item);
                }
            }
            // if (entity is OtherTypeOfEntity){}
        }
        
        
        private void HandleEntityContainer(BaseContainer.ContainerChange changeType, BaseItem item)
        {
            switch (changeType)
            {
                case BaseContainer.ContainerChange.Add:
                    // We add the prefab with the data needed
                    UIContainerManager.Instance.HandleAddItem(item);
                    break;
                case BaseContainer.ContainerChange.Remove:
                    // We remove the prefab
                    UIContainerManager.Instance.HandleRemoveItem(item);
                    break;  
            }
        }
        
        private void HandleActorInventory(BaseActorContainer.InventoryChange changeType, BaseItem item)
        {
            switch (changeType)
            {
                case BaseActorContainer.InventoryChange.Equip:
                    // We highlight the item using some kind of arrow to mark it from non-equipped items, we need a method to change the button to unequip instead of equip.
                    Debug.Log("Equipping item: " + item);
                    break;
                case BaseActorContainer.InventoryChange.Unequip:
                    // We unhighlight the item hiding the arrow to unmark it from equipped items, as well we need a method to change the button to equip instead of unequip.
                    Debug.Log("Unequipping item: " + item);
                    break;
                case BaseActorContainer.InventoryChange.Use:
                    // As well we need a method to change the button to use instead of the other options.
                    Debug.Log("Using item: " + item);
                    break;
            }
        }
    }
}