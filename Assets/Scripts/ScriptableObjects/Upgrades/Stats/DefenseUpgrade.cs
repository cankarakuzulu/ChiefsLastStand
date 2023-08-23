using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Stat Upgrades/Defense Upgrade")]
    public class DefenseUpgrade : Upgrade
    {
        public float defense;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ChefData.defense = chef.ChefDefautStats.defense + defense;
            Debug.Log("Applied defense to chef. New stat is: " + chef.ChefData.defense);
        }
    }
}
