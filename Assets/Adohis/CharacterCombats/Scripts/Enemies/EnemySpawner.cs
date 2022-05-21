using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [Serializable]
        public struct EnemyData
        {
            public float speed;
            public float health;
            public Enemy enemyPrefab;
            public float height;
            public float initialSpawnDelay;
            public float spawnInterval;
            public float score;
        }

        public List<EnemyData> enemyDataList;
        public Transform spawnPoint;

        private void Start()
        {
            StartSpawn();
        }

        private void StartSpawn()
        {
            foreach (var enemyData in enemyDataList)
            {
                SpawnAsync(enemyData, enemyData.initialSpawnDelay)
                    .AttachExternalCancellation(this.GetCancellationTokenOnDestroy()).Forget();
            }
        }

        private async UniTask SpawnAsync(EnemyData enemyData, float delay)
        {
            await UniTask.Delay((int)(delay * 1000f));
            CreateEnemy(enemyData);
            await SpawnAsync(enemyData, enemyData.spawnInterval);
        }

        private void CreateEnemy(EnemyData enemyData)
        {
            var enemy = Instantiate(enemyData.enemyPrefab);
            enemy.gameObject.SetActive(true);
            enemy.transform.position = spawnPoint.position + Vector3.up * enemyData.height;
            enemy.SetStatus(enemyData);
        }
    }

}
