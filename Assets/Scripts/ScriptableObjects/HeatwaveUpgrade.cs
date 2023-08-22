using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Heat Wave Skill Upgrade")]
    public class HeatwaveUpgrade : Upgrade
    {
        public GameObject heatWaveCirclePrefab;
        public float damagePerSecond;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ApplyHeatWaveSkill(this);
        }
    }
}
