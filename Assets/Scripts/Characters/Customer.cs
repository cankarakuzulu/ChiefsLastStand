using System;
using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.CustomerData;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public abstract class Customer : Character, IAttackable
    {
        [SerializeField] private GameObject coinPrefab;

        private bool isSlowed = false;
        private SpriteRenderer spriteRenderer;
        protected CustomerState currentState;
        protected Transform chefTransform;
        protected float lastAttackTime = float.MinValue;
        protected float currentSpeed;
        protected Rigidbody2D rb;

        protected enum CustomerState
        {
            Chasing,
            Attacking
        }

        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody2D>();
            currentSpeed = characterData.moveSpeed;
            spriteRenderer = GetComponent<SpriteRenderer>();

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

        private void Awake()
        {
            AttackableManager.Register(this);
        }

        private void OnDestroy()
        {
            AttackableManager.DeRegister(this);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void ApplySlowEffect(float slowEffect)
        {
            if (isSlowed) return;
            Debug.Log("Slow Effect Value: " + slowEffect);
            StartCoroutine(SlowEffectCoroutine(slowEffect));
        }

        private IEnumerator SlowEffectCoroutine(float slowEffect)
        {
            isSlowed = true;
            Color currentColor = spriteRenderer.color;
            Debug.Log("Entering slow coroutine. Original Speed: " + currentSpeed);

            // Reduce the move speed
            currentSpeed *= (1f - slowEffect);
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.6f); //to see slowed down customers
            Debug.Log("---------------------------------------Modified Speed: " + currentSpeed);
            // Stay slow for 5 seconds
            yield return new WaitForSeconds(5f);

            // Restore the original move speed
            currentSpeed = characterData.moveSpeed;
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f); //restore the alpha value
            Debug.Log("Coroutine finished. Speed restored to: " + currentSpeed);
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
