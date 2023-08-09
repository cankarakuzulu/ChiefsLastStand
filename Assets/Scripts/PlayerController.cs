using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.ChefData;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Controls
{
    [RequireComponent(typeof(PlayerMovementController))]
    [RequireComponent(typeof(PlayerAttackController))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovementController movementController;
        private PlayerAttackController attackController;

        private void Awake()
        {
            movementController = GetComponent<PlayerMovementController>();
            attackController = GetComponent<PlayerAttackController>();
        }

        private void Update()
        {
            movementController.HandleMovement();
            attackController.HandleAttack();
        }
    }
}

