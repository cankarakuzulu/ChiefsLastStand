using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using nopact.ChefsLastStand.Upgrades;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Projectiles
{
    public class HotSauceBottle : MonoBehaviour
    {
        public float speed = 5f;

        private HotSauceSkill skill;
        private Vector3 targetPosition;

        public void Initialize(HotSauceSkill skill, Vector3 targetPosition)
        {
            this.skill = skill;
            this.targetPosition = targetPosition;
        }

        void Update()
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                CreateHotSauceArea();
                Destroy(gameObject);
            }
        }

        void CreateHotSauceArea()
        {
            GameObject hotSauceArea = Instantiate(skill.hotSauceAreaPrefab, targetPosition, Quaternion.identity);
            HotSauceArea areaScript = hotSauceArea.GetComponent<HotSauceArea>();
            areaScript.Initialize(skill.damagePerSecond, skill.slowEffect);
        }
    }
}
