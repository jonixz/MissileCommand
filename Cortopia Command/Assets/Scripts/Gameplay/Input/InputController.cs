using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Input
{
    public class InputController : MonoBehaviour
    {
        public static event Action<Vector3> OnFire;
        public static event Action<Vector3> OnAim;

        [SerializeField]
        private Texture2D cursorTexture;

        private new UnityEngine.Camera camera;

        private void Awake()
        {
            camera = UnityEngine.Camera.main;
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        private void Update()
        {
            var worldPosition = camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            worldPosition.z = 0f;

            OnAim?.Invoke(worldPosition);

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnFire?.Invoke(worldPosition);
            }
        }
    }
}