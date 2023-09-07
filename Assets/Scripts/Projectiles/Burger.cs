using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Projectiles
{
    public class Burger : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private Chef chef;
        private float damage;
        private Vector2 moveDirection;

        private void Start()
        {
            chef = FindObjectOfType<Chef>();
            if (chef != null)
            {
                damage = chef.ChefData.damage;
            }
            else
            {
                Debug.LogError("Chef not found!");
            }
        }

        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Customer customer = other.GetComponent<Customer>();
            if (customer)
            {
                customer.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        public void SetInitialDirection(Vector2 direction)
        {
            moveDirection = direction;
        }

        private void Move()
        {
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
        }
    }
}
