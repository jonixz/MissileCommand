using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class AirplaneSpawner : Spawner
    {
        private enum Direction
        {
            Left,
            Right,
        }

        [SerializeField]
        private Direction direction;

        protected override void OnObjectSpawned(GameObject gameObject)
        {
            if (gameObject != null)
            {
                gameObject.transform.up = direction == Direction.Right ? Vector3.left : Vector3.right;
            }
        }
    }
}
