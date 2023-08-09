using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Data.ChefData;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Controls
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;

        private Chef chef;
        private Vector2 moveDirection;

        private void Start()
        {
            chef = GetComponent<Chef>();
        }

        public void HandleMovement()
        {
            moveDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
            moveDirection.Normalize();
            transform.position += (Vector3)moveDirection * chef.ChefData.moveSpeed * Time.deltaTime;
        }
    }
}
