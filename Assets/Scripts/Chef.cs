using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.ChefData;
using nopact.ChefsLastStand.Upgrades;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class Chef : Character
    {
        [SerializeField] private ChefData chefData;
        public ChefData ChefData => chefData;

        [SerializeField] private UpgradeManager upgradeManager;

        private int coinsCollected = 0;
        private int totalCoinsCollected = 0;
        private int currentLevel = 1;

        private int coinsForLevelUp => currentLevel * 3;

        public void CollectCoin()
        {
            coinsCollected++;
            totalCoinsCollected++;
            CheckForLevelUp();
        }

        private void CheckForLevelUp()
        {
            if (coinsCollected >= coinsForLevelUp)
            {
                LevelUp();
                coinsCollected = 0;
            }
        }

        private void LevelUp()
        {
            currentLevel++;
            upgradeManager.OnChefLevelUp();
        }

        public int GetTotalCoinsCollected()
        {
            return totalCoinsCollected;
        }
    }
}
