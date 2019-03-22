using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField]
        private int startHealth = 1;

        private int currentHealth;

        public Action OnDied;

        public bool IsDead { get { return currentHealth <= 0; } }

        private void OnEnable()
        {
            currentHealth = startHealth;
        }

        public void Damage()
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                OnDied?.Invoke();
            }
        }
    }

}
