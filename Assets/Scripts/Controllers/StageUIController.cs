using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace nopact.ChefsLastStand
{
    public class StageUIController : MonoBehaviour
    {
        public static StageUIController Instance { get; private set; }
        [SerializeField] private TMP_Text deathCounterText;
        [SerializeField] private Button collectRewardButton;
        [SerializeField] private Button doubleRewardButton;
        [SerializeField] private TMP_Text continueText;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button continueWithAdButton;
        [SerializeField] private GameObject rewardCoins;

        [Header("UI Panels")]
        public GameObject pauseGameUI;
        public GameObject endGameUI;
        public GameObject deathUI;

        
        private float deathCounter = 5f;
        private bool hasUsedContinue = false;

        private void Start()
        {
            pauseGameUI.SetActive(false);
            endGameUI.SetActive(false);
            deathUI.SetActive(false);
            deathCounter = 5f;
        }

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
        }

        public void PauseGame()
        {
            pauseGameUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void ContinueGame()
        {
            pauseGameUI.SetActive(false);
            Time.timeScale = 1f;
        }

        public void RestartLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OpenSettings()
        {
            // Implement settings UI
        }

        public void ExitToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void ShowEndGameUI()
        {
            endGameUI.SetActive(true);
        }

        public void ShowDeathUI()
        {
            deathUI.SetActive(true);
            deathCounterText.gameObject.SetActive(true);
            if (hasUsedContinue)
            {
                HideContinueButtons();
                ShowCollectButtons();
            }
            else
            {
                ShowContinueButtons();
                StartDeathCountdown();
            }
        }

        public void HideDeathUI()
        {
            deathUI.SetActive(false);
        }

        public void StartDeathCountdown()
        {
            StartCoroutine(DeathCountdownCoroutine());
        }

        private IEnumerator DeathCountdownCoroutine()
        {
            while (deathCounter > 0)
            {
                deathCounterText.text = deathCounter.ToString();
                yield return new WaitForSecondsRealtime(1f);
                deathCounter--;
            }
            if (deathCounter <= 0)
            {
                deathCounterText.gameObject.SetActive(false);
                HideContinueButtons();
                ShowCollectButtons();
            }
        }

        public void HideContinueButtons()
        {
            continueButton.gameObject.SetActive(false);
            continueWithAdButton.gameObject.SetActive(false);
            continueText.gameObject.SetActive(false);
            deathCounterText.gameObject.SetActive(false);
        }

        public void ShowContinueButtons()
        {
            continueButton.gameObject.SetActive(true);
            continueWithAdButton.gameObject.SetActive(true);
            continueText.gameObject.SetActive(true);
            deathCounterText.gameObject.SetActive(true);
        }

        public void ShowCollectButtons()
        {
            rewardCoins.SetActive(true);
            doubleRewardButton.gameObject.SetActive(true);
            collectRewardButton.gameObject.SetActive(true);
        }

        public void OnDoubleRewardButtonClicked()
        {
            // WATCH AD then DoubleCollectedCoins()
            DoubleCollectedCoins();
        }

        public void DoubleCollectedCoins()
        {
            Chef chefInstance = FindObjectOfType<Chef>();
            int coins = chefInstance.GetTotalCoinsCollected();
            PlayerPrefs.SetInt("Coins", 2 * coins + PlayerPrefs.GetInt("Coins", 0));
            
            ExitToMainMenu();
        }

        public void ClaimCollectedCoins()
        {
            Chef chefInstance = FindObjectOfType<Chef>();
            int coins = chefInstance.GetTotalCoinsCollected();
            PlayerPrefs.SetInt("Coins", coins + PlayerPrefs.GetInt("Coins", 0));
            
            ExitToMainMenu();
        }

        public void ContinueAfterDeath()
        {
            if (!hasUsedContinue)
            {
                Chef chefInstance = FindObjectOfType<Chef>();
                if (chefInstance != null)
                {
                    HideDeathUI();
                    chefInstance.ReviveAfterDeath();
                    hasUsedContinue = true;
                }
            }
        }

        public void ContinueWithAd()
        {
            // WATCH AD then revive
            ContinueAfterDeath();
        }

    }
}
