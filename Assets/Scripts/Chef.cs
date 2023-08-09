using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.ChefData;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public class Chef : MonoBehaviour
    {
        [SerializeField] private ChefData chefData;
        public ChefData ChefData => chefData;
        
    }
}
