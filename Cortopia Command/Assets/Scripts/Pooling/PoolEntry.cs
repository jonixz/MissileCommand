using System;
using UnityEngine;

namespace Assets.Scripts.Pooling
{
    [Serializable]
    public struct PoolEntry
    {
        public GameObject Prefab;
        public int Amount;
        public PrefabType Type;
    }
}