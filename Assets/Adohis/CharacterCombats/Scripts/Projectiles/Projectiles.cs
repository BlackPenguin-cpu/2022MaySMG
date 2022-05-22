using Cysharp.Threading.Tasks;
using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Utils;

namespace Character
{
    public class Projectiles : MonoBehaviour
    {
        public Camera screenCamera;

        public float maxRangeOffset = 2f;
        public float speed = 1f;
        public float collisionSize = 1f;
        public float damage;
        public float resourceConsumption = 1f;
        public AudioClip fireSFX;
        public AudioClip attackSFX;

        public Vector3EventReference onHitProjectile;

        private void Awake()
        {
            screenCamera = Camera.main;
        }

        void Start()
        {
            Fire();
        }

        // Update is called once per frame
        void Update()
        {
            Move();

            Scan();

            var orthographicSize = screenCamera.orthographicSize;

            var screenRatio = 16f / 9f;

            var outPositionX = orthographicSize * screenRatio + maxRangeOffset;

            if (transform.position.x > outPositionX)
            {
                Destroy();
            }
        }

        private void Fire()
        {
            if (fireSFX != null)
            {
                SoundManager.Instance.PlayDuplicatedSFXAsync(fireSFX).Forget();
            }
        }

        private void Move()
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        private void Attack(Enemy enemy)
        {
            onHitProjectile?.Event?.Raise(transform.position);

            if (attackSFX != null)
            {
                SoundManager.Instance.PlayDuplicatedSFXAsync(attackSFX).Forget();
            }

            enemy.GetDamage(damage);
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void Scan()
        {
            var hits = Physics2D.CircleCastAll(transform.position, collisionSize, Vector2.right, 0f);

            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent<Enemy>(out var enemy))
                {
                    Attack(enemy);
                    Destroy();
                }
            }
        }

    }

}
