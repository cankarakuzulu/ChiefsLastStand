using System;
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
            Action funyunSkillAction = () =>
            {
                GameObject funyunInstance = Instantiate(funyunsPrefab, chef.transform.position, Quaternion.identity);
                Funyun funyunScript = funyunInstance.GetComponent<Funyun>();
                funyunScript.Initialize(this, chef.transform.position);
            };

            chef.ApplySkillWithRoutine(chef.SkillRoutine(funyunSkillAction, cooldown));
        }
    }
}
