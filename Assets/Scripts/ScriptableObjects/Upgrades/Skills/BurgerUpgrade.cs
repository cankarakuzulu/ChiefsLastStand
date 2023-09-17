using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Stat Upgrades/Burger Upgrade")]
    public class BurgerUpgrade : Upgrade, IActiveSkill
    {
        public int extraBurgerCount;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ChefData.burgerCount = chef.ChefDefautStats.burgerCount + extraBurgerCount;
            Debug.Log("Applied burger upgrade, new burger count is: " + chef.ChefData.burgerCount);

        }
    }
}
