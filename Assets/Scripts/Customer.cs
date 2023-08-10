using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed = 1f;

        private Transform chefTransform;

        protected virtual void Start()
        {
            GameObject chefObject = GameObject.FindWithTag("Chef");
            if (chefObject != null)
            {
                chefTransform = chefObject.transform;
            }
            else
            {
                Debug.LogError("Chef object not found. Make sure the Chef is tagged appropriately.");
            }
        }
        protected virtual void Update()
        {
            ChaseChef();
        }

        protected virtual void ChaseChef()
        {
            if (chefTransform == null) return;

            Vector2 directionToChef = (chefTransform.position - transform.position).normalized;
            transform.position += (Vector3)directionToChef * moveSpeed * Time.deltaTime;
        }
    }
}
