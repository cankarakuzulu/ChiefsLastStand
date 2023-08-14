using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.CustomerData;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public abstract class Customer : Character
    {
        protected enum CustomerState
        {
            Chasing,
            Attacking
        }

        protected CustomerState currentState;
        protected Transform chefTransform;
        protected float lastAttackTime = float.MinValue;

        protected override void Start()
        {
            base.Start();

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
            UpdateState();
            switch (currentState)
            {
                case CustomerState.Chasing:
                    ChaseChef();
                    break;
                case CustomerState.Attacking:
                    Attack();
                    break;
            }
        }

        protected abstract void UpdateState();
        protected abstract void ChaseChef();
        protected abstract void Attack();
    }
}
