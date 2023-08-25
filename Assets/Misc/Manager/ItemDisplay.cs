using TMPro;
using UnityEngine;

namespace Misc.Manager
{
    public class ItemDisplay : MonoBehaviour
    {
        /// <summary>
        /// The singleton instance of the ItemDisplay class.
        /// </summary>
        public static ItemDisplay Instance;

        [SerializeField] private float displayDuration = 2f;
        [SerializeField] private TextMeshProUGUI textMeshPro;

        private float _displayTimer;
        public float DisplayTimer => _displayTimer;

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

            // Initially hide the TMP component by setting an empty string (or transparent text)
            textMeshPro.text = "";
        }

        private void Update()
        {
            // Check if the display timer is active
            if (_displayTimer > 0)
            {
                _displayTimer -= Time.deltaTime;

                // If the timer has expired, hide the item info
                if (_displayTimer <= 0)
                {
                    HideItemInfo();
                }
            }
        }

        /// <summary>
        /// Shows the item information on the UI for a specific duration.
        /// </summary>
        /// <param name="itemName">The name of the item to display.</param>
        /// <param name="quantity">The quantity of the item to display.</param>
        public void ShowItemInfo(string itemName, int quantity)
        {
            // Set the text to display the item name and quantity
            textMeshPro.text = itemName + " x" + quantity;

            // Start the display timer
            _displayTimer = displayDuration;
        }

        /// <summary>
        /// Hides the item information on the UI.
        /// </summary>
        private void HideItemInfo()
        {
            // Clear the text to hide the item info effectively
            textMeshPro.text = "";
        }
    }
}
