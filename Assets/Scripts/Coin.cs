using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Items
{
    public class Coin : MonoBehaviour
    {
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
