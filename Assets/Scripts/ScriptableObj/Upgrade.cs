using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    public abstract class Upgrade : ScriptableObject
    {
        public string upgradeName;
        public Sprite upgradeIcon;
        public string description;

        public abstract void ApplyUpgrade(Chef chef);
    }
}


