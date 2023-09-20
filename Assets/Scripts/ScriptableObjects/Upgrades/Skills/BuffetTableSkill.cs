using System;
using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Skill Upgrades/Buffet Table Skill")]
    public class BuffetTableSkill : Upgrade, IActiveSkill
    {
        public float damage;
        public float radius;
        public float cooldown;
        public GameObject buffetTablePrefab;
        public GameObject damageAreaPrefab;

        public override void ApplyUpgrade(Chef chef)
        {
            Action buffetTableSkillAction = () =>
            {
                IAttackable targetAttackable = AttackableSearch.FindRandomAttackable();
                if (targetAttackable != null)
                {
                    GameObject buffetTableInstance = Instantiate(buffetTablePrefab, chef.transform.position, Quaternion.identity);
                    BuffetTable buffetTableScript = buffetTableInstance.GetComponent<BuffetTable>();
                    buffetTableScript.Initialize(this, targetAttackable.GetTransform().position);
                }
            };

            chef.ApplySkillWithRoutine(chef.SkillRoutine(buffetTableSkillAction, cooldown));
        }
    }
}
 