using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand
{
    public enum StageEventType
    {
        Spawn,
        BossSpawn
    }

    [System.Serializable]
    public class StageEvent
    {
        public StageEventType eventType;
        public float triggerTime; //seconds
        public GameObject bossPrefab;
        public List<CustomerType> eventCustomerTypes;
        public bool isFinalBoss;
        public float spawnInterval;
    }

    [System.Serializable]
    public class CustomerType
    {
        public GameObject customerPrefab;
        public float spawnWeight;
    }

    public class CustomerSpawner : MonoBehaviour
    {
        public List<StageEvent> stageEvents;

        private GameObject[] spawnPoints;
        private float totalSpawnWeight;
        private bool isBossFightMode = false;
        private int nextEventIndex = 0;
        private List<CustomerType> currentEventCustomerTypes;
        private float currentEventSpawnInterval;


        private void Start()
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("CustomerSpawnPoint");
            currentEventCustomerTypes = stageEvents[0].eventCustomerTypes;
            currentEventSpawnInterval = stageEvents[0].spawnInterval;
            ResetSpawnWeights(currentEventCustomerTypes);
            StartCoroutine(SpawnCustomers());
        }

        private void OnEnable()
        {
            Boss.OnBossDefeated += HandleBossDefeat;
        }

        private void OnDisable()
        {
            Boss.OnBossDefeated -= HandleBossDefeat;
        }

        private void Update()
        {
            if (nextEventIndex < stageEvents.Count)
            {
                float currentTime = FindObjectOfType<GameTimer>().GetElapsedTime();
                StageEvent nextEvent = stageEvents[nextEventIndex];
                if (currentTime >= nextEvent.triggerTime)
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
                    GameObject customerToSpawn = SelectRandomCustomer(currentEventCustomerTypes);
                    Instantiate(customerToSpawn, spawnPoint.transform.position, Quaternion.identity);
                }

                yield return new WaitForSeconds(currentEventSpawnInterval);
            }
        }

        private void ProcessEvent(StageEvent currentEvent)
        {
            currentEventCustomerTypes = currentEvent.eventCustomerTypes;
            currentEventSpawnInterval = currentEvent.spawnInterval;
            ResetSpawnWeights(currentEventCustomerTypes);

            if (currentEvent.eventType == StageEventType.BossSpawn)
            {
                StartBossFight(currentEvent.bossPrefab);
                FindObjectOfType<GameTimer>().PauseTimer();
            }
            else if (currentEvent.eventType == StageEventType.Spawn)
            {
                isBossFightMode = false;
            }
        }

        private void StartBossFight(GameObject bossPrefab)
        {
            isBossFightMode = true;
            Instantiate(bossPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        }

        private void HandleBossDefeat()
        {
            isBossFightMode = false;
            FindObjectOfType<GameTimer>().ResumeTimer();

            if (nextEventIndex < stageEvents.Count)
            {
                StageEvent nextEvent = stageEvents[nextEventIndex];
                if (nextEvent.eventType == StageEventType.Spawn)
                {
                    currentEventCustomerTypes = nextEvent.eventCustomerTypes;
                    currentEventSpawnInterval = nextEvent.spawnInterval;
                    ResetSpawnWeights(currentEventCustomerTypes);
                }
            }

            if (stageEvents[nextEventIndex - 1].isFinalBoss)
            {
                StageUIController.Instance.ShowEndGameUI();
            }
        }
    }
}
