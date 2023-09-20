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
        private Rigidbody2D rb;

        public void Initialize(AssistantSkill skill, Chef chef)
        {
            this.skill = skill;
            this.chef = chef;
            rb = GetComponent<Rigidbody2D>();
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
                Vector2 directionToChef = (chef.transform.position - transform.position).normalized;
                rb.velocity = directionToChef * chef.ChefData.moveSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero; 
            }
        }

        private void HandleAttack()
        {
            if (Time.time >= lastAttackTime + skill.attackCooldown)
            {
                AttackNearestAttackable();
            }
        }

        private void AttackNearestAttackable()
        {
            IAttackable nearestAttackable = AttackableSearch.FindNearestAttackable(transform.position, chef.ChefData.attackRange);

            if (nearestAttackable != null)
            {
                GameObject burgerGO = Instantiate(assistantBurgerPrefab, transform.position, Quaternion.identity);
                Burger burger = burgerGO.GetComponent<Burger>();
                Vector2 directionToAttackable = nearestAttackable.GetTransform().position - transform.position;
                burger.SetInitialDirection(directionToAttackable.normalized);

                lastAttackTime = Time.time;
            }
        }
    }
}
