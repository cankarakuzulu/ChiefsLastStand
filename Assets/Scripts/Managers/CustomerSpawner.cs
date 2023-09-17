using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand
{
    public enum StageEventType
    {
        EnemyRush,
        BossSpawn
    }

    [System.Serializable]
    public class StageEvent
    {
        public StageEventType eventType;
        public float triggerTime;  // In minutes
        public GameObject bossPrefab;  // Only used if eventType is BossSpawn
    }
    public class CustomerSpawner : MonoBehaviour
    {
        [System.Serializable]
        public class CustomerType
        {
            public GameObject customerPrefab;
            public float spawnWeight;
        }

        public List<CustomerType> customerTypes;
        public List<CustomerType> enemyRushTypes;
        public List<StageEvent> stageEvents;
        public float spawnInterval = 5.0f;
        public float enemyRushSpawnInterval = 3.0f;

        private GameObject[] spawnPoints;
        private float totalSpawnWeight;
        private bool isBossFightMode = false;
        private int nextEventIndex = 0;

        private void Start()
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("CustomerSpawnPoint");

            ResetSpawnWeights(customerTypes);
            StartCoroutine(SpawnCustomers());
        }

        private void Update()
        {
            if (nextEventIndex < stageEvents.Count)
            {
                float currentTime = FindObjectOfType<GameTimer>().GetElapsedTime();
                StageEvent nextEvent = stageEvents[nextEventIndex];
                if (currentTime >= nextEvent.triggerTime * 60)  // Convert to seconds
                {
                    ProcessEvent(nextEvent);
                    nextEventIndex++;
                }
            }
        }

        private void ResetSpawnWeights(List<CustomerType> types)
        {
            totalSpawnWeight = 0;
            foreach (var customerType in types)
            {
                totalSpawnWeight += customerType.spawnWeight;
            }
        }

        private GameObject SelectRandomCustomer(List<CustomerType> types)
        {
            float randomNumber = Random.Range(0, totalSpawnWeight);
            float sum = 0;

            foreach (var customerType in types)
            {
                sum += customerType.spawnWeight;
                if (randomNumber <= sum)
                {
                    return customerType.customerPrefab;
                }
            }

            return null;
        }

        private IEnumerator SpawnCustomers()
        {
            while (true)
            {
                if (!isBossFightMode)
                {
                    GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    GameObject customerToSpawn = SelectRandomCustomer(customerTypes);
                    Instantiate(customerToSpawn, spawnPoint.transform.position, Quaternion.identity);
                }

                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void ProcessEvent(StageEvent stageEvent)
        {
            switch (stageEvent.eventType)
            {
                case StageEventType.EnemyRush:
                    StartEnemyRush();
                    break;
                case StageEventType.BossSpawn:
                    StartBossFight(stageEvent.bossPrefab);
                    break;
            }
        }

        private void StartEnemyRush()
        {
            spawnInterval = enemyRushSpawnInterval;
            ResetSpawnWeights(enemyRushTypes);
            customerTypes = enemyRushTypes;
        }

        private void StartBossFight(GameObject bossPrefab)
        {
            isBossFightMode = true;
            Instantiate(bossPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        }
    }
}
