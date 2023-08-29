using System;
using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Skill Upgrades/Buffet Table Skill")]
    public class BuffetTableSkill : Upgrade
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
                Customer targetCustomer = FindRandomCustomer();
                if (targetCustomer != null)
                {
                    GameObject buffetTableInstance = Instantiate(buffetTablePrefab, chef.transform.position, Quaternion.identity);
                    BuffetTable buffetTableScript = buffetTableInstance.GetComponent<BuffetTable>();
                    buffetTableScript.Initialize(this, targetCustomer.transform.position);
                }
            };

            chef.ApplySkillWithRoutine(chef.SkillRoutine(buffetTableSkillAction, cooldown));
        }
        private Customer FindRandomCustomer()
        {
            Customer[] customers = FindObjectsOfType<Customer>();
            if (customers.Length == 0) return null;

            int randomIndex = UnityEngine.Random.Range(0, customers.Length);
            return customers[randomIndex];
        }
    }
}
 