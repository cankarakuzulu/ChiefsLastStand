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
            if (Time.time >= lastAttackTime + chef.ChefData.attackRate)
            {
                isReadyToAttack = true;
            }

            if (isReadyToAttack)
            {
                AttackNearestCustomer();
            }
        }
        private void AttackNearestCustomer()
        {
            Customer nearestCustomer = FindNearestCustomer();

            if (nearestCustomer)
            {
                GameObject burgerGO = Instantiate(burgerPrefab, chef.transform.position, Quaternion.identity);

                Burger burger = burgerGO.GetComponent<Burger>();
                Vector2 directionToCustomer = nearestCustomer.transform.position - transform.position;
                burger.SetInitialDirection(directionToCustomer.normalized);

                lastAttackTime = Time.time;
                isReadyToAttack = false;
            }
        }
        private Customer FindNearestCustomer()
        {
            Customer[] customers = FindObjectsOfType<Customer>();
            Customer nearestCustomer = null;
            float minDistance = chef.ChefData.attackRange;

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
