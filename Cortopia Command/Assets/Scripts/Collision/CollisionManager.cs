using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Collision
{
    public class CollisionManager : MonoBehaviour
    {
        private static List<ColliderComponent> colliders = new List<ColliderComponent>(128);

        public static void Update(ColliderComponent colliderComponent)
        {
            colliders.Add(colliderComponent);
        }

        private void LateUpdate()
        {
            for (int i = 0; i < colliders.Count - 1; i++)
            {
                for (int j = i + 1; j < colliders.Count; j++)
                {
                    if (!Physics.GetIgnoreLayerCollision(colliders[i].Layer, colliders[j].Layer) &&
                        colliders[i].Intersects(colliders[j]))
                    {
                        if (colliders[i].Intersects(colliders[j]))
                        {

                        }
                        colliders[i].RaiseCollision(colliders[j]);
                        colliders[j].RaiseCollision(colliders[i]);
                    }
                }
            }

            colliders.Clear();
        }
    }
}
