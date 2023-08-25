using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.BaseClasses
{
    [Serializable]
    public class BaseContainer
    {
        public event Action<ContainerChange, BaseItem> OnContainerChanged;
        
        [SerializeField] private List<BaseItem> itemList = new List<BaseItem>();

        public List<BaseItem> ItemList
        {
            get => itemList;
            set => itemList = value;
        }

        // Create methods to manage the list as for add items, manage items, get the items in the list

        public void AddItem(BaseItem item) // Method to add items to the inventory / Container
        {
            // Add items to the actor list
            ItemList.Add(item);
            item.ItemCount ++;
            OnContainerChanged?.Invoke(ContainerChange.Add, item);
            
        }

        public void RemoveItem(BaseItem item) // Method to "drop" items
        {
            if (item.ItemCount > 0)
            {
                item.ItemCount --;
                Debug.Log(item +" Item not removed as for item count: " + item.ItemCount );
            }
            if (item.ItemCount == 0)
            {
                ItemList.Remove(item);
                Debug.Log(item +" Removed as for item count: " + item.ItemCount );
            }
            OnContainerChanged?.Invoke(ContainerChange.Remove, item);
        }
        
        public enum ContainerChange
        {
            Add,
            Remove
        }
    }
}