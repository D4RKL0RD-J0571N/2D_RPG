using System;
using System.Collections.Generic;
using UnityEngine;
namespace Misc.Manager.Items
{
    public class SpawnManager : MonoBehaviour
    {
        [Serializable]
        public class ItemSpawnData
        {
            private Dictionary<Transform, int> lastUsedSpawnIndices = new Dictionary<Transform, int>();
            public GameObject itemPrefab;
            public List<Transform> spawnPoints;
            protected internal List<GameObject> activeItems;
            public bool shouldRespawn; // New property to indicate if the item should respawn
            // New property to track the last used spawn index for this ItemSpawnData
            private int lastUsedSpawnIndex;
            
            
            
            public int GetNextSpawnIndex()
            {
                lastUsedSpawnIndex = (lastUsedSpawnIndex + 1) % spawnPoints.Count;
                return lastUsedSpawnIndex;
            }
            
            public ItemSpawnData(GameObject prefab, List<Transform> points, bool respawn)
            {
                itemPrefab = prefab;
                spawnPoints = points;
                shouldRespawn = respawn;
                activeItems = new List<GameObject>();
                lastUsedSpawnIndex = 0;

                // foreach (Transform spawnPoint in spawnPoints)
                // {
                //     lastUsedSpawnIndices.Add(spawnPoint, 0);
                // }
            }
        }
        

        [SerializeField]
        private List<ItemSpawnData> itemSpawnDataList;

        public float respawnDelay;

        private float _respawnTimer;
        
        private int[] lastUsedSpawnIndex; // Keep track of the last used spawn index for each ItemSpawnData


        public static SpawnManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            if (itemSpawnDataList == null)
            {
                Debug.LogError("ItemSpawnDataList is not assigned in the inspector.");
                return;
            }

            // Initialize the active item lists in each ItemSpawnData instance
            foreach (var spawnData in itemSpawnDataList)
            {
                spawnData.activeItems = new List<GameObject>();
            }

            // Initialize lastUsedSpawnIndex with default values of 0
            lastUsedSpawnIndex = new int[itemSpawnDataList.Count];
            for (int i = 0; i < lastUsedSpawnIndex.Length; i++)
            {
                lastUsedSpawnIndex[i] = 0;
            }

            SpawnItems();
        }

        private void Update()
        {
            _respawnTimer -= Time.deltaTime;
            if (_respawnTimer <= 0)
            {
                RespawnItems();
                _respawnTimer = respawnDelay;
            }
        }

        /// <summary>
        /// Spawns items on the predefined spawn points.
        /// </summary>
        private void SpawnItems()
        {
            foreach (var spawnData in itemSpawnDataList)
            {
                List<Transform> spawnPoints = spawnData.spawnPoints;
                GameObject itemPrefab = spawnData.itemPrefab;

                if (spawnPoints.Count == 0)
                {
                    Debug.LogWarning("No spawn points defined for item: " + itemPrefab.name);
                    continue;
                }

                int activeItemCount = GetActiveItemCount(spawnData);

                // Handle the case of the first item separately
                if (activeItemCount == 0)
                {
                    PlaceItem(spawnData);
                    activeItemCount++;
                }

                while (activeItemCount < spawnPoints.Count)
                {
                    PlaceItem(spawnData);
                    activeItemCount++;
                }
            }
        }

        
        /// <summary>
        /// Places an item on an available spawn point.
        /// </summary>
        /// <param name="spawnData">The ItemSpawnData containing the item prefab and spawn points.</param>
        private void PlaceItem(ItemSpawnData spawnData)
        {
            List<Transform> spawnPoints = spawnData.spawnPoints;
            GameObject itemPrefab = spawnData.itemPrefab;

            if (spawnData == null || spawnData.spawnPoints == null)
            {
                Debug.LogWarning("Invalid ItemSpawnData or spawn points.");
                return;
            }
            if (spawnPoints.Count == 0)
            {
                Debug.LogWarning("No spawn points defined for item: " + itemPrefab.name);
                return;
            }
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                // int index = (lastUsedSpawnIndex[itemSpawnDataList.IndexOf(spawnData)] + i) % spawnPoints.Count;
                // Transform spawnPoint = spawnPoints[index];
                
                int nextSpawnIndex = spawnData.GetNextSpawnIndex();
                Transform spawnPoint = spawnPoints[nextSpawnIndex];

                if (!IsSpawnPointOccupied(spawnPoint))
                {
                    Debug.Log($"Placing {itemPrefab.name} at spawn point index {nextSpawnIndex}");
                    
                    // Debug log to show available (free) spawn points
                    string freeSpawnPoints = string.Join(", ", GetFreeSpawnPointIndices(spawnPoints));
                    Debug.Log($"Available spawn points: {freeSpawnPoints}");
                    
                    // The spawn point is available, so use it to place the item
                    GameObject item = ObjectPool.Instance.GetPooledObject(itemPrefab);
                    if (item == null)
                    {
                        Debug.LogWarning("Item prefab not found in the object pool: " + itemPrefab.name);
                        return;
                    }

                    item.transform.position = spawnPoint.position;
                    item.SetActive(true);

                    lastUsedSpawnIndex[itemSpawnDataList.IndexOf(spawnData)] = nextSpawnIndex; // Update the last used spawn index

                    // Add the item to the activeItems list in the corresponding ItemSpawnData
                    spawnData.activeItems.Add(item);
                    return;
                }
            }
            // for (int i = 0; i < spawnPoints.Count; i++)
            // {
            //     int index = (lastUsedSpawnIndex[itemSpawnDataList.IndexOf(spawnData)] + i) % spawnPoints.Count;
            //     Transform spawnPoint = spawnPoints[index];
            //
            //     if (!IsSpawnPointOccupied(spawnPoint))
            //     {
            //         lastUsedSpawnIndex[itemSpawnDataList.IndexOf(spawnData)] = index;
            //         // The spawn point is available, so use it to place the item
            //         GameObject item = ObjectPool.Instance.GetPooledObject(itemPrefab);
            //         if (item == null)
            //         {
            //             Debug.LogWarning("Item prefab not found in the object pool: " + itemPrefab.name);
            //             return;
            //         }
            //
            //         item.transform.position = spawnPoint.position;
            //         item.SetActive(true);
            //
            //         // Add the item to the activeItems list in the corresponding ItemSpawnData
            //         spawnData.activeItems.Add(item);
            //         return;
            //     }
            // }
            
