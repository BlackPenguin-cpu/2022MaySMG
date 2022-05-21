using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Utils
{
    public class ImageScalerByAtom : MonoBehaviour
    {
        private float ratio { get => Mathf.InverseLerp(minValue.Value, maxValue.Value, currentValue.Value); }

        private RectTransform rectTransform;

        public FloatReference minValue;
        public FloatReference maxValue;
        public FloatReference currentValue;

        public Vector3Reference minScale;
        public Vector3Reference maxScale;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            rectTransform.localScale = Vector3.Lerp(minScale.Value, maxScale.Value, ratio);
        }
    }

}
