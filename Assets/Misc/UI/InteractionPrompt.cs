using Data.Actors.Player;
using Data.BaseClasses;
using UnityEngine;

namespace Misc.UI
{
    public class InteractionPrompt : MonoBehaviour
    {
        public static InteractionPrompt Instance;
        
        [SerializeField] protected internal GameObject activateTextPrefab;

        private GameObject _textPrefabInstance;
        
        // public event Action<BaseActor> OnActivate;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }


            Player.Instance.Variables.PlayerInputManager.OnActionButtonPressed += OnActivationRequest;
        }
        

        private void OnDestroy()
        {
            Player.Instance.Variables.PlayerInputManager.OnActionButtonPressed -= OnActivationRequest;
        }

        public void InstantiateActivatorUi(BaseEntity entity) // We get the name from the entity data
        {
            
            
        }
        
        private void OnActivationRequest(bool isActive)
        {
            if (isActive)
            {
                // Logic to 
            }
            Debug.Log("Activator working ");
            // If it is true
        }
    }
}