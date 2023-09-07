using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Projectiles
{
    public class HotSauceArea : MonoBehaviour
    {
        private float damagePerSecond;
        private float slowEffect;
        private Dictionary<Customer, Coroutine> activeCustomers = new Dictionary<Customer, Coroutine>();

        private void Start()
        {
            Destroy(gameObject, 5f);   
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Customer"))
            {
                Customer customer = other.gameObject.GetComponent<Customer>();
                customer.ApplySlowEffect(slowEffect);
                Coroutine damageRoutine = StartCoroutine(DamageOverTime(customer));
                activeCustomers.Add(customer, damageRoutine);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Customer"))
            {
                Customer customer = other.gameObject.GetComponent<Customer>();
                if (activeCustomers.TryGetValue(customer, out Coroutine damageRoutine))
                {
                    StopCoroutine(damageRoutine);
                    activeCustomers.Remove(customer);
                }
            }
        }

        public void Initialize(float damagePerSecond, float slowEffect)
        {
            this.damagePerSecond = damagePerSecond;
            this.slowEffect = slowEffect;
        }

        private IEnumerator DamageOverTime(Customer customer)
        {
            while (true)
            {
                customer.TakeDamage(damagePerSecond);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
