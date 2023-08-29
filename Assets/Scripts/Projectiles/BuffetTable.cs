using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using nopact.ChefsLastStand.Gameplay.Projectiles;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    public class BuffetTable : MonoBehaviour
    {
        private Vector3 targetPosition;
        private BuffetTableSkill skill;

        public void Initialize(BuffetTableSkill skill, Vector3 targetPosition)
        {
            this.skill = skill;
            this.targetPosition = targetPosition;
        }

        private void Update()
        {
            // Move towards the target
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5 * Time.deltaTime);

            // Check if we've reached the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Create the damage area
                GameObject damageArea = Instantiate(skill.damageAreaPrefab, targetPosition, Quaternion.identity);
                damageArea.GetComponent<DamageArea>().damage = skill.damage;

                // Destroy this projectile
                Destroy(gameObject);
            }
        }
    }
}

