using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace UiUtils
{
    public class ImageFillByAtom : MonoBehaviour
    {
        private float ratio { get => currentGreenGauge.Value / maxGreenGauge.Value; }

        private RectTransform rectTransform;

        public FloatReference minGreenGauge;
        public FloatReference maxGreenGauge;
        public FloatReference currentGreenGauge;

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
