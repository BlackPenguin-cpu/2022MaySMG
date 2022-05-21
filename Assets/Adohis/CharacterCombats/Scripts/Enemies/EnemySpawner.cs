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
            public float spawnDelay;
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
                SpawnAsync(enemyData).AttachExternalCancellation(this.GetCancellationTokenOnDestroy());
            }
        }

        private async UniTask SpawnAsync(EnemyData enemyData)
        {
            await UniTask.Delay((int)(enemyData.spawnDelay * 1000f));
            CreateEnemy(enemyData);
            await SpawnAsync(enemyData);
        }

        private void CreateEnemy(EnemyData enemyData)
        {
            var enemy = Instantiate(enemyData.enemyPrefab);
            enemy.transform.position = spawnPoint.position;
            enemy.SetStatus(enemyData);
            enemy.gameObject.SetActive(true);
        }
    }

}
