using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Controls
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Joystick joystick;

        [SerializeField]
        private float moveSpeed = 2f;

        private Vector2 moveDirection;
        void Update()
        {
            moveDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
            moveDirection.Normalize();
        }

        private void FixedUpdate()
        {
            transform.position += moveSpeed * Time.fixedDeltaTime * new Vector3(moveDirection.x, moveDirection.y, 0);
        }
    }
}

