using System;
using System.Collections.Generic;
using Data.BaseClasses;
using UnityEngine;

namespace Misc.Manager
{
    public class ObjectDetector : MonoBehaviour
    {
        public static ObjectDetector Instance;
        
        public event Action<BaseEntity> OnEntityInteract;
        [SerializeField] private List<string> targetTags = new List<string> { "Debug" };
        public float detectionRadius = 3.0f;

        private GameObject nearestObject;
        
        
        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            }
        }

        private void Update()
        {
            nearestObject = GetObjectNearMousePointer();
            DebugNearbyObject();
        }

        private GameObject GetObjectNearMousePointer()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D[] hits = Physics2D.CircleCastAll(mousePosition2D, detectionRadius, Vector2.zero);

            GameObject nearestObject = null;
            float nearestDistance = Mathf.Infinity;

            foreach (RaycastHit2D hit in hits)
            {
                if (targetTags.Contains(hit.collider.gameObject.tag))
                {
                    float distance = Vector2.Distance(mousePosition2D, hit.point);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestObject = hit.collider.gameObject;
                    }
                }
            }

            return nearestObject;
        }

        private void ActivateNearbyEntity()
        {
            if (nearestObject != null)
            {
                BaseEntity entity = nearestObject.GetComponent<BaseEntity>();
                if (entity != null)
                {
                    OnEntityInteract?.Invoke(entity);
                }
            }
        }

        private void DebugNearbyObject()
        {
            if (nearestObject != null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                Debug.DrawLine(mousePosition, nearestObject.transform.position, Color.red);
                DebugDrawCircle(mousePosition, detectionRadius, Color.green);

                Debug.Log("Detected Item: " + nearestObject.name + ", Tag: " + nearestObject.tag);
            }
            else
            {
                Debug.Log("No Detected Item");
            }
        }

        private void DebugDrawCircle(Vector3 center, float radius, Color color)
        {
            Vector3[] circlePoints = new Vector3[64];
            for (int i = 0; i < 64; i++)
            {
                float angle = i * (Mathf.PI * 2f) / 64;
                circlePoints[i] = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            }

            for (int i = 0; i < 64; i++)
            {
                Debug.DrawLine(circlePoints[i], circlePoints[(i + 1) % 64], color);
            }
        }
    }
}
