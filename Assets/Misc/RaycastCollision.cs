using UnityEngine;
namespace Misc
{
    public static class RaycastCollision
    {
        public static bool IsGroundedWithRaycast(this MonoBehaviour monoBehaviour, LayerMask groundLayer, float groundRaycastLength, float slopeRaycastAngle, float slopeRaycastLength)
        {
            // Check if the game object is grounded using a raycast
            bool isGrounded = Physics2D.Raycast(monoBehaviour.transform.position, Vector2.down, groundRaycastLength, groundLayer);

            // Check slope raycasts
            if (slopeRaycastAngle > 0f)
            {
                float slopeRaycastAngleRad = Mathf.Deg2Rad * slopeRaycastAngle;
                Vector2 slopeRaycastDirectionRight = new Vector2(Mathf.Cos(slopeRaycastAngleRad), -Mathf.Sin(slopeRaycastAngleRad));
                Vector2 slopeRaycastDirectionLeft = new Vector2(-Mathf.Cos(slopeRaycastAngleRad), -Mathf.Sin(slopeRaycastAngleRad));

                if (Physics2D.Raycast(monoBehaviour.transform.position, slopeRaycastDirectionRight, slopeRaycastLength, groundLayer) ||
                    Physics2D.Raycast(monoBehaviour.transform.position, slopeRaycastDirectionLeft, slopeRaycastLength, groundLayer))
                {
                    isGrounded = true;
                }
            }

            return isGrounded;
        }

        public static void DrawGizmosForRaycast(this MonoBehaviour monoBehaviour, bool showRaycastDebug, float groundRaycastLength, float slopeRaycastAngle, float slopeRaycastLength)
        {
            if (showRaycastDebug)
            {
                // Draw ground raycast
                Gizmos.color = Color.red;
                Gizmos.DrawLine(monoBehaviour.transform.position, monoBehaviour.transform.position + Vector3.down * groundRaycastLength);

                // Draw slope raycasts
                float slopeRaycastAngleRad = Mathf.Deg2Rad * slopeRaycastAngle;
                Vector2 slopeRaycastDirectionRight = new Vector2(Mathf.Cos(slopeRaycastAngleRad), -Mathf.Sin(slopeRaycastAngleRad));
                Vector2 slopeRaycastDirectionLeft = new Vector2(-Mathf.Cos(slopeRaycastAngleRad), -Mathf.Sin(slopeRaycastAngleRad));

                Gizmos.DrawLine(monoBehaviour.transform.position, monoBehaviour.transform.position + (Vector3)slopeRaycastDirectionRight * slopeRaycastLength);
                Gizmos.DrawLine(monoBehaviour.transform.position, monoBehaviour.transform.position + (Vector3)slopeRaycastDirectionLeft * slopeRaycastLength);
            }
        }
    }
}
