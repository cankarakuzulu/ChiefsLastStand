using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace nopact.ChefsLastStand.UI
{
    public class LevelingUI : MonoBehaviour
    {
        [SerializeField] private Slider progressSlider;
        [SerializeField] private Chef chef;
        [SerializeField] private TextMeshProUGUI levelText;

        private void Awake()
        {
            chef.OnCoinCollected += UpdateProgress;
            UpdateLevelText();
        }

        public void UpdateProgress(int currentCoins, int coinsNeeded)
        {
            progressSlider.maxValue = coinsNeeded;
            progressSlider.value = currentCoins;
            UpdateLevelText();
        }

        private void UpdateLevelText()
        {
            levelText.text = chef.CurrentLevel.ToString();
        }

        private void OnDestroy()
        {
            chef.OnCoinCollected -= UpdateProgress;
        }
    }
}
