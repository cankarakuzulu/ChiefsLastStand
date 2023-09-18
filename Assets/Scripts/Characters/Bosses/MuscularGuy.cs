using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class MuscularGuy : Boss
    {
        [Header("Special Attack")]
        [SerializeField] private GameObject dashIndicatorPrefab;
        [SerializeField] private float focusDuration = 1.0f;
        [SerializeField] private float dashSpeed = 10.0f;
        [SerializeField] private float specialAttackCooldown = 5f;

        private bool isDashing = false;
        private GameObject currentDashIndicator;
        private Vector2 dashTargetPosition;
        private float timeSinceLastSpecialAttack = 0f;

        protected override void Start()
        {
            base.Start();
            currentState = BossState.Moving;
            isDashing = false;
        }

        protected override void Update()
        {
            base.Update();

            timeSinceLastSpecialAttack += Time.deltaTime;

            if (timeSinceLastSpecialAttack > specialAttackCooldown && !isDashing)
            {
                currentState = BossState.SpecialAttack;
                timeSinceLastSpecialAttack = 0f;
            }
        }

        protected override void IdleBehavior()
        {
        }

        protected override void AttackBehavior()
        {
        }

        protected override void SpecialAttackBehavior()
        {
            StartCoroutine(ExecuteSpecialAttack());
            currentState = BossState.Moving;
        }

        protected override void MoveBehavior()
        {
            if (isDashing)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            Vector2 directionToChef = (chefTransform.position - transform.position).normalized;
            rb.velocity = directionToChef * characterData.moveSpeed;
        }

        private IEnumerator ExecuteSpecialAttack()
        {
            isDashing = true;

            // Focus
            rb.velocity = Vector2.zero; // Stop the boss
            CreateDashIndicator();

            yield return new WaitForSeconds(focusDuration);

            Destroy(currentDashIndicator);

            // Dash
            float dashDuration = Vector2.Distance(transform.position, dashTargetPosition) / dashSpeed;
            float startTime = Time.time;
            Vector2 startPosition = transform.position;

            while (Time.time < startTime + dashDuration)
            {
                float t = (Time.time - startTime) / dashDuration;
                transform.position = Vector2.Lerp(startPosition, dashTargetPosition, t);
                yield return null;
            }

            isDashing = false;
        }

        private void CreateDashIndicator()
        {
            if (currentDashIndicator != null)
            {
                Destroy(currentDashIndicator);
            }

            dashTargetPosition = chefTransform.position;
            Vector2 directionToChef = dashTargetPosition - (Vector2)transform.position;
            float distanceToChef = directionToChef.magnitude;

            Vector2 indicatorPosition = transform.position + (Vector3)(directionToChef * 0.5f);
            float angle = Mathf.Atan2(directionToChef.y, directionToChef.x) * Mathf.Rad2Deg;

            currentDashIndicator = Instantiate(dashIndicatorPrefab, indicatorPosition, Quaternion.Euler(0, 0, angle - 90));
            currentDashIndicator.transform.localScale = new Vector3(1, distanceToChef, 1);
        }
    }
}
