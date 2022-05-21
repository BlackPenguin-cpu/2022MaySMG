using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Character
{
    public class GunMotion : MonoBehaviour
    {
        private float toAngle;


        public float lerpValue = 10f;
        public float minAngle;
        public float maxAngle;

        public FloatReference reloadDuration;

        private void Update()
        {
            Rotate(toAngle);
        }

        public void SetAngle(float ratio)
        {
            toAngle = Mathf.Lerp(minAngle, maxAngle, ratio);
        }

        public void Reload()
        {
            transform.DORotate(new Vector3(0f, 0f, -360f), reloadDuration.Value, RotateMode.FastBeyond360);
        }

        private void Rotate(float angle)
        {
            //var lerpedAngle = Mathf.Lerp(transform.eulerAngles.z, angle, lerpValue * Time.deltaTime);
            var toRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, lerpValue * Time.deltaTime);
        }

 
    }

}
