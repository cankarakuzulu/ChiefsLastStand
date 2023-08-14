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

        private enum RangedCustomerState
        {
            Chasing,
            Attacking
        }

        private RangedCustomerState currentState = RangedCustomerState.Chasing;

        protected void Update()
        {
            DetermineState();

            switch (currentState)
            {
                case RangedCustomerState.Chasing:
                    ChaseChef();
                    break;
                case RangedCustomerState.Attacking:
                    if (Time.time >= lastAttackTime + characterData.attackCooldown)
                    {
                        Attack();
                    }
                    break;
            }
        }

        protected override void ChaseChef()
        {
            if (chefTransform == null) return;

            Vector2 directionToChef = (chefTransform.position - transform.position).normalized;
            transform.position += (Vector3)directionToChef * characterData.moveSpeed * Time.deltaTime;
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

        private void DetermineState()
        {
            if (chefTransform == null) return;

            float distanceToChef = Vector2.Distance(transform.position, chefTransform.position);

            if (distanceToChef > attackRange)
            {
                currentState = RangedCustomerState.Chasing;
            }
            else
            {
                currentState = RangedCustomerState.Attacking;
            }
        }
    }
}
