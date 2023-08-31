using System;
using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.CustomerData;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public abstract class Customer : Character
    {
        [SerializeField] private GameObject coinPrefab;

        private float originalMoveSpeed;
        private bool isSlowed = false;

        protected enum CustomerState
        {
            Chasing,
            Attacking
        }

        protected CustomerState currentState;
        protected Transform chefTransform;
        protected float lastAttackTime = float.MinValue;

        protected override void Start()
        {
            base.Start();
            originalMoveSpeed = characterData.moveSpeed;

            GameObject chefObject = GameObject.FindWithTag("Chef");
            if (chefObject != null)
            {
                chefTransform = chefObject.transform;
            }
            else
            {
                Debug.LogError("Chef object not found. Make sure the Chef is tagged appropriately.");
            }
        }

        protected virtual void Update()
        {
            UpdateState();
            switch (currentState)
            {
                case CustomerState.Chasing:
                    ChaseChef();
                    break;
                case CustomerState.Attacking:
                    Attack();
                    break;
            }
        }

        public void ApplySlowEffect(float slowEffect)
        {
            if (isSlowed) return;

            StartCoroutine(SlowEffectCoroutine(slowEffect));
        }

        private IEnumerator SlowEffectCoroutine(float slowEffect)
        {
            isSlowed = true;

            // Reduce the move speed
            characterData.moveSpeed *= (1f - slowEffect);

            // Stay slow for 5 seconds
            yield return new WaitForSeconds(5f);

            // Restore the original move speed
            characterData.moveSpeed = originalMoveSpeed;

            isSlowed = false;
        }

        protected override void Die()
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        protected abstract void UpdateState();
        protected abstract void ChaseChef();
        protected abstract void Attack();
    }
}
