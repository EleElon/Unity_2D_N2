using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemySpawnManager : MonoBehaviour {

    internal static EnemySpawnManager Instance { get; private set; }

    [Header("---------- Variables ----------")]
    [SerializeField] Transform[] spawnPosition;
    float timeToSpawn = 9f;

    void OnEnable() {
        StartCoroutine(SpawnEnemies());
    }

    private void Awake() {
        Instance = this;
    }

    IEnumerator SpawnEnemies() {
        if (spawnPosition == null || spawnPosition.Length == 0)
            yield break;

        while (true) {
            yield return new WaitForSeconds(timeToSpawn);
            Transform spawnPoint = spawnPosition[Random.Range(0, spawnPosition.Length)];

            GameObject enemy = null;

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

            if (enemy != null) {
                SetSpawnPoint(enemy, spawnPoint);
            }
        }
    }

    void SetSpawnPoint(GameObject obj, Transform pos) {
        obj.transform.position = pos.position;
    }

    internal float SetTimeSpawner(float amount) {
        return timeToSpawn -= amount;
    }

    internal float GetTimeSpawner() {
        return timeToSpawn;
    }
}
