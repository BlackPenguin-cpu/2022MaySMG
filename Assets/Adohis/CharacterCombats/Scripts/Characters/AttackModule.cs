using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Character
{
    public class AttackModule : MonoBehaviour
    {
        public Projectiles prefab;
        public float attackInterval = 1f;

        public FloatReference greenGauge;

        private void Start()
        {
            FireAsync(prefab, attackInterval).AttachExternalCancellation(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private void Fire()
        {
            var projectile = Instantiate(prefab, transform);

            if (greenGauge >= projectile.resourceConsumption)
            {
                projectile.gameObject.SetActive(true);
                greenGauge.Value -= projectile.resourceConsumption;
            }
        }

        private async UniTask FireAsync(Projectiles prefab, float interval)
        {
            Fire();

            await UniTask.Delay((int)(interval * 1000f));

            await FireAsync(prefab, interval);
        }

    }

}
