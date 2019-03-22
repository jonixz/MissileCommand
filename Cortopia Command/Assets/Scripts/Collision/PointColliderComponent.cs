using Assets.Scripts.Utility;

namespace Assets.Scripts.Collision
{
    public class PointColliderComponent : ColliderComponent
    {
        public override bool Intersects(ColliderComponent other)
        {
            return other.Intersects(this);
        }

        public override bool Intersects(PointColliderComponent point)
        {
            return CollisionUtility.PointIntersection(Position, point.Position);
        }

        public override bool Intersects(BoxColliderComponent box)
        {
            return CollisionUtility.PointAABBIntersection(Position, box.Position, box.Size * 0.5f);
        }

        public override bool Intersects(CircleColliderComponent circle)
        {
            return CollisionUtility.PointCircleIntersection(Position, circle.Position, circle.Size.x * 0.5f);
        }
    }
}
