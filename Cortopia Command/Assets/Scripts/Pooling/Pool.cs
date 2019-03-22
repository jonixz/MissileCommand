using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Pooling
{
    public class Pool : MonoBehaviour
    {
        private static Pool instance;

        [SerializeField]
        private List<PoolEntry> poolEntries;

        private Dictionary<PrefabType, List<GameObject>> pools = new Dictionary<PrefabType, List<GameObject>>();

        protected virtual void Awake()
        {
            instance = this;
            GameOverScreen.OnGameRestart += ResetPools;

            for (int i = 0; i < poolEntries.Count; i++)
            {
                pools.Add(poolEntries[i].Type, new List<GameObject>(poolEntries[i].Amount));
                for (int j = 0; j < poolEntries[i].Amount; j++)
                {
                    pools[poolEntries[i].Type].Add(InstantiatePoolObject(poolEntries[i].Prefab));
                }
            }
        }

        private void ResetPools()
        {
            foreach (var poolType in pools)
            {
                for (int i = 0; i < poolType.Value.Count; i++)
                {
                    poolType.Value[i].SetActive(false);
                }
            }
        }

        private GameObject InternalGetObject(PrefabType type, Vector3 position)
        {
            for (int i = 0; i < pools[type].Count; i++)
            {
                if (!pools[type][i].activeInHierarchy)
                {
                    return InitializeObject(pools[type][i], position);
                }
            }
            return null;
        }

        public static GameObject GetObject(PrefabType type, Vector3 position)
        {
            return instance.InternalGetObject(type, position);
        }

        public static T GetObject<T>(PrefabType type, Vector3 position) where T : Component
        {
            var obj = instance.InternalGetObject(type, position);
            return obj == null ? null : obj.GetComponent<T>();
        }

        private static GameObject InitializeObject(GameObject spawnedObject, Vector3 position)
        {
            spawnedObject.transform.position = position;
            spawnedObject.SetActive(true);
            return spawnedObject;
        }

        private GameObject InstantiatePoolObject(GameObject prefab)
        {
            var pooledObj = Instantiate(prefab);
            pooledObj.SetActive(false);
            return pooledObj;
        }
    }
}