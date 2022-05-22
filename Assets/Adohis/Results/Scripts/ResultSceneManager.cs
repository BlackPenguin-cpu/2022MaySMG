using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace GameSystem
{
    public class ResultSceneManager : MonoBehaviour
    {
        public Image fadeImage;

        public float fadeOutDelay;
        public float fadeOutDuration;
        public Ease fadeOutEase;

        public float fadeInDelay;
        public float fadeInDuration;
        public Ease fadeInEase;

        public string titleSceneName;

        private void Start()
        {
            Time.timeScale = 1f;
            ShowResultAsync().AttachExternalCancellation(this.GetCancellationTokenOnDestroy());
        }

        private async UniTask ShowResultAsync()
        {
            await UniTask.Delay((int)(fadeOutDelay * 1000f));

            SoundManager.Instance.PlayBGM(0, 3f);

            await fadeImage.DOFade(0f, fadeOutDuration).SetEase(fadeOutEase);

            await UniTask.Delay((int)(fadeInDelay * 1000f));

            await fadeImage.DOFade(1f, fadeInDuration).SetEase(fadeInEase);

            await SceneManager.LoadSceneAsync(titleSceneName);

        }

    }

}
