using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class Character : MonoBehaviour
    {
        public CharacterData characterData;

        protected float health;

        protected virtual void Start()
        {
            if (characterData == null)
            {
                Debug.LogError("CharacterData is not assigned for " + gameObject.name);
                return;
            }
            health = characterData.health;
        }

        public virtual void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
