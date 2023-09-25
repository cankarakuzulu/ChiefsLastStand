using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Data
{
    [CreateAssetMenu(fileName = "NewRestaurantData", menuName = "GameData/Restaurant Data")]
    public class RestaurantData : ScriptableObject
    {
        public string restaurantName;
        public int restaurantNo;
        public Sprite restaurantImage;
        public List<StageData> stages;
    }
}
