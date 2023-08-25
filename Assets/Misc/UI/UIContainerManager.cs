 using System.Collections.Generic;
 using Data.BaseClasses;
 using Data.BaseClasses.Scriptable;
 using TMPro;
 using UnityEngine;
 using UnityEngine.UI;

 namespace Misc.UI
{
    public class UIContainerManager : MonoBehaviour
    {
        public static UIContainerManager Instance { get; set; }
        
        [SerializeField] private GameObject itemPrefab; // Prefab to represent the item

        [SerializeField] private Transform uiPanel; // The canvas of the UI
        
        private Dictionary<BaseItem, GameObject> _itemUiElements = new Dictionary<BaseItem, GameObject>();
        
        public void HandleAddItem(BaseItem item)
        {
            if (!_itemUiElements.ContainsKey(item))
            {
                GameObject newItemUI = CreateItemUI(item);
                _itemUiElements.Add(item, newItemUI);
                Debug.Log("Adding New item: " + item);
            }
            else
            {
                UpdateItem(_itemUiElements[item], item);
                Debug.Log("Updating Item");
            }
        }

        public void HandleRemoveItem(BaseItem item)
        {
            if (_itemUiElements.ContainsKey(item))
            {
                GameObject existingItemUI = _itemUiElements[item];
                if (item.ItemCount == 0)
                {
                    existingItemUI.SetActive(false);
                    _itemUiElements.Remove(item);
                }
                else
                {
                    // Update the item in the UI
                    UpdateItem(existingItemUI, item);
                }
                Debug.Log("Removing Item: " + item);
            }
        }
        
        private GameObject CreateItemUI(BaseItem item)
        {
            GameObject itemUI = Instantiate(itemPrefab, uiPanel);
            itemUI.SetActive(true);
            
            ScriptableItem itemInfo = GetItemInfo(item);
            SetUIValues(itemUI, itemInfo, item);
            
            Debug.Log($"Creating UI Item for: {itemInfo.fullName}");
            return itemUI;
        }
        
        private void UpdateItem(GameObject itemUI, BaseItem item)
        {
            SetUIValues(itemUI, GetItemInfo(item), item);
        }
        
        private void SetUIValues(GameObject itemUI, ScriptableItem itemInfo, BaseItem item)
        {
            Image itemIcon = itemUI.GetComponentInChildren<Image>();
            TextMeshProUGUI[] textComponents = itemUI.GetComponentsInChildren<TextMeshProUGUI>();

            itemIcon.sprite = itemInfo.icon;
            textComponents[0].text = $"{itemInfo.fullName} ({item.ItemCount})";
            textComponents[1].text = itemInfo.entityType.ToString();
            textComponents[2].text = "Other Info";
        }
        
        private ScriptableItem GetItemInfo(BaseItem item)
        {
            if (item is BaseWeapon baseWeapon)
                return baseWeapon.WeaponData;
            if (item is BaseArmor baseArmor)
                return baseArmor.ArmorData;
            if (item is BaseConsumableItem baseConsumableItem)
                return baseConsumableItem.ConsumableData;
            return null;
        }
        
        
        private void HandleItemCategory(ItemCategory category)
        {
            foreach (var itemUiPair in _itemUiElements)
            {
                bool shouldShow = false;
                switch (category)
                {
                    case ItemCategory.All:
                        shouldShow = true;
                        break;
                    case ItemCategory.Weapons:
                        shouldShow = itemUiPair.Key is BaseWeapon;
                        break;
                    case ItemCategory.Armors:
                        shouldShow = itemUiPair.Key is BaseArmor;
                        break;
                    case ItemCategory.Consumables:
                        shouldShow = itemUiPair.Key is BaseConsumableItem;
                        break;
                    // Others like books, potions, books, scrolls, food, ingredients, misc
                }
                itemUiPair.Value.SetActive(shouldShow);
            }
        }
         
        private enum ItemCategory
        {
            All,
            Weapons,
            Armors,
            Consumables,
            //Others
        }
    }
}