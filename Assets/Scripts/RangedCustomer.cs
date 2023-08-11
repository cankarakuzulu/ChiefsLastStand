using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Projectiles;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class RangedCustomer : Customer
    {
        [SerializeField] private GameObject rangedProjectilePrefab;
        [SerializeField] private float attackRange;
        protected override void ChaseChef()
        {
            if (chefTransform == null) return;

            float distanceToChef = Vector2.Distance(transform.position, chefTransform.position);

            if (distanceToChef > attackRange)
            {
                // Chase the Chef if not within attack range
                Vector2 directionToChef = (chefTransform.position - transform.position).normalized;
                transform.position += (Vector3)directionToChef * characterData.moveSpeed * Time.deltaTime;
            }
            else
            {
                // Within attack range, so stop moving and prepare to attack
                if (Time.time >= lastAttackTime + characterData.attackCooldown)
                {
                    Attack();
                }
            }
        }

        protected override void Attack()
        {
            GameObject projectileGO = Instantiate(rangedProjectilePrefab, transform.position, Quaternion.identity);
            CustomerProjectile projectile = projectileGO.GetComponent<CustomerProjectile>();
            Vector2 directionToChef = (chefTransform.position - transform.position).normalized;
            projectile.SetMoveDirection(directionToChef);
            projectile.SetDamage(characterData.damage);

            lastAttackTime = Time.time;
        }
    }
}
