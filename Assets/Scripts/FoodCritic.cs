using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using nopact.ChefsLastStand.Gameplay.Projectiles;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class FoodCritic : Boss
    {
        [Header("Normal Attack")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float normalAttackRange = 5f;

        [Header("Special Attack")]
        [SerializeField] private float specialAttackInterval = 10f;
        [SerializeField] private float specialAttackSpreadAngle = 60f;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(NormalAttackRoutine());
            StartCoroutine(SpecialAttackRoutine());
        }

        protected override void Update()
        {
            base.Update();

            // Check if Chef is out of range and transition to Moving state
            if (Vector2.Distance(transform.position, chefTransform.position) > normalAttackRange && currentState != BossState.Moving)
            {
                currentState = BossState.Moving;
            }
        }

        protected override void IdleBehavior() {}

        protected override void AttackBehavior()
        {
            ShootProjectileAtAngle(0);
        }

        protected override void SpecialAttackBehavior()
        {
            ShootProjectileAtAngle(0);
            ShootProjectileAtAngle(specialAttackSpreadAngle);
            ShootProjectileAtAngle(-specialAttackSpreadAngle);
        }

        protected override void MoveBehavior()
        {
            Vector2 directionToChef = (chefTransform.position - transform.position).normalized;
            transform.position += (Vector3)directionToChef * characterData.moveSpeed * Time.deltaTime;

            if (Vector2.Distance(transform.position, chefTransform.position) <= normalAttackRange)
            {
                currentState = BossState.Idle;
            }
        }

        private IEnumerator NormalAttackRoutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => Vector2.Distance(transform.position, chefTransform.position) <= normalAttackRange);
                AttackBehavior();
                yield return new WaitForSeconds(characterData.attackCooldown);
            }
        }

        private IEnumerator SpecialAttackRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(specialAttackInterval);
                SpecialAttackBehavior();
            }
        }

        private void ShootProjectileAtAngle(float angle)
        {
            Vector2 directionToChef = (chefTransform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector2 rotatedDirection = rotation * directionToChef;

            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            CustomerProjectile projectile = proj.GetComponent<CustomerProjectile>();
            projectile.SetMoveDirection(rotatedDirection);
            projectile.SetDamage(characterData.damage);
        }
    }
}
