using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Misc.UI
{
    public class UserInterfaceInput : MonoBehaviour
    {
        public static UserInterfaceInput Instance { get; private set; }
        
        [SerializeField] private GameObject uiObject;
        
        private bool _isInventoryOpen;
        
        public event Action<bool> OnInventoryButtonPress;

        public event Action<bool> OnUseButtonPress;
        
        public event Action<bool> OnRemoveButtonPress;
        
        public event Action<bool> OnFavoriteButtonPress;
        
        
        
        
                
        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this;
                // Debug.Log("UiInput instance set.");
            } 
        }
        
        
        
        public void OnInventoryInput(InputAction.CallbackContext context) // Button "I"
        {
            if (context.performed)
            {
                _isInventoryOpen = !_isInventoryOpen;
                OnInventoryButtonPress?.Invoke(_isInventoryOpen);
                if (_isInventoryOpen)
                {
                    uiObject.SetActive(true);
                }
                else
                {
                    uiObject.SetActive(false);
                }
            }
        }

        public void OnUseInput(InputAction.CallbackContext context) 
        {// Button "E" its function changes depending on the type of item, if weapon or armor is to "Equip" or "Unequip", a book "Read", a consumable is "Use" etc.
            if (context.performed)
            {
                OnUseButtonPress?.Invoke(true);
            }
        }

        public void OnRemoveInput(InputAction.CallbackContext context)
        {// Button "R" to remove / drop items
            if (context.performed)
            {
                OnRemoveButtonPress?.Invoke(true);
            }
        }

        public void OnFavoriteInput(InputAction.CallbackContext context)
        {// Button "F" to mark the items as favorite for fast access, for later
            if (context.performed)
            {
                OnFavoriteButtonPress?.Invoke(true);
            }
        }
    }
}