using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand
{
    public class HeatWaveAura : MonoBehaviour
    {
        private float damagePerSecond;
        private HashSet<Customer> customersInAura = new HashSet<Customer>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Customer customer = collision.GetComponent<Customer>();
            if (customer)
            {
                customersInAura.Add(customer);
                StartCoroutine(DamageCustomer(customer));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Customer customer = collision.GetComponent<Customer>();
            if (customer)
            {
                customersInAura.Remove(customer);
            }
        }

        private IEnumerator DamageCustomer(Customer customer)
        {
            while (customersInAura.Contains(customer))
            {
                customer.TakeDamage(damagePerSecond);
                yield return new WaitForSeconds(1f);
            }
        }

        public void SetDamage(float damage)
        {
            damagePerSecond = damage;
        }
    }
}
