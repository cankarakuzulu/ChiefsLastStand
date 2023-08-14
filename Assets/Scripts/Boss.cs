using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public abstract class Boss : Character
    {
        protected enum BossState
        {
            Idle,
            Attacking,
            SpecialAttack,
            Moving
        }

        protected BossState currentState;
        protected float lastAttackTime;
        protected Transform chefTransform;

        protected override void Start()
        {
            base.Start();
            currentState = BossState.Idle;

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
            switch (currentState)
            {
                case BossState.Idle:
                    IdleBehavior();
                    break;
                case BossState.Attacking:
                    AttackBehavior();
                    break;
                case BossState.SpecialAttack:
                    SpecialAttackBehavior();
                    break;
                case BossState.Moving:
                    MoveBehavior();
                    break;
            }
        }

        protected abstract void IdleBehavior();
        protected abstract void AttackBehavior();
        protected abstract void SpecialAttackBehavior();
        protected abstract void MoveBehavior();
    }
}
