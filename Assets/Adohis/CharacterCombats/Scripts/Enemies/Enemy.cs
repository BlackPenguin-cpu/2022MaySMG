using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Utils;
using static Enemies.EnemySpawner;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        private float score;

        public FloatReference currentHealth;
        public FloatReference maxHealth;
        public float speed;
        public AudioClip deathSound;

        public TmpRevealer tmpRevealer;
        public GameObject healthBar;
        public SpriteRenderer body;

        //public Animator hitAnimator;

        [Header("ShakeSetting")]
        public float shakeDuration = 0.1f;
        public float shakeStrength = 1f;
        public int shakeVibrato = 10;

        [Header("Fx")]
        public GameObject dieFxPrefab;
        public float fxSize;

        [Header("Event")]
        public VoidBaseEventReference onDie;
        public VoidBaseEventReference onHit;

        private void Start()
        {
            InitStatus();
        }

        private void Update()
        {
            Move();
        }

        public void SetStatus(EnemyData data)
        {
            maxHealth.Value = data.health;
            speed = data.speed;
            score = data.score;

            tmpRevealer.tmp.text = "+" + score;
        }

        public void GetDamage(float damage)
        {
            currentHealth.Value -= damage;

            transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato);

            onHit?.Event?.Raise();

            if (currentHealth.Value <= 0f)
            {
                DieAsync().AttachExternalCancellation(this.GetCancellationTokenOnDestroy()).Forget();
            }
        }

        private void InitStatus()
        {
            currentHealth.Value = maxHealth.Value;
        }

        private void Move()
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        private async UniTask DieAsync()
        {
            GetComponent<Collider2D>().enabled = false;

            onDie?.Event?.Raise();

            currentHealth.Value = 0f;
            //
            if (deathSound != null)
            {
                SoundManager.Instance.PlayDuplicatedSFXAsync(deathSound).Forget();
            }
            ScoreManager.Instance.AddScore(score);

            var fx = Instantiate(dieFxPrefab);
            fx.transform.position = transform.position;
            fx.transform.localScale = Vector3.one * fxSize;
            fx.gameObject.SetActive(true);

            body.enabled = false;

            await tmpRevealer.Show(true);

            Destroy(gameObject);

            //
        }



    }

}
