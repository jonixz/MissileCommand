using Assets.Scripts.Pooling;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Missile : Projectile
    {
        private Vector3 target;

        public Vector3 Target
        {
            get { return target; }
            set
            {
                target = value;
                transform.up = transform.position - target;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void Update()
        {
            base.Update();
            if (ReachedTarget())
            {
                var explosion = Pool.GetObject<Explosion>(PrefabType.PlayerExplosion, transform.position);
                gameObject.SetActive(false);
            }
        }

        private bool ReachedTarget()
        {
            return Vector3.Dot(-transform.up, transform.position - target) > 0;
        }
    }
}