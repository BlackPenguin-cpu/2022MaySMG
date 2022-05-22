using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace GameSystem
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public FloatReference globalScore;
        public FloatVariable highScore;

        private void Awake()
        {
            globalScore.Value = 0f;
        }

        public void AddScore(float score)
        {
            globalScore.Value += score;
        }

        public void CalculateResult()
        {
            if (globalScore > highScore.Value)
            {
                highScore.Value = globalScore.Value;
                highScore.InitialValue = globalScore.Value;
            }
        }
    }

}
