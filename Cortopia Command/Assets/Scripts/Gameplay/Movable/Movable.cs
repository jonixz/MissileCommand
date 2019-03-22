using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Movable : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private Vector3 axis = Vector3.up;

        protected virtual void Update()
        {
            transform.position -= transform.rotation * axis * speed * Time.deltaTime;
        }
    }
}
