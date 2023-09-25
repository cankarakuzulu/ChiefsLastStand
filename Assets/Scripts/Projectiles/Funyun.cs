using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    public class Funyun : MonoBehaviour
    {
        private Vector3 direction;
        private bool isReturning = false;
        private FunyunSkill skill;
        private Transform chefTransform;

        private void Update()
        {
            transform.position += direction * skill.speed * Time.deltaTime;

            // Check for max range and switch direction towards chef
            if (Vector3.Distance(transform.position, chefTransform.position) > skill.maxRange && !isReturning)
            {
                direction = (chefTransform.position - transform.position).normalized;
                isReturning = true;
            }

            if (!transform.IsInCameraView())
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IAttackable attackable = other.GetComponent<IAttackable>();
            if (attackable != null)
            {
                attackable.TakeDamage(skill.damage);
            }
        }

        public void Initialize(FunyunSkill skill, Vector3 chefPosition)
        {
            this.skill = skill;
            IAttackable target = AttackableSearch.FindNearestAttackable(chefPosition,skill.maxRange);

            if (target != null)
            {
                direction = (target.GetTransform().position - transform.position).normalized;
                chefTransform = FindObjectOfType<Chef>().transform;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
