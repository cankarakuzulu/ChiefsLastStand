using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Fast Food Upgrade")]
    public class FastFoodUpgrade : Upgrade
    {
        public float cooldownReductionPercent;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ChefData.attackCooldown -= chef.ChefData.attackCooldown * (cooldownReductionPercent / 100);
        }
    }
}
