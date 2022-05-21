using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    public class TitleSceneManager : MonoBehaviour
    {
        public string nextSceneName;
        public KeyCode keyCode;

        private void Start()
        {
            WaitInputAsync().AttachExternalCancellation(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask WaitInputAsync()
        {
            await UniTask.WaitUntil(() => Input.GetKeyDown(keyCode));

            await SceneManager.LoadSceneAsync(nextSceneName);
        }
    }

}
