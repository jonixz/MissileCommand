using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class CollisionUtility
    {
        public static bool PointIntersection(Vector2 first, Vector2 second)
        {
            return Vector2.SqrMagnitude(first - second) <= Mathf.Epsilon;
        }

        public static bool PointCircleIntersection(Vector2 firstPos, Vector2 secondPos, float radius)
        {
            return Vector2.SqrMagnitude(firstPos - secondPos) < (radius * radius);
        }

        public static bool PointAABBIntersection(Vector2 point, Vector2 center, Vector2 halfExtents)
        {
            return point.x < center.x + halfExtents.x &&
                point.x > center.x - halfExtents.x &&
                point.y < center.y + halfExtents.y &&
                point.y > center.y - halfExtents.y;
        }

        public static bool CircleAABBIntersection(Vector2 circlePos, float circleRadius, Vector2 rectPos, Vector2 halfExtents)
        {
            var deltaX = circlePos.x - Mathf.Max(rectPos.x - halfExtents.x, Mathf.Min(circlePos.x, rectPos.x + halfExtents.x));
            var deltaY = circlePos.y - Mathf.Max(rectPos.y - halfExtents.y, Mathf.Min(circlePos.y, rectPos.y + halfExtents.y));
            return (deltaX * deltaX + deltaY * deltaY) < (circleRadius * circleRadius);
        }

        public static bool AABBIntersection(Vector2 firstPos, Vector2 firstHalfExtents, Vector2 secondPos, Vector2 secondHalfExtents)
        {
            return !(firstPos.x + firstHalfExtents.x < secondPos.x - secondHalfExtents.x ||
                    firstPos.x - firstHalfExtents.x > secondPos.x + secondHalfExtents.x ||
                    firstPos.y - firstHalfExtents.y > secondPos.y + secondHalfExtents.y ||
                    firstPos.y + firstHalfExtents.y < secondPos.y - secondHalfExtents.y);
        }

        public static bool CircleIntersection(Vector2 firstPos, Vector2 secondPos, float firstRadius, float secondRadius)
        {
            return Vector2.SqrMagnitude(firstPos - secondPos) < ((firstRadius + secondRadius) * (firstRadius + secondRadius));
        }
    }
}
