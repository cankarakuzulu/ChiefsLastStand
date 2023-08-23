using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Stat Upgrades/Health Upgrade")]
    public class HealthUpgrade : Upgrade
    {
        public float maxhp;

        public override void ApplyUpgrade(Chef chef)
        {
            float maxhpbefore = chef.ChefData.maxHealth;
            chef.ChefData.maxHealth = chef.ChefDefautStats.maxHealth + chef.ChefDefautStats.maxHealth * (maxhp / 100);
            chef.Heal(chef.ChefData.maxHealth - maxhpbefore);
            Debug.Log("Applied health to chef. New stat is: " + chef.ChefData.maxHealth);
        }
    }
}