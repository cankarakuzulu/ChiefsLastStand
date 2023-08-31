using System;
using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using nopact.ChefsLastStand.Gameplay.Projectiles;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Skill Upgrades/Hot Sauce Skill")]
    public class HotSauceSkill : Upgrade
    {
        public float damagePerSecond;
        public float slowEffect;
        public float cooldown;
        public GameObject hotSauceBottlePrefab;
        public GameObject hotSauceAreaPrefab;

        public override void ApplyUpgrade(Chef chef)
        {
            Action hotSauceSkillAction = () =>
            {
                Customer targetCustomer = FindRandomCustomer();
                if (targetCustomer != null)
                {
                    GameObject hotSauceInstance = Instantiate(hotSauceBottlePrefab, chef.transform.position, Quaternion.identity);
                    HotSauceBottle hotSauceScript = hotSauceInstance.GetComponent<HotSauceBottle>();
                    hotSauceScript.Initialize(this, targetCustomer.transform.position);
                }
            };

            chef.ApplySkillWithRoutine(chef.SkillRoutine(hotSauceSkillAction, cooldown));
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
