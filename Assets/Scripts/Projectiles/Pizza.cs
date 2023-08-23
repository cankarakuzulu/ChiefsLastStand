using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Upgrades
{
    public class Pizza : MonoBehaviour
    {
        private GameObject[] activePizzas;
        private float rotationSpeed;
        private float damage;
        private Coroutine pizzaSkillCoroutine;

        public void ActivatePizzaSkill(PizzaSkill skill)
        {
            if (pizzaSkillCoroutine != null)
            {
                StopCoroutine(pizzaSkillCoroutine);
                DespawnPizzas();
            }
            rotationSpeed = skill.rotationSpeed;
            damage = skill.damage;
            pizzaSkillCoroutine = StartCoroutine(PizzaSkillRoutine(skill));
        }

        private IEnumerator PizzaSkillRoutine(PizzaSkill skill)
        {
            while (true)
            {
                SpawnPizzas(skill);
                yield return new WaitForSeconds(5);
                DespawnPizzas();
                yield return new WaitForSeconds(3);
            }
        }

        private void SpawnPizzas(PizzaSkill skill)
        {
            activePizzas = new GameObject[skill.pizzaCount];
            float angleDifference = 360f / skill.pizzaCount;
            float spawnDistance = 1.3f;
            for (int i = 0; i < skill.pizzaCount; i++)
            {
                var position = RotatePointAroundPivot(transform.position + (Vector3.up * spawnDistance), transform.position, Vector3.forward * angleDifference * i);
                var pizzaInstance = Instantiate(skill.pizzaPrefab, position, Quaternion.identity, transform);
                pizzaInstance.GetComponent<PizzaBehaviour>().Initialize(damage);
                activePizzas[i] = pizzaInstance;
            }
        }

        private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            return Quaternion.Euler(angles) * (point - pivot) + pivot;
        }

        private void Update()
        {
            if (activePizzas != null)
            {
                foreach (var pizza in activePizzas)
                {
                    pizza.transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                }
            }
        }

        private void DespawnPizzas()
        {
            if (activePizzas != null)
            {
                foreach (var pizza in activePizzas)
                {
                    Destroy(pizza);
                }
                activePizzas = null;
            }
        }

        private void OnDisable()
        {
            if (pizzaSkillCoroutine != null)
            {
                StopCoroutine(pizzaSkillCoroutine);
            }
            DespawnPizzas();
        }
    } 
}
