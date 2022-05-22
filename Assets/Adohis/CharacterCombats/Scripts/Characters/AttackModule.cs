using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Utils;

namespace Character
{
    public class AttackModule : MonoBehaviour
    {
        public Projectiles prefab;
        public float attackInterval = 1f;

        public FloatReference greenGauge;
        public FloatReference maxGreenGauge;

        public GunMotion motion;

        [Header("Reload")]
        public int maxAmmo;
        private int currentAmmo;
        public FloatReference reloadDelay;

        [Header("Variance")]
        public float minHeightOffset = -0.1f;
        public float maxHeightOffset = 0.1f;
        public float minSize = 0.9f;
        public float maxSize = 1.1f;
        public float minFireMultiply = 0.9f;
        public float maxFireMultiply = 1.1f;

        [Header("Events")]
        public VoidBaseEventReference onFire;
        public VoidBaseEventReference onReload;

        [Header("Sounds")]
        public AudioClip reloadSFX;

        private void Awake()
        {
            currentAmmo = maxAmmo;
        }

        private void Start()
        {
            FireAsync(prefab).AttachExternalCancellation(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private void Fire()
        {
            if (greenGauge >= prefab.resourceConsumption)
            {
                var projectile = Instantiate(prefab, transform);
                var angleRatio = Random.Range(0f, 1f);
                projectile.transform.position = projectile.transform.position + Vector3.up * Mathf.Lerp(minHeightOffset, maxHeightOffset, angleRatio);
                motion.SetAngle(angleRatio); 

                projectile.transform.localScale = projectile.transform.localScale * Random.Range(minSize, maxSize);
                projectile.gameObject.SetActive(true);
                greenGauge.Value = Mathf.Clamp(greenGauge.Value - projectile.resourceConsumption, 0f, maxGreenGauge.Value);

                onFire?.Event?.Raise();

                currentAmmo--;
            }
        }

        private async UniTask FireAsync(Projectiles prefab)
        {
            Fire();

            await UniTask.Delay((int)(attackInterval * Random.Range(minFireMultiply, maxFireMultiply) * 1000f));

            if (currentAmmo == 0)
            {
                await ReloadAsync();
            }

            await FireAsync(prefab);
        }

        private async UniTask ReloadAsync()
        {
            currentAmmo = maxAmmo;

            onReload?.Event?.Raise();

            if (reloadSFX != null)
            {
                SoundManager.Instance.PlayDuplicatedSFXAsync(reloadSFX).Forget();
            }

            await UniTask.Delay((int)(reloadDelay.Value * 1000f));  
        }

    }

}
