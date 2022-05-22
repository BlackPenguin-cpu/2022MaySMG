using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace GameSystem
{
    public class TitleSceneManager : MonoBehaviour
    {
        public string nextSceneName;
        public KeyCode keyCode;

        [Header("Foreground")]
        public SpriteRenderer foreground;
        public float startDelay;
        public float foregroundOutFadeDuration;
        public float foregroundInFadeDuration;
        public Ease foregroundFadeOutEase;
        public Ease foregroundFadeInEase;

        [Header("Smoke")]
        public SpriteRenderer smoke;
        public float smokeFadeDuration;
        public Ease smokeFadeEase;


        [Header("StartButton")]
        public SpriteRenderer startButton;

        [Header("BGMs")]
        public float bgmFadeDuration = 10f;

        private void Start()
        {
            TitleAsync().AttachExternalCancellation(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask TitleAsync()
        {
            await UniTask.Delay((int)(startDelay * 1000f));


            smoke.gameObject.SetActive(true);
            SoundManager.Instance.PlayBGM(0, 0f);

            SkipTitle().AttachExternalCancellation(this.GetCancellationTokenOnDestroy()).Forget();

            await smoke.DOFade(1f, smokeFadeDuration).SetEase(smokeFadeEase);

            await foreground.DOFade(0f, foregroundOutFadeDuration).SetEase(foregroundFadeOutEase);
            
            startButton.gameObject.SetActive(true);

            await WaitInputAsync(0f);
        }

        private async UniTask SkipTitle()
        {
            await UniTask.WaitUntil(() => Input.GetKeyDown(keyCode));

            await SceneManager.LoadSceneAsync(nextSceneName);
        }

        private async UniTask WaitInputAsync(float delay)
        {           
            await UniTask.Delay((int)(delay * 1000f));

            await UniTask.WaitUntil(() => Input.anyKeyDown);

            SoundManager.Instance.StopBGM(foregroundInFadeDuration);

            await foreground.DOFade(1f, foregroundInFadeDuration).SetEase(foregroundFadeInEase);

            await SceneManager.LoadSceneAsync(nextSceneName);
        }
    }

}
