using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace nopact.ChefsLastStand
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Currency Display")]
        public TMP_Text coinsText;
        public TMP_Text diamondText;

        [Header("Restaurant and Stage")]
        public Image restaurantImage;
        public TMP_Text restaurantNameText;
        public TMP_Text stageInfoText;
        public List<RestaurantData> restaurants;
        private int currentRestaurantIndex = 0;
        private int currentStageIndex = 0;

        private void Start()
        {
            UpdateCurrencyDisplay();
            UpdateStageDisplay();
        }

        public void UpdateCurrencyDisplay()
        {
            coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
            diamondText.text = PlayerPrefs.GetInt("Diamond", 0).ToString();
        }

        public void UpdateStageDisplay()
        {
            restaurantImage.sprite = restaurants[currentRestaurantIndex].restaurantImage;
            restaurantNameText.text = restaurants[currentRestaurantIndex].restaurantName;
            stageInfoText.text = "Stage " + (currentStageIndex + 1).ToString();
        }

        public void NextStage()
        {
            currentStageIndex = (currentStageIndex + 1) % restaurants[currentRestaurantIndex].stages.Count;
            UpdateStageDisplay();
        }

        public void PreviousStage()
        {
            currentStageIndex--;
            if (currentStageIndex < 0)
            {
                currentStageIndex = restaurants[currentRestaurantIndex].stages.Count - 1;
            }
            UpdateStageDisplay();
        }

        public void StartBattle()
        {
            string sceneToLoad = restaurants[currentRestaurantIndex].stages[currentStageIndex].sceneName;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
