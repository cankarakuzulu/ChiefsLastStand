using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/SpeedUpgrade")]
    public class SpeedUpgrade : Upgrade
    {
        public float speedMultiplier = 1.5f;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.characterData.moveSpeed *= speedMultiplier;
        }
    }
}
