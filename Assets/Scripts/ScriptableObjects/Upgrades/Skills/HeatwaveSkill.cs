using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Skill Upgrades/Heat Wave Skill")]
    public class HeatwaveSkill : Upgrade
    {
        public GameObject heatWaveCirclePrefab;
        public float damagePerSecond;


        public override void ApplyUpgrade(Chef chef)
        {
            var existingAura = chef.transform.GetComponentInChildren<HeatWaveAura>();

            if (existingAura != null)
            {
                Destroy(existingAura.gameObject);
            }

            GameObject newHeatWaveAura = Instantiate(heatWaveCirclePrefab, chef.transform.position, Quaternion.identity, chef.transform);
            var auraScript = newHeatWaveAura.GetComponent<HeatWaveAura>();
            auraScript.SetDamage(damagePerSecond);
        }
    }
}
