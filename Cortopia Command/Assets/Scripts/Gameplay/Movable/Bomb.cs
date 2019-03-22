using Assets.Scripts.Collision;
using Assets.Scripts.Pooling;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Bomb : Movable
    {
        private HealthComponent health;

        private void Awake()
        {
            health = GetComponent<HealthComponent>();
            var colliders = GetComponentsInChildren<ColliderComponent>();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].OnCollision += OnCollision;
            }
        }

        private void OnCollision(ColliderComponent other)
        {
            var health = other.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.Damage();
                this.health.Damage();

                Pool.GetObject<Explosion>(PrefabType.EnemyExplosion, transform.position);

                var dir = other.Position - other.Position;
                var particles = Pool.GetObject<ParticleSystem>(PrefabType.ExplosionParticles, transform.position + new Vector3(dir.x, dir.y, 0f) * transform.localScale.x * 0.5f);
                if (particles != null)
                {
                    particles.Stop();
                    particles.Play();
                }
            }
        }
    }

}
