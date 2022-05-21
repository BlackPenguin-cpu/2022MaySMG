using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maps
{
    public class Splat : MonoBehaviour
    {
        public float destroyXPosition = -22f;

        public float moveSpeed;



        private void Update()
        {
            if (transform.position.x < destroyXPosition)
            {
                Destroy(gameObject);
            }

            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
    }

}
