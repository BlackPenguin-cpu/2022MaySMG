using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace GameSystem
{
    public class ResultManager : MonoBehaviour
    {
        private bool isGameOver;

        public Canvas resultCanvas;
        //public Canvas 
        public FloatReference currentGauge;
        public string resultSceneName;

        public float fadeOutDelay;
        public Image fadeOutImage;


        private void Update()
        {
            if (!isGameOver && currentGauge.Value <= 0f)
            {
                GameOverAsync().Forget();
            }
        }

        public async UniTask GameOverAsync()
        {
            isGameOver = true;
            Time.timeScale = 0f;
            ScoreManager.Instance.CalculateResult();

            SoundManager.Instance.StopBGM(fadeOutDelay);
            await fadeOutImage.DOFade(1f, fadeOutDelay).SetUpdate(true);

            await SceneManager.LoadSceneAsync(resultSceneName);
        }
    }

}
