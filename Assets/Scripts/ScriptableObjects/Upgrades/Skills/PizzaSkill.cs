using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Skill Upgrades/Pizza Skill")]
    public class PizzaSkill : Upgrade
    {
        public int pizzaCount;
        public float damage;
        public float rotationSpeed;
        public GameObject pizzaPrefab;

        public override void ApplyUpgrade(Chef chef)
        {
            chef.ActivatePizzaSkill(this);
        }
    }
}
