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

        public FloatReference currentDifficulty;
        public float initialDifficulty = 1f;
        public float estimatedEndGameTime = 120f;
        private void Awake()
        {
            globalScore.Value = 0f;
            currentDifficulty.Value = initialDifficulty;
        }

        private void Update()
        {
            currentDifficulty.Value += Time.deltaTime / estimatedEndGameTime * initialDifficulty;
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
