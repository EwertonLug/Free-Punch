using System;
using UnityEngine;

namespace FreePunch.AI
{
    public abstract class NPCBase : MonoBehaviour
    {
        public event Action<NPCBase> OnTakeDamage;

        private int _life = 100;

        public int Life => _life;
        public abstract void Initialize();
        public abstract void Die();
        public abstract void DisabeAllPhysics();

        public virtual void TakeDamage(int damageAmount)
        {
            _life -= damageAmount;
            OnTakeDamage?.Invoke(this);

        }

    }
}
