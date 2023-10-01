using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public abstract class Character : MonoBehaviour
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
            if(this != null) Debug.Log(this.name + "'s health is: " + health);
        }

        public virtual void Heal(float hp)
        {
            health += hp;
            if (health >= characterData.health)
            {
                health = characterData.health;
            }
            Debug.Log(this.name + "'s health is: " + health);
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
