using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Stat Upgrades/Fast Food Upgrade")]
    public class FastFoodUpgrade : Upgrade
    {
        public float cooldownReductionPercent;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ChefData.attackCooldown = chef.ChefDefautStats.attackCooldown - chef.ChefDefautStats.attackCooldown * (cooldownReductionPercent / 100);
            Debug.Log("Applied speed to chef. New stat is: " + chef.ChefData.attackCooldown);
        }
    }
}
