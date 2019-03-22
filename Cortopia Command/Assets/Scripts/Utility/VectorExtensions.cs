using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class VectorExtensions
    {
        public static Vector3 XY0(this Vector2 vec)
        {
            return new Vector3(vec.x, vec.y, 0f);
        }

        public static Vector2 XY(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }
    }
}
