using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemySpawnManager : MonoBehaviour {

    [Header("---------- Variables ----------")]
    // [SerializeField] BasicEnemyOP _basicEnemyOP;
    // [SerializeField] EnergyEnemyOP _energyEnemyOP;
    // [SerializeField] ExplosionEnemyOP _explosionEnemyOP;
    // [SerializeField] HealerEnemyOP _healerEnemyOP;
    // [SerializeField] GameObject[] enemies;
    [SerializeField] Transform[] spawnPosition;
    float timeToSpawn = 4.7f;
    GameObject enemy;

    private void Start() {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        if (spawnPosition == null || spawnPosition.Length == 0)
            yield break;

        while (true) {
            yield return new WaitForSeconds(timeToSpawn);
            Transform spawnPoint = spawnPosition[Random.Range(0, spawnPosition.Length)];
            // GameObject enemy = enemies[Random.Range(0, enemies.Length)];

            // Instantiate(enemy, spawnPoint.position, Quaternion.identity);

            int randomCase = Random.Range(0, 4);

            switch (randomCase) {
                case 0:
                    enemy = BasicEnemyOP.Instance?.GetBasicEnemy();
                    break;
                case 1:
                    enemy = EnergyEnemyOP.Instance?.GetEnergyEnemy();
                    break;
                case 2:
                    enemy = HealerEnemyOP.Instance?.GetHealerEnemy();
                    break;
                case 3:
                    enemy = ExplosionEnemyOP.Instance?.GetExplosionEnemy();
                    break;
            }
            if (enemy == null)
                continue;
            enemy.transform.position = spawnPoint.position;
        }
    }
}
