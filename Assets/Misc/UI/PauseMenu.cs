// using UnityEngine;
//
// namespace Misc.UI
// {
//     public class PauseMenu : MonoBehaviour
//     {
//         [SerializeField, Tooltip("The GameObject representing the pause panel in the UI.")]
//         private GameObject pausePanel;
//
//         private bool _isPaused;
//
//         void Update()
//         {
//             // if (Input.GetButtonDown("Pause"))
//             // {
//             //     if (_isPaused)
//             //     {
//             //         ResumeGame();
//             //     }
//             //     else
//             //     {
//             //         PauseGame();
//             //     }
//             // }
//         }
//
//         /// <summary>
//         /// Pauses the game by setting the time scale to 0 and activating the pause panel.
//         /// </summary>
//         public void PauseGame()
//         {
//             Time.timeScale = 0;
//             pausePanel.SetActive(true);
//             _isPaused = true;
//         }
//
//         /// <summary>
//         /// Resumes the game by setting the time scale to 1 and deactivating the pause panel.
//         /// </summary>
//         public void ResumeGame()
//         {
//             Time.timeScale = 1;
//             pausePanel.SetActive(false);
//             _isPaused = false;
//         }
//     }
// }