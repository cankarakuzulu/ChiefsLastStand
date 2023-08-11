using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class MeleeCustomer : Customer
    {
        protected override void ChaseChef()
        {
            if (chefTransform == null) return;

            Vector2 directionToChef = (chefTransform.position - transform.position).normalized;
            transform.position += (Vector3)directionToChef * characterData.moveSpeed * Time.deltaTime;
        }

        protected override void Attack()
        {
            // Melee attack will be handled in OnCollisionEnter2D
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Chef"))
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
    }
}
