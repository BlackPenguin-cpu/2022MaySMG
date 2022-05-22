using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class SetWorldHeight : MonoBehaviour
    {
        public float height;
        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
    }

}
