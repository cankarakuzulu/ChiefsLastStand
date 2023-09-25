using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nopact.ChefsLastStand.Gameplay.Entities
{
    public interface IAttackable
    {
        void TakeDamage(float damage);
        Transform GetTransform();
    }
}
