using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        public Camera screenCamera;
        public Vector2 positionRatio;


        private void Awake()
        {
            if (screenCamera == null)
            {
                screenCamera = Camera.main;
            }
        }

        private void Start()
        {
            SetPosition();
        }

        public void SetPosition()
        {
            var orthographicSize = screenCamera.orthographicSize;

            var screenRatio = 16f / 9f;

            var xPos = Mathf.Lerp(-orthographicSize * screenRatio, orthographicSize * screenRatio, positionRatio.x);
            var yPos = Mathf.Lerp(-orthographicSize, orthographicSize, positionRatio.y);

            transform.position = new Vector3(xPos, yPos, 0f);
        }
    }

}
