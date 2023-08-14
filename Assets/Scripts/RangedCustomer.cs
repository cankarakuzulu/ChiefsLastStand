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

        protected override void UpdateState()
        {
            if (chefTransform == null) return;

            float distanceToChef = Vector2.Distance(transform.position, chefTransform.position);

            currentState = distanceToChef <= attackRange ? CustomerState.Attacking : CustomerState.Chasing;
        }

        protected override void ChaseChef()
        {
            Vector2 directionToChef = (chefTransform.position - transform.position).normalized;
            transform.position += (Vector3)directionToChef * characterData.moveSpeed * Time.deltaTime;
        }

        protected override void Attack()
        {
            if (Time.time >= lastAttackTime + characterData.attackCooldown && chefTransform != null)
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
}
