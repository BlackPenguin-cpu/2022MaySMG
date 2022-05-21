using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        public FloatReference currentHealth;
        public FloatReference maxHealth;
        public float speed;
        public AudioClip deathSound;
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
        }

        public void GetDamage(float damage)
        {
            currentHealth.Value -= damage;
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
            currentHealth.Value = 0f;
            //
            if (deathSound != null)
            {
                SoundManager.Instance.PlayDuplicatedSFXAsync(deathSound).Forget();
                Destroy(gameObject);
            }
            //
        }


        
    }

}
