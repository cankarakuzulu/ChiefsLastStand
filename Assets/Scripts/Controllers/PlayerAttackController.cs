using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.ChefData;
using nopact.ChefsLastStand.Gameplay.Entities;
using nopact.ChefsLastStand.Gameplay.Projectiles;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Controls
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private GameObject burgerPrefab;

        private Chef chef;
        private float lastAttackTime = float.MinValue;
        private bool isReadyToAttack = true;

        private void Start()
        {
            chef = GetComponent<Chef>();
        }
        
        public void HandleAttack()
        {
            if (Time.time >= lastAttackTime + chef.ChefData.attackCooldown)
            {
                isReadyToAttack = true;
            }

            if (isReadyToAttack)
            {
                AttackNearestAttackable();
            }
        }
        private void AttackNearestAttackable()
        {
            for (int i = 0; i < chef.ChefData.burgerCount; i++)
            {
                float delay = i * 0.2f;
                StartCoroutine(ThrowBurger(delay));
            }

            lastAttackTime = Time.time;
            isReadyToAttack = false;
        }

        private IEnumerator ThrowBurger(float delay)
        {
            yield return new WaitForSeconds(delay);
            IAttackable nearestAttackable = AttackableSearch.FindNearestAttackable(transform.position, chef.ChefData.attackRange);

            if (nearestAttackable != null)
            {
                Burger burger = BurgerPool.Instance.GetBurger();
                burger.SetDamage(chef.ChefData.damage);
                burger.transform.position = chef.transform.position;

                Vector2 directionToAttackable = nearestAttackable.GetTransform().position - transform.position;
                burger.SetInitialDirection(directionToAttackable.normalized);

                burger.gameObject.SetActive(true);
            }
        }
    }
}
