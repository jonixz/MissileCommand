using Assets.Scripts.Gameplay.Building;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class DefenseController : MonoBehaviour
    {
        public static event Action OnAllBuildingsDestroyed;

        private List<Destroyable> destroyables = new List<Destroyable>();
        private List<MissileDepot> depots = new List<MissileDepot>();

        private MissileDepot activeDepot;
        private int destroyedDepots;

        private void Awake()
        {
            GameOverScreen.OnGameRestart += Reset;
            GetComponentsInChildren(destroyables);
            GetComponentsInChildren(depots);

            for (int i = 0; i < destroyables.Count; i++)
            {
                destroyables[i].OnDestroyed += OnBuildingDestroyed;
            }

            Input.InputController.OnFire += Fire;
            Input.InputController.OnAim += Aim;
        }

        private void Reset()
        {
            for (int i = 0; i < destroyables.Count; i++)
            {
                destroyables[i].gameObject.SetActive(true);
            }
        }

        private void OnBuildingDestroyed(Destroyable destroyable)
        {
            if (destroyable.GetComponent<MissileDepot>())
            {
                destroyedDepots++;
                if (destroyedDepots >= depots.Count)
                {
                    OnAllBuildingsDestroyed?.Invoke();
                }
            }
        }

        private void Fire(Vector3 target)
        {
            var depot = ClosestDepot(target);
            if (depot != null)
            {
                depot.Fire(target);
            }
        }

        private void Aim(Vector3 target)
        {
            var closestDepot = ClosestDepot(target);
            if (activeDepot != closestDepot)
            {
                if (activeDepot != null)
                {
                    activeDepot.Activate(false);
                }

                activeDepot = closestDepot;

                if (activeDepot != null)
                {
                    activeDepot.Activate(true);
                }
            }

            if (activeDepot != null)
            {
                activeDepot.Aim(target);
            }
        }

        private MissileDepot ClosestDepot(Vector3 target)
        {
            var closestIndex = -1;
            var closestDistance = float.MaxValue;

            for (int i = 0; i < depots.Count; i++)
            {
                if (depots[i].MissileCount <= 0) continue;
                var distance = Vector3.SqrMagnitude(depots[i].transform.position - target);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            if (closestIndex >= 0)
            {
                return depots[closestIndex];
            }

            return null;
        }
    }
}
