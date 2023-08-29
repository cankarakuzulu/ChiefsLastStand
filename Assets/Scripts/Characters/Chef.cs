using System;
using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.ChefData;
using nopact.ChefsLastStand.Upgrades;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class Chef : Character
    {
        [SerializeField] private ChefData defaultChefData; // This remains unchanged
        private ChefData currentChefData; // This changes with upgrades
        [SerializeField] private UpgradeManager upgradeManager;

        public ChefData ChefData => currentChefData;
        public ChefData ChefDefautStats => defaultChefData;

        private int coinsCollected = 0;
        private int totalCoinsCollected = 0;
        private int currentLevel = 1;

        private GameObject currentHeatWaveAura;
        private Pizza currentPizza;

        private int coinsForLevelUp => currentLevel * 3;
        public int CurrentLevel => currentLevel;

        public delegate void CoinCollectedHandler(int currentCoins, int coinsNeededForLevelUp);
        public event CoinCollectedHandler OnCoinCollected;

        protected override void Start()
        {
            base.Start();
            ResetToDefaultStats();
        }

        public void CollectCoin()
        {
            coinsCollected++;
            totalCoinsCollected++;
            CheckForLevelUp();

            OnCoinCollected?.Invoke(coinsCollected, coinsForLevelUp);
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

        public void ApplySkillWithRoutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        public IEnumerator SkillRoutine(Action skillAction, float cooldown)
        {
            while (true)
            {
                skillAction();
                yield return new WaitForSeconds(cooldown);
            }
        }

        public void ApplyHeatWaveSkill(HeatwaveUpgrade skill)
        {
            if (currentHeatWaveAura != null)
            {
                Destroy(currentHeatWaveAura);
            }

            currentHeatWaveAura = Instantiate(skill.heatWaveCirclePrefab, transform.position, Quaternion.identity, transform);
            var auraScript = currentHeatWaveAura.GetComponent<HeatWaveAura>();
            auraScript.SetDamage(skill.damagePerSecond);
        }

        public void ActivatePizzaSkill(PizzaSkill skill)
        {
            if (currentPizza != null)
            {
                Destroy(currentPizza);
            }

            currentPizza = gameObject.AddComponent<Pizza>();
            currentPizza.ActivatePizzaSkill(skill);
        }

        private void ResetToDefaultStats()
        {
            currentChefData = ScriptableObject.CreateInstance<ChefData>();
            currentChefData.health = defaultChefData.health;
            currentChefData.damage = defaultChefData.damage;
            currentChefData.moveSpeed = defaultChefData.moveSpeed;
            currentChefData.attackCooldown = defaultChefData.attackCooldown;
            currentChefData.attackRange = defaultChefData.attackRange;
            currentChefData.defense = defaultChefData.defense;
            currentChefData.evasion = defaultChefData.evasion;
            currentChefData.maxHealth = defaultChefData.maxHealth;
            currentChefData.pickUpArea = defaultChefData.pickUpArea;
            currentChefData.burgerCount = defaultChefData.burgerCount;
        }
    }
}
