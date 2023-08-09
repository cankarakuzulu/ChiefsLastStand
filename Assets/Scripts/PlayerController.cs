using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.ChefData;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Controls
{
    [RequireComponent(typeof(PlayerMovementController))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovementController movementController;

        private void Awake()
        {
            movementController = GetComponent<PlayerMovementController>();
        }

        private void Update()
        {
            movementController.HandleMovement();
        }
    }
}

