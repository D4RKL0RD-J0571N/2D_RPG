// using System.Collections.Generic;
// using Data.BaseClasses;
// using Data.BaseClasses.Scriptable;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace Misc.UI
// {
//     public class ContainerDisplay : BaseEntity
//     {
//             [SerializeField] private GameObject itemPrefab; // Prefab to represent the item
//
//             [SerializeField] private Transform uiPanel; // The canvas of the UI
//
//         [SerializeField] private BaseActor player;
//
//         private Dictionary<BaseItem, GameObject> _itemUiElements = new Dictionary<BaseItem, GameObject>();
//
//         #region Variables
//
//         private bool _openInventory;
//
//         #endregion
//         
//
//         private void Awake()
//         {
//             player.ActorInventory.OnContainerChanged += HandleEntityContainer;
//             player.ActorInventory.OnInventoryChanged += HandleActorInventory;
//             UiInput.Instance.OnInventoryButtonPress += ToggleInventory;
//             
//         }
//         
//         private void OnDestroy()
//         {
//             player.ActorInventory.OnContainerChanged -= HandleEntityContainer;
//             player.ActorInventory.OnInventoryChanged -= HandleActorInventory;
//             UiInput.Instance.OnInventoryButtonPress += ToggleInventory;
//         }
//
//         private void ToggleInventory(bool isOpen)
//         {
//             if (isOpen)
//             {
//                 HandlePlayerInventory();
//
//             }
//         }
//         
//         // Debug method to access the player inventory, for debugging only
//         private void HandlePlayerInventory() 
//         {   // We check the item list of each entity, and we iterate trough the list, we check if the item is already in
//             // the dictionary and we add the item this way we could handle leveled list or hard coded filled inventories 
//             foreach (BaseItem item in player.ActorInventory.ItemList)
//             {
//                 HandleAddItem(item);
//             }
//         }
//         
//         // We need to create a method to access any entity container without needing to drag and drop manually the entity in to a serialized field
//         private void HandleActorLeveledItemList(BaseActor actor) 
//         {   // We check the item list of each entity, and we iterate trough the list, we check if the item is already in
//             // the dictionary and we add the item this way we could handle leveled list or hard coded filled inventories 
//             foreach (BaseItem item in actor.ActorInventory.ItemList)
//             {
//                 HandleAddItem(item);
//             }
//         }
//         
//         
//         private void HandleEntityContainer(BaseContainer.ContainerChange changeType, BaseItem item)
//         {
//             switch (changeType)
//             {
//                 case BaseContainer.ContainerChange.Add:
//                     // We add the prefab with the data needed
//                     HandleAddItem(item);
//                     break;
//                 case BaseContainer.ContainerChange.Remove:
//                     // We remove the prefab
//                     HandleRemoveItem(item);
//                     break;  
//             }
//         }
//
//         private void HandleAddItem(BaseItem item)
//         {
//             if (!_itemUiElements.ContainsKey(item))
//             {
//                 GameObject newItemUI = CreateItemUI(item);
//                 _itemUiElements.Add(item, newItemUI);
//                 Debug.Log("Adding New item: " + item);
//             }
//             else
//             {
//                 UpdateItem(_itemUiElements[item], item);
//                 Debug.Log("Updating Item");
//             }
//         }
//
//         private void HandleRemoveItem(BaseItem item)
//         {
//             if (_itemUiElements.ContainsKey(item))
//             {
//                 GameObject existingItemUI = _itemUiElements[item];
//                 if (item.ItemCount == 0)
//                 {
//                     existingItemUI.SetActive(false);
//                     _itemUiElements.Remove(item);
//                 }
//                 else
//                 {
//                     // Update the item in the UI
//                     UpdateItem(existingItemUI, item);
//                 }
//                 Debug.Log("Removing Item: " + item);
//             }
//         }
//         
//         private void HandleActorInventory(BaseActorContainer.InventoryChange changeType, BaseItem item)
//         {
//             switch (changeType)
//             {
//                 case BaseActorContainer.InventoryChange.Equip:
//                     // We highlight the item using some kind of arrow to mark it from non-equipped items, we need a method to change the button to unequip instead of equip.
//                     Debug.Log("Equipping item: " + item);
//                     break;
//                 case BaseActorContainer.InventoryChange.Unequip:
//                     // We unhighlight the item hiding the arrow to unmark it from equipped items, as well we need a method to change the button to equip instead of unequip.
//                     Debug.Log("Unequipping item: " + item);
//                     break;
//                 case BaseActorContainer.InventoryChange.Use:
//                     // As well we need a method to change the button to use instead of the other options.
//                     Debug.Log("Using item: " + item);
//                     break;
//             }
//         }
//
//         private GameObject CreateItemUI(BaseItem item)
//         {
//             GameObject itemUI = Instantiate(itemPrefab, uiPanel);
//             itemUI.SetActive(true);
//             
//             ScriptableItem itemInfo = GetItemInfo(item);
//             SetUIValues(itemUI, itemInfo, item);
//             
//             Debug.Log($"Creating UI Item for: {itemInfo.fullName}");
//             return itemUI;
//         }
//
//         private ScriptableItem GetItemInfo(BaseItem item)
//         {
//             if (item is BaseWeapon baseWeapon)
//                 return baseWeapon.WeaponData;
//             if (item is BaseArmor baseArmor)
//                 return baseArmor.ArmorData;
//             if (item is BaseConsumableItem baseConsumableItem)
//                 return baseConsumableItem.ConsumableData;
//             return null;
//         }
//
//         private void SetUIValues(GameObject itemUI, ScriptableItem itemInfo, BaseItem item)
//         {
//             Image itemIcon = itemUI.GetComponentInChildren<Image>();
//             TextMeshProUGUI[] textComponents = itemUI.GetComponentsInChildren<TextMeshProUGUI>();
//
//             itemIcon.sprite = itemInfo.icon;
//             textComponents[0].text = $"{itemInfo.fullName} ({item.ItemCount})";
//             textComponents[1].text = itemInfo.entityType.ToString();
//             textComponents[2].text = "Other Info";
//         }
//          
//          private void UpdateItem(GameObject itemUI, BaseItem item)
//          {
//              SetUIValues(itemUI, GetItemInfo(item), item);
//          }
//
//          private void HandleItemCategory(ItemCategory category)
//          {
//              foreach (var itemUiPair in _itemUiElements)
//              {
//                  bool shouldShow = false;
//                  switch (category)
//                  {
//                      case ItemCategory.All:
//                          shouldShow = true;
//                          break;
//                      case ItemCategory.Weapons:
//                          shouldShow = itemUiPair.Key is BaseWeapon;
//                          break;
//                      case ItemCategory.Armors:
//                          shouldShow = itemUiPair.Key is BaseArmor;
//                          break;
//                      case ItemCategory.Consumables:
//                          shouldShow = itemUiPair.Key is BaseConsumableItem;
//                          break;
//                      // Others like books, potions, books, scrolls, food, ingredients, misc
//                  }
//                  itemUiPair.Value.SetActive(shouldShow);
//              }
//          }
//          
//          private enum ItemCategory
//          {
//              All,
//              Weapons,
//              Armors,
//              Consumables,
//              //Others
//          }
//          
//          
//          
//          
//          
//          
//          
//          
//          
//          
//          
//          
//          
//          
//          
//          
//          
//          
//     }
// }