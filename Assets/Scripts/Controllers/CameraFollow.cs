using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Controls
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float offset = 1.0f;

        private Transform chefTransform;
        private Vector2 minBounds;  // bottom-left corner of the restaurant
        private Vector2 maxBounds;  // top-right corner of the restaurant
        private Camera cam;

        private void Start()
        {
            cam = GetComponent<Camera>();
            if (!chefTransform)
            {
                chefTransform = FindObjectOfType<Chef>().transform;
            }

            GameObject restaurant = GameObject.FindGameObjectWithTag("Restaurant");
            if (restaurant)
            {
                SpriteRenderer sr = restaurant.GetComponent<SpriteRenderer>();

                if (sr)
                {
                    Bounds bounds = sr.bounds;
                    minBounds = bounds.min;
                    maxBounds = bounds.max;
                }
                else
                {
                    Debug.LogError("SpriteRenderer not found on restaurant object or its children!");
                }
            }
            else
            {
                Debug.LogError("Restaurant object not found!");
            }
        }

        private void LateUpdate()
        {
            if (chefTransform == null) return;

            Vector3 desiredPosition = new Vector3(chefTransform.position.x, chefTransform.position.y, transform.position.z);

            float camHeight = cam.orthographicSize;
            float camWidth = cam.aspect * camHeight;

            // Clamp the camera's position
            float x = Mathf.Clamp(desiredPosition.x, minBounds.x + camWidth - offset, maxBounds.x - camWidth + offset);
            float y = Mathf.Clamp(desiredPosition.y, minBounds.y + camHeight - offset, maxBounds.y - camHeight + offset);

            transform.position = new Vector3(x, y, desiredPosition.z);
        }
    }
}
