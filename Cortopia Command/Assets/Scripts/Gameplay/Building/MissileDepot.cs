using Assets.Scripts.Pooling;
using Assets.Scripts.UI;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Building
{
    public class MissileDepot : MonoBehaviour
    {
        [SerializeField]
        private int startMissileCount = 12;

        [SerializeField]
        private int bonusMissileCount = 6;

        private TextMeshPro text;

        private GameObject aim;

        private int currentMissiles;

        public int MissileCount
        {
            get { return currentMissiles; }
            set
            {
                currentMissiles = value;
                if (text != null)
                {
                    text.text = currentMissiles.ToString();
                }
            }
        }

        private void Awake()
        {
            GameOverScreen.OnGameRestart += () => MissileCount = startMissileCount;
            aim = transform.Find("Aim").gameObject;
            text = GetComponentInChildren<TextMeshPro>();
            var destroyable = GetComponent<Destroyable>();
            if (destroyable != null)
            {
                destroyable.OnDestroyed += (d) =>
                {
                    MissileCount = 0;
                    Score.OnBonusReceived -= Refill;
                };
            }
            MissileCount = startMissileCount;
            Activate(false);
            Score.OnBonusReceived += Refill;
        }

        private void OnDestroy()
        {
            Score.OnBonusReceived -= Refill;
        }

        private void Refill()
        {
            MissileCount += bonusMissileCount;
        }

        public void Aim(Vector3 target)
        {
            aim.transform.up = target - aim.transform.position;
        }

        public void Activate(bool state)
        {
            aim.SetActive(state);
        }

        public void Fire(Vector3 target)
        {
            MissileCount--;
            var missile = Pool.GetObject<Missile>(PrefabType.PlayerMissile, transform.position);
            missile.Target = target;
        }

    }
}