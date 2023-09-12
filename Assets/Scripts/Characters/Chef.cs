using System;
using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.ChefData;
using nopact.ChefsLastStand.Upgrades;
using nopact.ChefsLastStand.Gameplay.Items;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class Chef : Character
    {
        [SerializeField] private ChefData defaultChefData;
        [SerializeField] private UpgradeManager upgradeManager;

        private ChefData currentChefData;
        private int coinsCollected = 0;
        private int totalCoinsCollected = 0;
        private int currentLevel = 1;

        private Pizza currentPizza;

        public ChefData ChefData => currentChefData;
        public ChefData ChefDefautStats => defaultChefData;
        private int coinsForLevelUp => currentLevel * 3;
        public int CurrentLevel => currentLevel;

        public delegate void CoinCollectedHandler(int currentCoins, int coinsNeededForLevelUp);
        public event CoinCollectedHandler OnCoinCollected;

        protected override void Start()
        {
            base.Start();
            ResetToDefaultStats();
        }
        private void Update()
        {
            PickUpCoinsInRange();
            CheckForLevelUp();
        }

        public override void TakeDamage(float damage)
        {
            float evasionChance = UnityEngine.Random.Range(0f, 100f);
            if (evasionChance < ChefData.evasion)
            {
                Debug.Log("Attack evaded!");
                return;
            }

            float reducedDamage = damage * (1 - (ChefData.defense / 100f));

            base.TakeDamage(reducedDamage);
        }


        public void CollectCoin()
        {
            coinsCollected++;
            totalCoinsCollected++;
            CheckForLevelUp();
            OnCoinCollected?.Invoke(coinsCollected, coinsForLevelUp);
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

        public void ActivatePizzaSkill(PizzaSkill skill)
        {
            if (currentPizza != null)
            {
                Destroy(currentPizza);
            }

            currentPizza = gameObject.AddComponent<Pizza>();
            currentPizza.ActivatePizzaSkill(skill);
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

        private void PickUpCoinsInRange()
        {
            // Check in a circular area around the chef for any coins
            Collider2D[] coinsInRange = Physics2D.OverlapCircleAll(transform.position, ChefData.pickUpArea);

            foreach (Collider2D collider in coinsInRange)
            {
                if (collider.gameObject.CompareTag("Coin"))
                {
                    Coin coin = collider.gameObject.GetComponent<Coin>();
                    coin.MoveToChef();
                }
            }
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
