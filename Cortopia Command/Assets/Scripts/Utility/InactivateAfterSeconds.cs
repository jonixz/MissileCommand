using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class InactivateAfterSeconds : MonoBehaviour
    {
        [SerializeField]
        private int seconds;

        private float elapsedTime;

        private void OnEnable()
        {
            elapsedTime = 0f;
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= seconds)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
