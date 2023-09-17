using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Stat Upgrades/Delicious Food Upgrade")]
    public class DeliciousFoodUpgrade : Upgrade, IPassiveSkill
    {
        public float damageBoostPercent;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ChefData.damage = chef.ChefDefautStats.damage + chef.ChefDefautStats.damage * (damageBoostPercent / 100);
            Debug.Log("Applied speed to chef. New stat is: " + chef.ChefData.damage);
        }
    }
}
