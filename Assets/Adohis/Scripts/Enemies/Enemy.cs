using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        private float currentHealth;

        public float maxHealth;
        public float speed;

        private void Start()
        {
            InitStatus();
        }

        private void Update()
        {
            Move();
        }

        public void GetDamage(float damage)
        {
            maxHealth -= damage;
            if (maxHealth <= 0f)
            {
                Die();
            }
        }

        private void InitStatus()
        {
            currentHealth = maxHealth;
        }

        private void Move()
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }


        private void Die()
        {
            maxHealth = 0f;
            //

            //
            Destroy(gameObject);
        }


        
    }

}