            // No available spawn point for item
            Debug.LogWarning("No available spawn point for item: " + itemPrefab.name);
        }

        /// <summary>
        /// Removes the given item from the activeItems list in the corresponding ItemSpawnData.
        /// </summary>
        /// <param name="item">The item to remove from the activeItems list.</param>
        public void RemoveItem(GameObject item)
        {
            if (item == null)
            {
                Debug.LogWarning("Invalid item (null).");
                return;
            }
            
            foreach (var spawnData in itemSpawnDataList)
            {
                if (spawnData.activeItems.Contains(item))
                {
                    spawnData.activeItems.Remove(item);
                    break;
                }
            }
        }

        /// <summary>
        /// Respawns items on the predefined spawn points.
        /// </summary>
        private void RespawnItems()
        {
            foreach (var spawnData in itemSpawnDataList)
            {
                if (!spawnData.shouldRespawn)
                    continue; // Skip this spawn data if the item should not respawn
                if (spawnData == null || spawnData.spawnPoints == null)
                {
                    Debug.LogWarning("Invalid ItemSpawnData or spawn points.");
                    continue;
                }
                List<Transform> spawnPoints = spawnData.spawnPoints;
                int activeItemCount = GetActiveItemCount(spawnData);
                if (spawnPoints.Count > activeItemCount)
                {
                    PlaceItem(spawnData);
                }
                // No available spawn point for item - Here was an "else" with a continue statement.
            }
        }

        /// <summary>
        /// Gets the number of active items in the given ItemSpawnData.
        /// </summary>
        /// <param name="spawnData">The ItemSpawnData to check.</param>
        /// <returns>The number of active items in the ItemSpawnData.</returns>
        private int GetActiveItemCount(ItemSpawnData spawnData)
        {
            return spawnData.activeItems.Count;
        }

        /// <summary>
        /// Checks if the spawn point is currently occupied by any active item.
        /// </summary>
        /// <param name="spawnPoint">The spawn point to check.</param>
        /// <returns>True if the spawn point is occupied, otherwise false.</returns>
        private bool IsSpawnPointOccupied(Transform spawnPoint)
        {
            if (spawnPoint == null)
            {
                Debug.LogWarning("Invalid spawn point (null).");
                return false;
            }
    
            foreach (var spawnData in itemSpawnDataList)
            {
                foreach (var item in spawnData.activeItems)
                {
                    if (item.activeSelf && Vector3.Distance(item.transform.position, spawnPoint.position) < 0.1f) // Adjust the threshold as needed
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private List<int> GetFreeSpawnPointIndices(List<Transform> spawnPoints)
        {
            List<int> freeIndices = new List<int>();
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                if (!IsSpawnPointOccupied(spawnPoints[i]))
                {
                    freeIndices.Add(i);
                }
            }
            return freeIndices;
        }
        
        
        
        /// <summary>
        /// Gets an available spawn point from the list of spawn points.
        /// </summary>
        /// <param name="spawnPoints">The list of spawn points to check.</param>
        /// <returns>An available spawn point if found, otherwise null.</returns>
        private Transform GetAvailableSpawnPoint(List<Transform> spawnPoints)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                if (!IsSpawnPointOccupied(spawnPoint))
                {
                    return spawnPoint;
                }
            }
            return null;
        }
    }
}
