using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand
{
    public class AttackableManager : MonoBehaviour
    {
        private static List<IAttackable> attackableObjects = new List<IAttackable>();

        public static void Register(IAttackable attackable)
        {
            if (!attackableObjects.Contains(attackable))
            {
                attackableObjects.Add(attackable);
            }
        }

        public static void DeRegister(IAttackable attackable)
        {
            if (attackableObjects.Contains(attackable))
            {
                attackableObjects.Remove(attackable);
            }
        }

        public static List<IAttackable> GetAllAttackables()
        {
            return attackableObjects;
        }
    }
}
