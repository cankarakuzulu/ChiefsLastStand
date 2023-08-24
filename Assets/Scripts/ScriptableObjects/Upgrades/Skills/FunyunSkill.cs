using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Skill Upgrades/Funyun Skill")]
    public class FunyunSkill : Upgrade
    {
        public float damage;
        public float cooldown;
        public float speed;
        public GameObject funyunsPrefab;
        public float maxRange;      // Maximum range before it starts returning

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ApplyFunyunSkill(this);
        }
    }
}
