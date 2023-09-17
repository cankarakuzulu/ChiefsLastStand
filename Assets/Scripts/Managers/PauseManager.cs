using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuUI;

        private bool isPaused = false;

        private void Start()
        {
            pauseMenuUI.SetActive(false);
        }

        public void TogglePauseMenu()
        {
            if (isPaused)
            {
                HidePauseMenu();
                ResumeGame();
            }
            else
            {
                ShowPauseMenu();
                PauseGame();
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            isPaused = false;
        }

        public void ShowPauseMenu()
        {
            pauseMenuUI.SetActive(true);
        }

        public void HidePauseMenu()
        {
            pauseMenuUI.SetActive(false);
        }
    }
}
