using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utils
{
    public class TmpRevealer : MonoBehaviour
    {
        public TextMeshProUGUI tmp;

        public float randomRadius;
        public Vector3 offset = Vector3.up;
        public float moveDuration = 1f;
        public float remainDuration = 0.5f;
        public Ease ease = Ease.Linear;

        private void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
        }

        public async UniTask Show(bool isRandomOffset = true)
        {
            tmp.enabled = true;
            transform.position += Random.insideUnitSphere * randomRadius;
            await MoveAsync().AttachExternalCancellation(this.GetCancellationTokenOnDestroy());
        }

        private async UniTask MoveAsync()
        {
            await transform.DOMove(transform.position + offset, moveDuration).SetEase(ease);
            await UniTask.Delay((int)(remainDuration * 1000f));

            tmp.enabled = false;
        }

    }

}
