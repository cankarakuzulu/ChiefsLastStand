using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand
{
    public static class AttackableSearch
    {
        public static IAttackable FindNearestAttackable(Vector3 fromPosition, float range)
        {
            List<IAttackable> attackables = AttackableManager.GetAllAttackables();
            IAttackable nearestAttackable = null;
            float minDistance = range;

            foreach (var attackable in attackables)
            {
                Transform attackableTransform = attackable.GetTransform();

                if (!attackableTransform.IsInCameraView())
                {
                    continue;
                }

                float distance = Vector2.Distance(fromPosition, attackableTransform.position);
                if (distance < minDistance)
                {
                    nearestAttackable = attackable;
                    minDistance = distance;
                }
            }

            return nearestAttackable;
        }

        public static IAttackable FindRandomAttackable()
        {
            List<IAttackable> attackables = AttackableManager.GetAllAttackables();
            List<IAttackable> visibleAttackables = new List<IAttackable>();

            foreach (var attackable in attackables)
            {
                Transform attackableTransform = attackable.GetTransform();

                if (attackableTransform.IsInCameraView())
                {
                    visibleAttackables.Add(attackable);
                }
            }

            if (visibleAttackables.Count == 0) return null;

            int randomIndex = UnityEngine.Random.Range(0, visibleAttackables.Count);
            return visibleAttackables[randomIndex];
        }
    }
}
