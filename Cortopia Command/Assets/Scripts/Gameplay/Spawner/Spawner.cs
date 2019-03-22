using Assets.Scripts.Pooling;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private Vector2 size;

        [SerializeField]
        private Vector2 spawnInterval = new Vector2(0.2f, 3f);

        [SerializeField]
        private PrefabType type;

        private Coroutine spawnRoutine;

        private void OnEnable()
        {
            spawnRoutine = StartCoroutine(Spawn());
        }

        private void OnDisable()
        {
            if (spawnRoutine != null)
            {
                StopCoroutine(spawnRoutine);
            }
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(spawnInterval.x, spawnInterval.y));
                var poolObject = Pool.GetObject(type, GetSpawnPoint());
                if (poolObject != null)
                {
                    OnObjectSpawned(poolObject);
                }
            }
        }

        protected virtual void OnObjectSpawned(GameObject gameObject) { }


        protected Vector2 GetSpawnPoint()
        {
            Vector2 min = new Vector2(transform.position.x, transform.position.y) - size * 0.5f;
            Vector2 max = new Vector2(transform.position.x, transform.position.y) + size * 0.5f;
            return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }

        protected virtual void OnDrawGizmos()
        {
            Vector3 leftBot = transform.position + new Vector3(-size.x * 0.5f, -size.y * 0.5f);
            Vector3 rightBot = transform.position + new Vector3(size.x * 0.5f, -size.y * 0.5f);
            Vector3 leftTop = transform.position + new Vector3(-size.x * 0.5f, size.y * 0.5f);
            Vector3 rightTop = transform.position + new Vector3(size.x * 0.5f, size.y * 0.5f);

            var prevColor = Gizmos.color;
            Gizmos.color = Color.white;

            Gizmos.DrawLine(leftTop, rightTop);
            Gizmos.DrawLine(rightTop, rightBot);
            Gizmos.DrawLine(rightBot, leftBot);
            Gizmos.DrawLine(leftBot, leftTop);

            Gizmos.color = prevColor;
        }
    }
}
