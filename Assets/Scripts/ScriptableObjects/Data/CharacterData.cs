using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ChefsLastStand/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        public float health;
        public float damage;
        public float moveSpeed;
        public float attackCooldown;
    }
}
