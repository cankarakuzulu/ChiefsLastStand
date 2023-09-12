using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    public class Funyun : MonoBehaviour
    {
        private Vector3 direction;
        private bool isReturning = false;
        private FunyunSkill skill;
        private Transform chefTransform;

        private void Update()
        {
            transform.position += direction * skill.speed * Time.deltaTime;

            // Check for max range and switch direction towards chef
            if (Vector3.Distance(transform.position, chefTransform.position) > skill.maxRange && !isReturning)
            {
                direction = (chefTransform.position - transform.position).normalized;
                isReturning = true;
            }

            if (IsOutsideCameraView())
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Customer"))
            {
                other.GetComponent<Customer>().TakeDamage(skill.damage);
            }
        }

        public void Initialize(FunyunSkill skill, Vector3 chefPosition)
        {
            this.skill = skill;
            Customer target = FindNearestCustomer();

            if (target != null)
            {
                direction = (target.transform.position - transform.position).normalized;
                chefTransform = FindObjectOfType<Chef>().transform;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private bool IsOutsideCameraView()
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            return screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1;
        }

        private Customer FindNearestCustomer()
        {
            Customer[] customers = FindObjectsOfType<Customer>();
            Customer nearest = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (Customer customer in customers)
            {
                if (!customer.transform.IsInCameraView())
                {
                    continue;
                }

                float dist = Vector3.Distance(customer.transform.position, currentPos);
                if (dist < minDist)
                {
                    nearest = customer;
                    minDist = dist;
                }
            }
            return nearest;
        }
    }
}
