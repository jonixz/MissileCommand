using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public abstract class Projectile : Movable
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        public Vector2 Position { get { return new Vector2(transform.position.x, transform.position.y); } }

        protected virtual void OnEnable()
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }

        protected override void Update()
        {
            base.Update();
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
