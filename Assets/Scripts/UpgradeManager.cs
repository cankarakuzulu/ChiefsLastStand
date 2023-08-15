using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        public List<Upgrade> allUpgrades; 
        private List<Upgrade> currentOptions = new List<Upgrade>(); 

        public void OnChefLevelUp()
        {
            // Select 3 random upgrades from the list
            currentOptions.Clear();

            for (int i = 0; i < 3; i++)
            {
                Upgrade randomUpgrade = allUpgrades[Random.Range(0, allUpgrades.Count)];
                currentOptions.Add(randomUpgrade);
                allUpgrades.Remove(randomUpgrade);
            }

            // will implement upgrade selection UI later
            // (UI should be able to access currentOptions to display the options to the player)
        }

        public void ApplySelectedUpgrade(int index)
        {
            Chef chef = FindObjectOfType<Chef>();
            currentOptions[index].ApplyUpgrade(chef);

            // Optionally, put the used upgrade back to the allUpgrades list
            // allUpgrades.Add(currentOptions[index]);
        }
    }
}
