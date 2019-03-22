using Assets.Scripts.Collision;
using Assets.Scripts.Pooling;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Explosion : MonoBehaviour
    {
        public static event Action<int> OnExplosion;

        [SerializeField]
        private float explosionTime;

        [SerializeField]
        private float fadeOutTime;

        [SerializeField]
        private float startSize;

        [SerializeField]
        private float endSize;

        [SerializeField]
        private AnimationCurve explosionCurve;

        [SerializeField]
        private Gradient colorRamp;

        public GameObject particles;

        private new ColliderComponent collider;
        private new SpriteRenderer renderer;
        private Color color;

        public int Order
        {
            get { return renderer.sortingOrder; }
            set
            {
                renderer.sortingOrder = value;
                renderer.color = colorRamp.Evaluate(Mathf.Min(0.1f * value, 1f));
                color = renderer.color;
            }
        }

        private void Awake()
        {
            collider = GetComponent<ColliderComponent>();
            renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            Order = 0;
            if (collider != null)
            {
                collider.OnCollision += OnCollision;
            }
            transform.localScale = new Vector3(startSize, startSize, 1f);
            renderer.color = color;
            StartCoroutine(Explode());
        }

        private void OnDisable()
        {
            if (collider != null)
            {
                collider.OnCollision -= OnCollision;
            }
        }

        private IEnumerator Explode()
        {
            var elapsedTime = 0f;
            while (elapsedTime < explosionTime)
            {
                var scale = Mathf.Lerp(startSize, endSize, explosionCurve.Evaluate(elapsedTime / explosionTime));
                transform.localScale = new Vector3(scale, scale, 1f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < fadeOutTime)
            {
                renderer.color = Color.Lerp(color, new Color(1f, 1f, 1f, 0f), elapsedTime / fadeOutTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            gameObject.SetActive(false);
        }


        private void OnCollision(ColliderComponent other)
        {
            var health = other.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.Damage();

                var dir = other.Position - collider.Position;
                var explosion = Pool.GetObject<Explosion>(PrefabType.PlayerExplosion, transform.position + new Vector3(dir.x, dir.y, 0f) * transform.localScale.x * 0.5f);
                if (explosion != null)
                {
                    explosion.Order = Order + 1;
                }
                var particles = Pool.GetObject<ParticleSystem>(PrefabType.ExplosionParticles, explosion.transform.position);
                if (particles != null)
                {
                    particles.Stop();
                    particles.Play();
                }

                OnExplosion?.Invoke(Order + 1);
            }

        }

    }
}

