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
        //stat upgrades
        DeliciousFood,
        FastFood,
        Speed,
        Defense,
        Evasion,
        MaxHP,
        PickUpArea,
        Burger,
        //skills will be added later
    }
    public class UpgradeManager : MonoBehaviour
    {
        public List<Upgrade> allUpgrades; 
        private List<Upgrade> currentOptions = new List<Upgrade>();

        [Header("UI References")]
        public GameObject upgradePanel;
        public UpgradeUIOption[] upgradeUIOptions = new UpgradeUIOption[3];

        private Dictionary<UpgradeType, int> currentUpgrades = new Dictionary<UpgradeType, int>();

        public void OnChefLevelUp()
        {
            currentOptions.Clear();

            List<Upgrade> tempUpgradeList = EligibleUpgrades();

            //List<Upgrade> tempUpgradeList = new List<Upgrade>(allUpgrades);

            for (int i = 0; i < 3; i++)
            {
                Upgrade randomUpgrade = tempUpgradeList[Random.Range(0, tempUpgradeList.Count)];
                currentOptions.Add(randomUpgrade);
                tempUpgradeList.Remove(randomUpgrade);
            }

            DisplayUpgradeOptions();
        }

        private List<Upgrade> EligibleUpgrades()
        {
            List<Upgrade> eligibleUpgrades = new List<Upgrade>();
            foreach (var upgrade in allUpgrades)
            {
                int currentLevel = GetUpgradeLevel(upgrade.upgradeType);
                if (upgrade.level == currentLevel + 1)
                {
                    eligibleUpgrades.Add(upgrade);
                }
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

        public void ApplySelectedUpgrade(int index)
        {
            Chef chef = FindObjectOfType<Chef>();
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
