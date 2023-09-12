using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Items
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5.0f;
        private Chef chef;

        private void Start()
        {
            chef = FindObjectOfType<Chef>();
        }

        public void MoveToChef()
        {
            StartCoroutine(MoveTowardsChefCoroutine());
        }

        private IEnumerator MoveTowardsChefCoroutine()
        {
            while (true)
            {
                transform.position = Vector2.MoveTowards(transform.position, chef.transform.position, moveSpeed * Time.deltaTime);
                yield return null; 
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Chef"))
            {
                Chef chef = other.GetComponent<Chef>();
                chef.CollectCoin();
                Destroy(gameObject);
            }
        }
    }
}
