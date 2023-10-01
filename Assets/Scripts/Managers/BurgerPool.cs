using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Projectiles;
using UnityEngine;

namespace nopact.ChefsLastStand
{
    public class BurgerPool : MonoBehaviour
    {
        public static BurgerPool Instance;

        [SerializeField] private GameObject burgerPrefab;
        [SerializeField] private int initialSize = 10;

        private Queue<Burger> pooledBurgers = new Queue<Burger>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializePool();
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void InitializePool()
        {
            for (int i = 0; i < initialSize; i++)
            {
                Burger newBurger = Instantiate(burgerPrefab, transform).GetComponent<Burger>();
                newBurger.gameObject.SetActive(false);
                pooledBurgers.Enqueue(newBurger);
            }
        }

        public Burger GetBurger()
        {
            if (pooledBurgers.Count == 0)
            {
                Burger newBurger = Instantiate(burgerPrefab, transform).GetComponent<Burger>();
                newBurger.gameObject.SetActive(false);
                return newBurger;
            }
            return pooledBurgers.Dequeue();
        }

        public void ReturnToPool(Burger burger)
        {
            burger.gameObject.SetActive(false);
            pooledBurgers.Enqueue(burger);
        }
    }
}
