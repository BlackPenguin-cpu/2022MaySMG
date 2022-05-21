using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ParticleSpawner : MonoBehaviour
    {
        public Color color;
        public ParticleSystem particlePrefab;

        public float height;

        public void Spawn()
        {
            var spawnedParticle = Instantiate(particlePrefab);
            spawnedParticle.transform.position = transform.position + Vector3.up * height;
            var spawnedParticleMain = spawnedParticle.main;
            spawnedParticleMain.startColor = color;
            spawnedParticle.gameObject.SetActive(true);

            
        }

    }

}
