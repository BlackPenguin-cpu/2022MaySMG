using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class TransformShaker : MonoBehaviour
    {
        private Vector3 initialPosition;
        public bool isReturnToInitialPoint;

        public bool isAlwaysShaking;

        public bool isShakingPosition;
        public float positionShakingDuration;
        public float positionShakingStrength;
        public int positionShakingVibrato;

        public bool isShakingScale;
        public float scaleShakingDuration;
        public float scaleShakingStrength;
        public int scaleShakingVibrato;

        private void Awake()
        {
            initialPosition = transform.position;            
        }
        private void Start()
        {
            if (isAlwaysShaking)
            {
                ShakeAsync().AttachExternalCancellation(this.GetCancellationTokenOnDestroy()).Forget();
            }
        }

        public void Shake()
        {
            if (isShakingPosition)
            {
                if (isReturnToInitialPoint)
                {
                    transform.DOShakePosition(positionShakingDuration, positionShakingStrength, positionShakingVibrato).OnComplete(() => transform.position = initialPosition);
                }
                else
                {
                    transform.DOShakePosition(positionShakingDuration, positionShakingStrength, positionShakingVibrato);
                }
            }

            if (isShakingScale)
            {
                transform.DOShakeScale(scaleShakingDuration, scaleShakingStrength, scaleShakingVibrato);
            }
        }

        public async UniTask ShakeAsync()
        {
            if (isShakingPosition)
            {
                await transform.DOShakePosition(positionShakingDuration, positionShakingStrength, positionShakingVibrato);
            }

            if (isShakingScale)
            {
                await transform.DOShakeScale(scaleShakingDuration, scaleShakingStrength, scaleShakingVibrato);
            }

            await ShakeAsync();
        }
    }

}
