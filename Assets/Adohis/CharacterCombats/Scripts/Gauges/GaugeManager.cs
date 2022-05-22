using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Utils;

namespace GameSystem
{
    public class GaugeManager : MonoBehaviour
    {
        public FloatReference maxGauge;
        public FloatReference currentGauge;
        public float gaugeReduceSpeed;

        [Header("Alert")]
        public float alertRatio;
        public TransformShaker shaker;

        private void Awake()
        {
            currentGauge.Value = maxGauge.Value;
        }

        private void Update()
        {
            currentGauge.Value = Mathf.Clamp(currentGauge.Value - (gaugeReduceSpeed * Time.deltaTime), 0f, maxGauge.Value);

            /*
            var ratio = currentGauge.Value / maxGauge.Value;

            if (ratio < alertRatio)
            {
                shaker.Shake();
            }
            */
        }
    }

}
