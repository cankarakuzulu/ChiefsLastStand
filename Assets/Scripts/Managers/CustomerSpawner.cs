using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand
{
    [System.Serializable]
    public class CustomerType
    {
        public GameObject customerPrefab;
        public float spawnWeight;
    }
    public class CustomerSpawner : MonoBehaviour
    {
        public List<CustomerType> customerTypes;
        public float spawnInterval = 5.0f;

        private GameObject[] spawnPoints;
        private float totalSpawnWeight;

        void Start()
        {
            totalSpawnWeight = 0;
            foreach (var customerType in customerTypes)
            {
                totalSpawnWeight += customerType.spawnWeight;
            }

            spawnPoints = GameObject.FindGameObjectsWithTag("CustomerSpawnPoint");

            StartCoroutine(SpawnCustomers());
        }

        IEnumerator SpawnCustomers()
        {
            while (true)
            {
                foreach (GameObject spawnPoint in spawnPoints)
                {
                    GameObject customerToSpawn = SelectRandomCustomer();

                    Instantiate(customerToSpawn, spawnPoint.transform.position, Quaternion.identity);
                }

                yield return new WaitForSeconds(spawnInterval);
            }
        }

        GameObject SelectRandomCustomer()
        {
            float randomNumber = Random.Range(0, totalSpawnWeight);
            float sum = 0;

            foreach (var customerType in customerTypes)
            {
                sum += customerType.spawnWeight;
                if (randomNumber <= sum)
                {
                    return customerType.customerPrefab;
                }
            }

            return null;
        }
    }
}
