using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maps
{
    public class BackgroundMover : MonoBehaviour
    {       
        public float speed;
        public float resetOffset = -18.5f;

        public GameObject pair;
        private void Update()
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        private void LateUpdate()
        {
            if (transform.position.x <= resetOffset)
            {
                transform.position = pair.transform.position + Vector3.right * 19.2f;
            }
        }


    }

}
