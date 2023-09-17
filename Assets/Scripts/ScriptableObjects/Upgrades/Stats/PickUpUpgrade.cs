using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Stat Upgrades/Pick Up Area Upgrade")]
    public class PickUpUpgrade : Upgrade, IPassiveSkill
    {
        public float pickupEnhancementPercent;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ChefData.pickUpArea = chef.ChefDefautStats.pickUpArea + chef.ChefDefautStats.pickUpArea * (pickupEnhancementPercent / 100);
            Debug.Log("Applied pick up range to chef. New stat is: " + chef.ChefData.pickUpArea);
        }
    }
}
