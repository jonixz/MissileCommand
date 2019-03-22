using Assets.Scripts.Gameplay;
using Kino;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        public static event Action OnGameRestart;

        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private DigitalGlitch glitch;

        private CanvasGroup canvasGroup;

        private int currentScore;

        private Vector3 defaultScale;
        private Color defaultColor;
        private Coroutine scaleRoutine;
        private Coroutine colorRoutine;
        private Coroutine glitchRoutine;

        public int gameOver = 1;

        private void Update()
        {
            if (gameOver == 0)
            {
                ShowGameOverScreen();
            }
        }

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            canvasGroup = GetComponentInParent<CanvasGroup>();

            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;

            defaultScale = text.transform.localScale;
            defaultColor = text.color;

            DefenseController.OnAllBuildingsDestroyed += ShowGameOverScreen;
        }

        private void ShowGameOverScreen()
        {
            Delay(() =>
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
            }, 2f);

            Interpolate(f => Time.timeScale = f, 1f, 0.2f, 1f);
            Glitch();
        }

        private void HideGameOverScreen()
        {
            Time.timeScale = 1f;
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;

            if (glitchRoutine != null)
            {
                StopCoroutine(glitchRoutine);
            }
            glitchRoutine = Interpolate(f => glitch.intensity = f, 1f, 0f, 2f);
        }

        private void Glitch()
        {
            var target = UnityEngine.Random.Range(0f, 1f);
            var start = glitch.intensity;
            var time = UnityEngine.Random.Range(0.1f, 1f);

            glitchRoutine = Interpolate(f => glitch.intensity = f, start, target, time, Glitch);
        }

        private void AnimateText(int bonuses)
        {
            if (scaleRoutine != null)
            {
                StopCoroutine(scaleRoutine);
            }
            if (colorRoutine != null)
            {
                StopCoroutine(colorRoutine);
            }

            scaleRoutine = StartCoroutine(Scale(Vector3.one + Vector3.one * Mathf.Min(0.1f * Mathf.Max(bonuses, 1), 0.3f), 0.1f, 0.3f));
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void Restart()
        {
            HideGameOverScreen();
            OnGameRestart?.Invoke();
        }

        private IEnumerator Scale(Vector3 scale, float inTime, float outTime)
        {
            var elapsedTime = 0f;
            var startScale = text.transform.localScale;
            while (elapsedTime < inTime)
            {
                text.transform.localScale = Vector3.Lerp(startScale, scale, elapsedTime / inTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            elapsedTime = 0f;
            while (elapsedTime < outTime)
            {
                text.transform.localScale = Vector3.Lerp(scale, defaultScale, elapsedTime / outTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        private Coroutine Interpolate(Action<float> value, float from, float to, float time, Action onDone = null)
        {
            return StartCoroutine(DoInterpolate(value, from, to, time, onDone));
        }

        private void Delay(Action action, float time)
        {
            StartCoroutine(DoDelay(action, time));
        }

        private IEnumerator DoDelay(Action action, float time)
        {
            yield return new WaitForSecondsRealtime(time);
            action?.Invoke();
        }

        private IEnumerator DoInterpolate(Action<float> value, float from, float to, float time, Action onDone)
        {
            var elapsedTime = 0f;
            while (elapsedTime < time)
            {
                value(Mathf.Lerp(from, to, elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            value(to);
            onDone?.Invoke();
        }
    }
}
