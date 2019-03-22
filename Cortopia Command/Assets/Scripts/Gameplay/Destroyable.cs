using Assets.Scripts.Collision;
using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Destroyable : MonoBehaviour
    {
        private enum DestroyMode
        {
            OnCollision,
            OnDead,
        }

        public static event Action OnDestroyableDestroyed;
        public event Action<Destroyable> OnDestroyed;

        [SerializeField]
        private DestroyMode mode = DestroyMode.OnDead;

        private HealthComponent health;

        private void Awake()
        {
            if (mode == DestroyMode.OnDead)
            {
                health = GetComponent<HealthComponent>();
                health.OnDied += Destroy;
            }

            var colliders = GetComponentsInChildren<ColliderComponent>();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].OnCollision += OnCollision;
            }
        }

        private void OnDestroy()
        {
            if (mode == DestroyMode.OnDead)
            {
                health.OnDied -= Destroy;
            }
            var colliders = GetComponentsInChildren<ColliderComponent>();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].OnCollision -= OnCollision;
            }
        }

        private void Destroy()
        {
            OnDestroyed?.Invoke(this);
            OnDestroyableDestroyed?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnCollision(ColliderComponent other)
        {
            if (mode == DestroyMode.OnDead)
            {
                var explosion = other.GetComponent<Explosion>();
                if (explosion != null)
                {
                    health.Damage();
                }
            }
            else
            {
                Destroy();
            }
        }
    }
}