using System;
using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Skill Upgrades/Assistant Skill")]
    public class AssistantSkill : Upgrade
    {
        public float attackCooldown;
        public float followDistance;
        public GameObject assistantPrefab;

        public override void ApplyUpgrade(Chef chef)
        {
            Vector3 spawnpos = new Vector3(chef.transform.position.x + 1f, chef.transform.position.y, chef.transform.position.z);
            Action assistantSkillAction = () =>
            {
                // Instantiate the assistant and initialize its attributes
                GameObject assistantInstance = Instantiate(assistantPrefab, spawnpos, Quaternion.identity);
                Assistant assistantScript = assistantInstance.GetComponent<Assistant>();
                assistantScript.Initialize(this, chef);
            };

            // Start the assistant skill
            assistantSkillAction();
        }
    }
}
