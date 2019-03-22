using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Collision
{
    public class BoxColliderComponent : ColliderComponent
    {
        [SerializeField]
        private Vector2 size = Vector2.one;

        public Vector2 HalfExtents { get { return Size * 0.5f; } }
        public override Vector2 Size { get { return base.Size * size; } }

        public override bool Intersects(ColliderComponent other)
        {
            return other.Intersects(this);
        }

        public override bool Intersects(PointColliderComponent point)
        {
            return CollisionUtility.PointAABBIntersection(point.Position, Position, HalfExtents);
        }

        public override bool Intersects(BoxColliderComponent box)
        {
            return CollisionUtility.AABBIntersection(Position, HalfExtents, box.Position, box.HalfExtents);
        }

        public override bool Intersects(CircleColliderComponent circle)
        {
            return CollisionUtility.CircleAABBIntersection(circle.Position, circle.Radius, Position, HalfExtents);
        }

        private void OnDrawGizmos()
        {
            Vector3 leftBot = Position.XY0() + new Vector3(-Size.x * 0.5f, -Size.y * 0.5f);
            Vector3 rightBot = Position.XY0() + new Vector3(Size.x * 0.5f, -Size.y * 0.5f);
            Vector3 leftTop = Position.XY0() + new Vector3(-Size.x * 0.5f, Size.y * 0.5f);
            Vector3 rightTop = Position.XY0() + new Vector3(Size.x * 0.5f, Size.y * 0.5f);

            var prevColor = Gizmos.color;
            Gizmos.color = new Color(0.5f, 1f, 1f);

            Gizmos.DrawLine(leftTop, rightTop);
            Gizmos.DrawLine(rightTop, rightBot);
            Gizmos.DrawLine(rightBot, leftBot);
            Gizmos.DrawLine(leftBot, leftTop);
            Gizmos.color = prevColor;
        }
    }
}
