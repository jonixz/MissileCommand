using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Vector2 maximumTranslationShake = Vector2.one;

        [SerializeField]
        private float frequency = 25;

        [SerializeField]
        private float traumaExponent = 1;

        [SerializeField]
        private float recoverySpeed = 1;

        private void Awake()
        {
            Explosion.OnExplosion += Shake;
            Destroyable.OnDestroyableDestroyed += Shake;
        }

        private void OnDestroy()
        {
            Explosion.OnExplosion -= Shake;
            Destroyable.OnDestroyableDestroyed -= Shake;
        }

        private void Shake()
        {
            StartCoroutine(DoShake());
        }

        private void Shake(int count)
        {
            Shake();
        }

        private IEnumerator DoShake()
        {
            var seed = Random.value;
            var trauma = 1f;

            while (trauma > 0)
            {
                float shake = Mathf.Pow(trauma, traumaExponent);

                transform.localPosition = new Vector3(
                    maximumTranslationShake.x * (Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1) * shake,
                    maximumTranslationShake.y * (Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2 - 1) * shake,
                    transform.localPosition.z);

                trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
