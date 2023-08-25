using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc.Manager.Items
{
    /// <summary>
    /// Represents the item collector that collects items when the player collides with them.
    /// </summary>
    public class ItemCollector : MonoBehaviour
    {
        /// <summary>
        /// Gets the singleton instance of the ItemCollector.
        /// </summary>
        public static ItemCollector Instance { get; private set; }

        [SerializeField]
        private ItemDisplay itemDisplay; // Reference to the ItemDisplay script

        [Header("Debug Item Collection")]
        [SerializeField, Tooltip("If true, destroy the item instead of returning it to the object pool.")]
        private bool destroyInstead;

        [SerializeField, Tooltip("If true, activate debug logs for item collection.")]
        private bool activateDebugLogs;

        [SerializeField, Tooltip("List of tags to search for when collecting items.")]
        private List<string> itemTags = new List<string> { "Items" };

        private int _quantity = 1;

        private ObjectPool _objectPool; // Reference to the Object Pool script

        private Dictionary<GameObject, bool> _itemTriggerIndicators = new Dictionary<GameObject, bool>();
    
        // Event that will be triggered when an item is collected.
        public event Action<GameObject> OnItemCollected;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _objectPool = ObjectPool.Instance; // Get a reference to the Object Pool instance
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (itemTags.Contains(collision.gameObject.tag))
            {
                if (activateDebugLogs)
                {
                    Debug.Log("Item collision: " + collision.gameObject.name);
                }

                if (CollectItem(collision.gameObject))
                {
                    SetItemTriggerIndicator(collision.gameObject, true);

                    if (activateDebugLogs)
                    {
                        Debug.Log("Item collected: " + collision.gameObject.name);
                    }

                    // Item was collected
                    // Update the ItemPlacer dictionary to remove the collected item
                    SpawnManager.Instance.RemoveItem(collision.gameObject);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (itemTags.Contains(collision.gameObject.tag))
            {
                if (IsItemTriggered(collision.gameObject))
                {
                    SetItemTriggerIndicator(collision.gameObject, false);
                }
            }
        }

        /// <summary>
        /// Collects the specified item, either by returning it to the object pool or destroying it.
        /// </summary>
        /// <param name="item">The item GameObject to collect.</param>
        /// <returns>Returns true if the item was collected successfully.</returns>
        public bool CollectItem(GameObject item)
        {
            if (!destroyInstead)
            {
                if (activateDebugLogs)
                {
                    Debug.Log("Item Collected: " + item.name);
                }

                if (IsItemTriggered(item))
                {
                    // The item was already collected before, update its quantity
                    _quantity++;
                }

                if (itemDisplay.DisplayTimer <= 0)
                {
                    _quantity = 1;
                }

                // Display the item info using the ItemDisplay script
                itemDisplay.ShowItemInfo(item.name, _quantity); // Assuming "quantity" is the variable representing the item quantity
                _objectPool.ReturnPooledObject(item, item.tag);
            
                // Trigger the OnItemCollected event.
                OnItemCollected?.Invoke(item);

                return true; // Item was collected
            }

            if (activateDebugLogs)
            {
                Debug.Log("Item Destroyed: " + item.name);
            }

            // Display the item info using the ItemDisplay script
            itemDisplay.ShowItemInfo(item.name, _quantity); // Assuming "quantity" is the variable representing the item quantity
            Destroy(item); // Destroy the item instead of returning it to the object pool
            return true; // Item was collected
        }

        /// <summary>
        /// Checks if the specified item has been triggered.
        /// </summary>
        /// <param name="item">The item GameObject to check.</param>
        /// <returns>Returns true if the item has been triggered.</returns>
        public bool IsItemTriggered(GameObject item)
        {
            if (_itemTriggerIndicators.TryGetValue(item, out var triggered))
            {
                return triggered;
            }
            return false;
        }

        /// <summary>
        /// Sets the trigger indicator for the specified item.
        /// </summary>
        /// <param name="item">The item GameObject to set the trigger indicator for.</param>
        /// <param name="value">The value to set for the trigger indicator.</param>
        public void SetItemTriggerIndicator(GameObject item, bool value)
        {
            _itemTriggerIndicators[item] = value;
        }
    }
}
