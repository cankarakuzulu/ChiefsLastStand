using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Projectiles
{
    public class CustomerProjectile : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private Vector2 moveDirection;
        private float damage;

        private void Update()
        {
            Move();
        }

        public void SetMoveDirection(Vector2 direction)
        {
            moveDirection = direction.normalized;
        }

        public void SetDamage(float damageValue)
        {
            damage = damageValue;
        }

        private void Move()
        {
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Chef"))
            {
                Chef chef = other.GetComponent<Chef>();
                chef.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
