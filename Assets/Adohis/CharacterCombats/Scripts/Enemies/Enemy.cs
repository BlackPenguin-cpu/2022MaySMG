using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using static Enemies.EnemySpawner;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public FloatReference currentHealth;
        public FloatReference maxHealth;
        public float speed;

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
                Die();
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


        private void Die()
        {
            currentHealth.Value = 0f;
            //

            //
            Destroy(gameObject);
        }


        
    }

}
