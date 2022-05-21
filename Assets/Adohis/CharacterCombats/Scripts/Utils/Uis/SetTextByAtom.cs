using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Utils
{
    public class SetTextByAtom : MonoBehaviour
    {

        private TextMeshProUGUI tmp;
        public FloatReference value;

        private void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            tmp.text = value.Value.ToString();
        }
    }

}
