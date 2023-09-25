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
        private Dictionary<IAttackable, Coroutine> activeAttackables = new Dictionary<IAttackable, Coroutine>();

        private void Start()
        {
            Destroy(gameObject, 5f);   
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IAttackable attackable = other.GetComponent<IAttackable>();
            if (attackable != null)
            {
                if (attackable is Customer customer)
                {
                    customer.ApplySlowEffect(slowEffect);
                }

                Coroutine damageRoutine = StartCoroutine(DamageOverTime(attackable));
                activeAttackables.Add(attackable, damageRoutine);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IAttackable attackable = other.GetComponent<IAttackable>();
            if (attackable != null && activeAttackables.TryGetValue(attackable, out Coroutine damageRoutine))
            {
                StopCoroutine(damageRoutine);
                activeAttackables.Remove(attackable);
            }
        }

        public void Initialize(float damagePerSecond, float slowEffect)
        {
            this.damagePerSecond = damagePerSecond;
            this.slowEffect = slowEffect;
        }

        private IEnumerator DamageOverTime(IAttackable attackable)
        {
            while (true)
            {
                attackable.TakeDamage(damagePerSecond);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
