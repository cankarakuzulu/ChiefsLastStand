using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Projectiles
{
    public class Burger : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private Vector2 moveDirection;

        private void Update()
        {
            Move();
        }
        public void SetInitialDirection(Vector2 direction)
        {
            moveDirection = direction;
        }

        private void Move()
        {
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Customer customer = other.GetComponent<Customer>();
            if (customer)
            {
                Destroy(customer.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
