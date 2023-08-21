using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Speed Upgrade")]
    public class SpeedUpgrade : Upgrade
    {
        public float speedBoostPercent;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ChefData.moveSpeed += chef.ChefData.moveSpeed * (speedBoostPercent / 100);
            Debug.Log("Applied speed to chef. New stat is: " + chef.ChefData.moveSpeed);
        }
    }
}
