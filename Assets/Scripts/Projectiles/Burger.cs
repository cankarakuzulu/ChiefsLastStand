using System.Collections;
using System.Collections.Generic;
using nopact.ChefsLastStand.Gameplay.Entities;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Projectiles
{
    public class Burger : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private float damage;
        private Vector2 moveDirection;

        private void Update()
        {
            Move();
            CheckOutOfBounds();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IAttackable attackableEntity = other.GetComponent<IAttackable>();
            if (attackableEntity != null)
            {
                attackableEntity.TakeDamage(damage);
                ReturnToPool();
            }
        }

        public void SetInitialDirection(Vector2 direction)
        {
            moveDirection = direction;
        }

        public void SetDamage(float dmg)
        {
            damage = dmg;
        }

        private void Move()
        {
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
        }

        private void CheckOutOfBounds()
        {
            if (!transform.IsInCameraView())
            {
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            BurgerPool.Instance.ReturnToPool(this);
        }
    }
}
