using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Data
{
    [CreateAssetMenu(fileName = "NewStageData", menuName = "GameData/Stage Data")]
    public class StageData : ScriptableObject
    {
        public string stageName; 
        public string sceneName;
        public int stageNumber;
        public Sprite stageImage;
    }
}
 