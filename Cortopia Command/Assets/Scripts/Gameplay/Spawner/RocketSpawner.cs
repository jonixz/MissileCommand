using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class RocketSpawner : Spawner
    {
        [SerializeField]
        private Rect targetRect;

        protected override void OnObjectSpawned(GameObject gameObject)
        {
            if (gameObject != null)
            {
                gameObject.transform.up = gameObject.transform.position - GetTarget();
            }
        }
        
        private Vector3 GetTarget()
        {
            var x = Random.Range(targetRect.min.x, targetRect.max.x);
            var y = Random.Range(targetRect.min.y, targetRect.max.y);
            return new Vector3(x, y, 0f);
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            var prevColor = Gizmos.color;
            Gizmos.color = Color.white;

            Vector3 leftTop = new Vector3(targetRect.min.x, targetRect.max.y);
            Vector3 rightTop = new Vector3(targetRect.max.x, targetRect.max.y);
            Vector3 leftBot = new Vector3(targetRect.min.x, targetRect.min.y);
            Vector3 rightBot = new Vector3(targetRect.max.x, targetRect.min.y);

            Gizmos.DrawLine(leftTop, rightTop);
            Gizmos.DrawLine(rightTop, rightBot);
            Gizmos.DrawLine(rightBot, leftBot);
            Gizmos.DrawLine(leftBot, leftTop);
            Gizmos.color = prevColor;
        }
    }
}
