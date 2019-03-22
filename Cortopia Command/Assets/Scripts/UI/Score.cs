using Assets.Scripts.Gameplay;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Score : MonoBehaviour
    {
        public static event Action OnBonusReceived;

        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private int scorePerCount = 100;

        [SerializeField]
        private int bonusLevel = 1000;

        [SerializeField]
        private Color bonusColor;

        private int currentScore;

        private Vector3 defaultScale;
        private Color defaultColor;
        private Coroutine scaleRoutine;
        private Coroutine colorRoutine;

        public int CurrentScore { get { return currentScore; } }

        private void Awake()
        {
            GameOverScreen.OnGameRestart += ResetScore;
            Explosion.OnExplosion += AddScore;
            text = GetComponentInChildren<TextMeshProUGUI>();
            defaultScale = text.transform.localScale;
            defaultColor = text.color;
            ResetScore();
        }

        private void ResetScore()
        {
            SetScore(0);
        }

        private void AddScore(int count)
        {
            var bonuses = CountBonuses(count * scorePerCount);

            for (int i = 0; i < bonuses; i++)
            {
                OnBonusReceived?.Invoke();
            }

            AnimateText(bonuses);

            SetScore(currentScore + count * scorePerCount);
        }

        private int CountBonuses(int score)
        {
            var bonuses = score / bonusLevel;
            var rest = currentScore % bonusLevel;
            if (((score % bonusLevel) + rest) >= bonusLevel)
            {
                bonuses++;
            }
            return bonuses;
        }

        private void SetScore(int score)
        {
            currentScore = score;
            text.text = currentScore.ToString("N0");
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
            colorRoutine = StartCoroutine(Color(bonusColor, 0.1f, 0.3f));

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

        private IEnumerator Color(Color color, float inTime, float outTime)
        {
            var elapsedTime = 0f;
            var startColor = text.color;
            while (elapsedTime < inTime)
            {
                text.color = UnityEngine.Color.Lerp(startColor, color, elapsedTime / inTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            elapsedTime = 0f;
            while (elapsedTime < outTime)
            {
                text.color = UnityEngine.Color.Lerp(color, defaultColor, elapsedTime / outTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
