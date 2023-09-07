using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class MeleeCustomer : Customer
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Chef"))
            {
                currentState = CustomerState.Attacking;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Chef") && currentState == CustomerState.Attacking)
            {
                if (Time.time >= lastAttackTime + characterData.attackCooldown)
                {
                    Debug.Log(gameObject.name + " attacking: " + collision.gameObject.name);
                    Chef chef = collision.gameObject.GetComponent<Chef>();
                    chef.TakeDamage(characterData.damage);
                    lastAttackTime = Time.time;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Chef"))
            {
                currentState = CustomerState.Chasing;
            }
        }

        protected override void UpdateState()
        {
            // For melee customers, the state is determined by collisions.
            // The state is set to Attacking during OnCollisionEnter2D 
            // and back to Chasing during OnCollisionExit2D.
        }

        protected override void ChaseChef()
        {
            if (chefTransform == null) return;

            Vector2 directionToChef = (chefTransform.position - transform.position).normalized;
            transform.position += (Vector3)directionToChef * currentSpeed * Time.deltaTime;
        }

        protected override void Attack()
        {
            // Melee attack will be handled in OnCollisionEnter2D
        }
    }
}
