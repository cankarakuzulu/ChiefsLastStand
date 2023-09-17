using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;
using TMPro;

namespace nopact.ChefsLastStand.UI
{
    public class TotalCoinCounterUI : MonoBehaviour
    {
        [SerializeField] private Chef chef;
        [SerializeField] private TextMeshProUGUI coinText;

        private void Awake()
        {
            UpdateCoinText();

            chef.OnCoinCollected += UpdateCoinText;
        }

        private void UpdateCoinText(int currentCoins = 0, int coinsNeededForLevelUp = 0)
        {
            coinText.text = chef.GetTotalCoinsCollected().ToString();
        }
        private void OnDestroy()
        {
            chef.OnCoinCollected -= UpdateCoinText;
        }
    }
}
