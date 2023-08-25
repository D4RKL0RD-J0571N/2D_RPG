using System.Collections.Generic;
using UnityEngine;

namespace Misc.Manager
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField, Tooltip("The prefabs of the items to pool.")]
        private GameObject[] itemPrefabs;

        [SerializeField, Tooltip("The initial number of items to pool.")]
        private int poolSize = 20;

        [Header("Debug Object Pool")]
        [SerializeField, Tooltip("If true, activate debug logs for object pooling.")]
        private bool activateDebugLogs;

        // private List<GameObject>[] _pooledObjects; // Changed to an array of lists

        public static ObjectPool Instance;
        private Dictionary<Hex, List<GameObject>> _pooledObjects = new Dictionary<Hex, List<GameObject>>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeObjectPool();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Initializes the object pool with the specified number of pooled objects for each item prefab.
        /// </summary>
        private void InitializeObjectPool()
        {
            _pooledObjects = new Dictionary<Hex, List<GameObject>>(); // Initialize array

            for (int i = 0; i < itemPrefabs.Length; i++)
            {
                _pooledObjects[i] = new List<GameObject>(); // Initialize each list

                for (int j = 0; j < poolSize; j++)
                {
                    GameObject obj = Instantiate(itemPrefabs[i]);
                    obj.SetActive(false);
                    _pooledObjects[i].Add(obj);
                }
            }

            if (activateDebugLogs)
            {
                Debug.Log("Object Pool initialized with " + poolSize + " objects per item.");
            }
        }

        /// <summary>
        /// Retrieves a pooled object of the specified item prefab.
        /// </summary>
        /// <param name="itemPrefab">The item prefab to retrieve from the object pool.</param>
        /// <returns>The pooled object if available; otherwise, null.</returns>
        public GameObject GetPooledObject(GameObject itemPrefab)
        {
            for (int i = 0; i < itemPrefabs.Length; i++)
            {
                if (itemPrefabs[i] == itemPrefab)
                {
                    for (int j = 0; j < _pooledObjects[i].Count; j++)
                    {
                        if (!_pooledObjects[i][j].activeInHierarchy)
                        {
                            if (activateDebugLogs)
                            {
                                Debug.Log("Pooled object retrieved: " + _pooledObjects[i][j].name);
                            }
                            return _pooledObjects[i][j];
                        }
                    }

                    if (activateDebugLogs)
                    {
                        Debug.Log("No available pooled object of type: " + itemPrefab.name);
                    }
                    return null;
                }
            }

            if (activateDebugLogs)
            {
                Debug.Log("Item prefab not found in the object pool: " + itemPrefab.name);
            }
            return null;
        }

        /// <summary>
        /// Returns a pooled object to the object pool and sets it inactive.
        /// </summary>
        /// <param name="obj">The object to return to the object pool.</param>
        /// <param name="itemTag">The tag associated with the item prefab.</param>
        public void ReturnPooledObject(GameObject obj, string itemTag)
        {
            obj.SetActive(false);

            // Add the returned object back to the corresponding pooledObjects list based on its itemTag
            for (int i = 0; i < itemPrefabs.Length; i++)
            {
                if (itemPrefabs[i].CompareTag(itemTag))
                {
                    _pooledObjects[i].Add(obj);
                    break;
                }
            }

            if (activateDebugLogs)
            {
                Debug.Log("Pooled object returned: " + obj.name);
            }
        }
    }
}
