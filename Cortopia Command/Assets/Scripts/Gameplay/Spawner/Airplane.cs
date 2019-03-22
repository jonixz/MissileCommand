
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Airplane : Spawner
    {
        protected override void OnObjectSpawned(GameObject gameObject)
        {
            gameObject.transform.up = Vector3.Slerp(Vector3.up, transform.up, Random.Range(0.1f, 0.3f));
        }
    }
}
