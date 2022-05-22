using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    public class ResultManager : MonoBehaviour
    {
        private bool isGameOver;

        public Canvas resultCanvas;
        //public Canvas 
        public FloatReference currentGauge;
        public string resultSceneName;

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
            await SceneManager.LoadSceneAsync(resultSceneName);
        }
    }

}
