using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Character
{
    public class SunglassesMotion : MonoBehaviour
    {
        private Vector3 initialPosition;
        private Vector3 toPosition;

        public float height;
        public int rotationCount;

        public Ease upEase;
        public Ease downEase;

        public FloatReference duration;

        private void Start()
        {
            initialPosition = transform.localPosition;
        }

        public async void DoActionAsync()
        {
            toPosition = initialPosition + Vector3.up * height;

            transform.DORotate(new Vector3(0f, 0f, 360f * rotationCount), duration, RotateMode.FastBeyond360);
            
            await transform.DOLocalMove(toPosition, duration * 0.5f).SetEase(upEase);
            await transform.DOLocalMove(initialPosition, duration * 0.5f).SetEase(downEase);

            transform.localPosition = initialPosition;
            transform.rotation = Quaternion.identity;
        }
    }

}
