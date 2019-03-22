using Assets.Scripts.Utility;
using System;
using UnityEngine;

namespace Assets.Scripts.Collision
{
    public abstract class ColliderComponent : MonoBehaviour
    {
        [SerializeField]
        private Vector2 offset;

        public event Action<ColliderComponent> OnCollision;

        public Vector2 Position { get { return transform.position.XY() + (transform.rotation * offset.XY0()).XY(); } }
        public virtual Vector2 Size { get { return transform.localScale.XY(); } }
        public int Layer { get { return gameObject.layer; } }

        public abstract bool Intersects(ColliderComponent other);
        public abstract bool Intersects(PointColliderComponent other);
        public abstract bool Intersects(BoxColliderComponent other);
        public abstract bool Intersects(CircleColliderComponent other);

        public virtual void RaiseCollision(ColliderComponent other)
        {
            if (OnCollision != null)
            {
                OnCollision.Invoke(other);
            }
        }

        protected virtual void Update()
        {
            if (gameObject.activeInHierarchy)
            {
                CollisionManager.Update(this);
            }
        }
    }
}
