using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemySpawnManager : MonoBehaviour {

    [Header("---------- Variables ----------")]
    [SerializeField] BasicEnemyOP _basicEnemyOP;
    [SerializeField] EnergyEnemyOP _energyEnemyOP;
    [SerializeField] ExplosionEnemyOP _explosionEnemyOP;
    [SerializeField] HealerEnemyOP _healerEnemyOP;
    [SerializeField] Transform[] spawnPosition;
    float timeToSpawn = 4.7f;

    private void Start() {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        while (true) {
            yield return new WaitForSeconds(timeToSpawn);
            Transform spawnPoint = spawnPosition[Random.Range(0, spawnPosition.Length)];

            int randomCase = Random.Range(0, 4);

            switch (randomCase) {
                case 0:
                    _basicEnemyOP.GetBasicEnemy(spawnPoint.position);
                    break;
                case 1:
                    _energyEnemyOP.GetEnergyEnemy(spawnPoint.position);
                    break;
                case 2:
                    _explosionEnemyOP.GetExplosionEnemy(spawnPoint.position);
                    break;
                case 3:
                    _healerEnemyOP.GetHealerEnemy(spawnPoint.position);
                    break;
            }
        }
    }
}
