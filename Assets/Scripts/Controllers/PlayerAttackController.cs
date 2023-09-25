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
            IAttackable nearestAttackable = AttackableSearch.FindNearestAttackable(transform.position, chef.ChefData.attackRange);

            if (nearestAttackable != null)
            {
                GameObject burgerGO = Instantiate(burgerPrefab, chef.transform.position, Quaternion.identity);

                Burger burger = burgerGO.GetComponent<Burger>();
                Vector2 directionToAttackable = nearestAttackable.GetTransform().position - transform.position;
                burger.SetInitialDirection(directionToAttackable.normalized);

                lastAttackTime = Time.time;
                isReadyToAttack = false;
            }
        }
    }
}
