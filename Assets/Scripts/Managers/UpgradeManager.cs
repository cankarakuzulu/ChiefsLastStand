using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace nopact.ChefsLastStand.Upgrades
{
    public enum UpgradeType
    {
        //Passive Skills
        DeliciousFood,
        FastFood,
        Speed,
        Defense,
        Evasion,
        MaxHP,
        PickUpArea,
        //Active Skills
        Burger,
        HeatWave,
        Pizza,
        Funyun,
        BuffetTable,
        HotSauce,
        Assistant,
    }
    public class UpgradeManager : MonoBehaviour
    {
        public List<Upgrade> allUpgrades;
        public GameObject upgradePanel;
        public UpgradeUIOption[] upgradeUIOptions = new UpgradeUIOption[3];

        private Chef chef;
        private List<Upgrade> currentOptions = new List<Upgrade>();
        private Dictionary<UpgradeType, int> currentUpgrades = new Dictionary<UpgradeType, int>();

        private void Awake()
        {
            chef = FindObjectOfType<Chef>();
        }

        public void OnChefLevelUp()
        {
            Time.timeScale = 0f;
            currentOptions.Clear();

            List<Upgrade> tempUpgradeList = EligibleUpgrades();

            for (int i = 0; i < 3; i++)
            {
                Upgrade randomUpgrade = tempUpgradeList[Random.Range(0, tempUpgradeList.Count)];
                currentOptions.Add(randomUpgrade);
                tempUpgradeList.Remove(randomUpgrade);
            }

            DisplayUpgradeOptions();
        }

        public void ApplySelectedUpgrade(int index)
        {
            currentOptions[index].ApplyUpgrade(chef);

            // Track the applied upgrade's level
            if (currentUpgrades.ContainsKey(currentOptions[index].upgradeType))
            {
                currentUpgrades[currentOptions[index].upgradeType] = currentOptions[index].level;
            }
            else
            {
                currentUpgrades.Add(currentOptions[index].upgradeType, currentOptions[index].level);
            }

            upgradePanel.SetActive(false);
            Time.timeScale = 1f;
        }

        private List<Upgrade> EligibleUpgrades()
        {
            List<Upgrade> eligibleUpgrades = new List<Upgrade>();
            int activeSkillsCount = 0;
            int passiveSkillsCount = 0;
            HashSet<UpgradeType> possessedSkills = new HashSet<UpgradeType>(currentUpgrades.Keys);

            foreach (var key in currentUpgrades.Keys)
            {
                if (key <= UpgradeType.PickUpArea)
                    passiveSkillsCount++;
                else
                    activeSkillsCount++;
            }

            foreach (var upgrade in allUpgrades)
            {
                int currentLevel = GetUpgradeLevel(upgrade.upgradeType);

                if (upgrade is IActiveSkill && activeSkillsCount >= 5 && !possessedSkills.Contains(upgrade.upgradeType)) continue;
                if (upgrade is IPassiveSkill && passiveSkillsCount >= 5 && !possessedSkills.Contains(upgrade.upgradeType)) continue;

                if (upgrade.level == currentLevel + 1)
                {
                    eligibleUpgrades.Add(upgrade);
                }
            }
            if (eligibleUpgrades.Count == 0)
            {
                Debug.LogWarning("All possessed skills are at their maximum tier. No upgrades available.");
            }
            return eligibleUpgrades;
        }

        private int GetUpgradeLevel(UpgradeType type)
        {
            return currentUpgrades.TryGetValue(type, out int level) ? level : 0;
        }

        private void DisplayUpgradeOptions()
        {
            for (int i = 0; i < currentOptions.Count; i++)
            {
                upgradeUIOptions[i].optionIndex = i;
                upgradeUIOptions[i].SetOption(currentOptions[i], ApplySelectedUpgrade);
            }

            upgradePanel.SetActive(true);
        }
    }

    [System.Serializable]
    public class UpgradeUIOption
    {
        public TMP_Text upgradeNameText;
        public TMP_Text descriptionText;
        public Button selectButton;
        public int optionIndex;

        public void SetOption(Upgrade upgrade, System.Action<int> callback)
        {
            upgradeNameText.text = upgrade.upgradeName;
            descriptionText.text = upgrade.description;
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => callback(optionIndex));
        }
    }
}
