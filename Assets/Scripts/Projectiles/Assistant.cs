using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using nopact.ChefsLastStand.Gameplay.Projectiles;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    public class Assistant : MonoBehaviour
    {
        [SerializeField] private GameObject assistantBurgerPrefab;

        private AssistantSkill skill;
        private Chef chef;
        private float lastAttackTime;

        public void Initialize(AssistantSkill skill, Chef chef)
        {
            this.skill = skill;
            this.chef = chef;
        }

        private void Update()
        {
            FollowChef();
            HandleAttack();
        }

        private void FollowChef()
        {
            float distanceToChef = Vector3.Distance(chef.transform.position, transform.position);
            if (distanceToChef > skill.followDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, chef.transform.position, chef.ChefData.moveSpeed * Time.deltaTime);
            }
        }

        private void HandleAttack()
        {
            if (Time.time >= lastAttackTime + skill.attackCooldown)
            {
                AttackNearestCustomer();
            }
        }

        private void AttackNearestCustomer()
        {
            Customer nearestCustomer = FindNearestCustomer();

            if (nearestCustomer)
            {
                GameObject burgerGO = Instantiate(assistantBurgerPrefab, transform.position, Quaternion.identity);
                Burger burger = burgerGO.GetComponent<Burger>();
                Vector2 directionToCustomer = nearestCustomer.transform.position - transform.position;
                burger.SetInitialDirection(directionToCustomer.normalized);

                lastAttackTime = Time.time;
            }
        }

        private Customer FindNearestCustomer()
        {
            Customer[] customers = FindObjectsOfType<Customer>();
            Customer nearestCustomer = null;
            float minDistance = Mathf.Infinity;  // Setting to infinity to ensure any distance will be shorter

            foreach (var customer in customers)
            {
                float distance = Vector2.Distance(transform.position, customer.transform.position);
                if (distance < minDistance)
                {
                    nearestCustomer = customer;
                    minDistance = distance;
                }
            }

            return nearestCustomer;
        }
    }
}
