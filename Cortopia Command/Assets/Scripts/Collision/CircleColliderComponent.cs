using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Collision
{
    public class CircleColliderComponent : ColliderComponent
    {
        [SerializeField]
        private float size = 1f;

        public float Radius { get { return Size.x * 0.5f; } }

        public override Vector2 Size => base.Size * size;

        public override bool Intersects(ColliderComponent other)
        {
            return other.Intersects(this);
        }

        public override bool Intersects(PointColliderComponent point)
        {
            return CollisionUtility.PointCircleIntersection(Position, point.Position, Radius);
        }

        public override bool Intersects(BoxColliderComponent box)
        {
            return CollisionUtility.CircleAABBIntersection(Position, Size.x * 0.5f, box.Position, box.Size * 0.5f);
        }

        public override bool Intersects(CircleColliderComponent circle)
        {
            return CollisionUtility.CircleIntersection(Position, circle.Position, Size.x * 0.5f, circle.Radius);
        }

        private void OnDrawGizmos()
        {
            var prevColor = Gizmos.color;
            Gizmos.color = new Color(0.5f, 1f, 1f);

            Gizmos.DrawWireSphere(Position.XY0(), Size.x * 0.5f);

            Gizmos.color = prevColor;
        }
    }
}