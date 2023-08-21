using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Evasion Upgrade")]
    public class EvasionUpgrade : Upgrade
    {
        public float evasion;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ChefData.evasion = chef.ChefDefautStats.evasion + evasion;
            Debug.Log("Applied evasion to chef. New stat is: " + chef.ChefData.evasion);
        }
    }
}