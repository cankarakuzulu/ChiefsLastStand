using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Projectiles;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class AngryBoss : Boss
    {
        [Header("Normal Attack")]
        [SerializeField] private GameObject directProjectilePrefab;
        [SerializeField] private float normalAttackRange = 5f;

        [Header("Surrounding Attack")]
        [SerializeField] private GameObject surroundingProjectilePrefab;
        [SerializeField] private float surroundingAttackInterval = 7f;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(DirectAttackRoutine());
            StartCoroutine(SurroundingAttackRoutine());
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void IdleBehavior() { }

        protected override void AttackBehavior()
        {
            ShootDirectProjectile();
        }

        protected override void SpecialAttackBehavior()
        {
            ShootSurroundingProjectile();
        }

        protected override void MoveBehavior()
        {
        }

        private IEnumerator DirectAttackRoutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => Vector2.Distance(transform.position, chefTransform.position) <= normalAttackRange);
                AttackBehavior();
                yield return new WaitForSeconds(characterData.attackCooldown);
            }
        }

        private IEnumerator SurroundingAttackRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(surroundingAttackInterval);
                SpecialAttackBehavior();
            }
        }

        private void ShootDirectProjectile()
        {
            Vector2 directionToChef = (chefTransform.position - transform.position).normalized;

            GameObject proj = Instantiate(directProjectilePrefab, transform.position, Quaternion.identity);
            CustomerProjectile projectile = proj.GetComponent<CustomerProjectile>();
            projectile.SetMoveDirection(directionToChef);
            projectile.SetDamage(characterData.damage);
        }

        private void ShootSurroundingProjectile()
        {
            int numberOfProjectiles = 8;
            float angleStep = 360f / numberOfProjectiles;

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float currentAngle = i * angleStep;
                Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
                Vector2 direction = rotation * Vector2.up;

                GameObject proj = Instantiate(surroundingProjectilePrefab, transform.position, rotation);
                CustomerProjectile projectile = proj.GetComponent<CustomerProjectile>();
                projectile.SetMoveDirection(direction);
                projectile.SetDamage(characterData.damage);
            }
        }
    }
}
