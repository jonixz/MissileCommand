using Assets.Scripts.Collision;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class WorldBounds : MonoBehaviour
    {
        private void Awake()
        {
            var colliders = GetComponentsInChildren<ColliderComponent>();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].OnCollision += OnCollision;
            }
        }

        private void OnCollision(ColliderComponent other)
        {
            other.gameObject.SetActive(false);
        }
    }
}
