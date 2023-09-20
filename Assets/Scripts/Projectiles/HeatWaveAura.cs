using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand
{
    public class HeatWaveAura : MonoBehaviour
    {
        private float damagePerSecond;
        private HashSet<IAttackable> entitiesInAura = new HashSet<IAttackable>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IAttackable entity = collision.GetComponent<IAttackable>();
            if (entity != null)
            {
                entitiesInAura.Add(entity);
                StartCoroutine(DamageEntity(entity));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IAttackable entity = collision.GetComponent<IAttackable>();
            if (entity != null)
            {
                entitiesInAura.Remove(entity);
            }
        }

        private IEnumerator DamageEntity(IAttackable entity)
        {
            while (entitiesInAura.Contains(entity))
            {
                entity.TakeDamage(damagePerSecond);
                yield return new WaitForSeconds(1f);
            }
        }

        public void SetDamage(float damage)
        {
            damagePerSecond = damage;
        }
    }
}
