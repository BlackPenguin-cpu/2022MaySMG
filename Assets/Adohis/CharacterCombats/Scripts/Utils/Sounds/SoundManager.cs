using Cysharp.Threading.Tasks;
using DG.Tweening;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class SoundManager : Singleton<SoundManager>
    {
        //public AudioSource idleBackgroundMusic;
        //public AudioSource dieBackgroundMusic;
        public float bgmFadeDuration = 3f;


        public List<AudioSource> bgms;
        public List<AudioSource> sfxs;

        /*
        public void PlayBGM()
        {
            idleBackgroundMusic.volume = 0f;
            idleBackgroundMusic.Play();
            Fade(idleBackgroundMusic, 1f, bgmFadeDuration);
        }
        public void StopBGM()
        {
            //idleBackgroundMusic.volume = 0f;
            //idleBackgroundMusic.Play();
            Fade(idleBackgroundMusic, 0f, bgmFadeDuration, true);
        }
        public void PlayDieBGM()
        {
            dieBackgroundMusic.volume = 0f;
            dieBackgroundMusic.Play();
            Fade(dieBackgroundMusic, 1f, bgmFadeDuration);
        }
        public void StopDieBGM()
        {
            //dieBackgroundMusic.volume = 0f;
            //dieBackgroundMusic.Play();
            Fade(dieBackgroundMusic, 0f, bgmFadeDuration, true);
        }
        */

        public void PlayBGM(int index, float bgmFadeDuration = 3f)
        {
            var bgm = bgms[index];
            bgm.volume = 0f;
            bgm.Play();
            Fade(bgm, 1f, bgmFadeDuration);
        }

        public void StopBGM(float bgmFadeDuration = 3f)
        {

            foreach (var bgm in bgms)
            {
                if (bgm.isPlaying)
                {
                    Fade(bgm, 0f, bgmFadeDuration, true);
                }
            }
            //var bgm = bgms[index];

        }

        public void PlaySFX(int index)
        {
            var sfx = sfxs[index];
            if (!sfx.isPlaying)
            {
                sfx.Play();
            }
        }

        public async UniTask PlayDuplicatedSFXAsync(int index)
        {
            var sfx = Instantiate(sfxs[index], transform);

            if (!sfx.isPlaying)
            {
                sfx.Play();
            }

            await UniTask.WaitUntil(() => !sfx.isPlaying);

            Destroy(sfx.gameObject);
        }

        public async UniTask PlayDuplicatedSFXAsync(AudioClip clip, float volume = 1f)
        {
            var sfxGameobject = new GameObject();
            sfxGameobject.transform.SetParent(transform, true);
            var sfx = sfxGameobject.AddComponent<AudioSource>();
            sfx.clip = clip;
            sfx.volume = volume;
            if (!sfx.isPlaying)
            {
                sfx.Play();
            }

            await UniTask.WaitUntil(() => !sfx.isPlaying).AttachExternalCancellation(this.GetCancellationTokenOnDestroy());

            Destroy(sfx.gameObject);
        }

        public void StopSFX(int index)
        {

            var sfx = sfxs[index];
            if (sfx.isPlaying)
            {
                sfx.Stop();
            }
            //var bgm = bgms[index];

        }

        public void StopSFX()
        {

            foreach (var sfx in sfxs)
            {
                if (sfx.isPlaying)
                {
                    sfx.Stop();
                }
            }

        }

        public void Fade(AudioSource audioSource, float fadeValue, float fadeDuration, bool isStop = false)
        {
            if (isStop)
            {
                audioSource.DOFade(fadeValue, fadeDuration).OnComplete(audioSource.Stop);
            }
            else
            {
                audioSource.DOFade(fadeValue, fadeDuration);
            }
        }

    }
}
