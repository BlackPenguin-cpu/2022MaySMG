using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class ImageFillByAtom : MonoBehaviour
    {
        private float ratio { get => Mathf.InverseLerp(minValue.Value, maxValue.Value, currentValue.Value); }

        private Image image;

        public FloatReference minValue;
        public FloatReference maxValue;
        public FloatReference currentValue;

        public FloatReference minFill;
        public FloatReference maxFill;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Update()
        {
            image.fillAmount = Mathf.Lerp(minFill.Value, maxFill.Value, ratio);
        }
    }

}
