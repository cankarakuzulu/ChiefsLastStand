using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Projectiles
{
    public class DamageArea : MonoBehaviour
    {
        public float damage;

        private void Start()
        {
            Destroy(gameObject, 0.5f);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Customer"))
            {
                other.GetComponent<Customer>().TakeDamage(damage);
            }
        }
    }
}
