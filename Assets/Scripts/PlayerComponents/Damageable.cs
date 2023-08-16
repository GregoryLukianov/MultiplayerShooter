using System;
using UnityEngine;

namespace PlayerComponents
{
    public abstract class Damageable : MonoBehaviour
    {
        public virtual event Action OnHit;
        public virtual event Action OnDeath;
        public abstract void GetDamage(float damage);

        protected void RiseHit()
        {
            OnHit?.Invoke();
        }

        protected void RiseDeath()
        {
            OnDeath?.Invoke();
        }

        public abstract float GetHealthPointsLeft();

    }
}