using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Delicious Food Upgrade")]
    public class DeliciousFoodUpgrade : Upgrade
    {
        public float damageBoostPercent;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ChefData.damage += chef.ChefData.damage * (damageBoostPercent / 100);
        }
    }
}
