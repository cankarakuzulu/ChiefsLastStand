using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    public class PizzaBehaviour : MonoBehaviour
    {
        private float damage;

        public void Initialize(float dmg)
        {
            damage = dmg;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Customer"))
            {
                var customer = collision.GetComponent<Customer>();
                if (customer != null)
                {
                    customer.TakeDamage(damage);
                }
            }
        }
    }
}