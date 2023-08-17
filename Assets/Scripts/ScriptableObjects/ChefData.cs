using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Data.ChefData
{
    [CreateAssetMenu(fileName = "ChefData", menuName = "ChefsLastStand/ChefData", order = 0)]
    public class ChefData : CharacterData
    {
        public float attackRange;
        public float defense;
        public float evasion;
        public float maxHealth;
        public float pickUpArea;
        public int burgerCount;
    }
}