using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemySpawnManagerTest : MonoBehaviour {
    internal static EnemySpawnManagerTest Instance { get; private set; }

    [Header("---------- Variables ----------")]
    // [SerializeField] BasicEnemyOP _basicEnemyOP;
    // [SerializeField] EnergyEnemyOP _energyEnemyOP;
    // [SerializeField] ExplosionEnemyOP _explosionEnemyOP;
    // [SerializeField] HealerEnemyOP _healerEnemyOP;
    // [SerializeField] GameObject[] enemies;
    [SerializeField] Transform[] spawnPosition;
    float timeToSpawn = 4.7f;
    static GameObject _basicEnemy, _energyEnemy, _explosionEnemy, _healerEnemy;
    Dictionary<string, GameObject> enemies = new Dictionary<string, GameObject>();
    GameObject[] enemiesToSpawn = { _basicEnemy, _energyEnemy, _explosionEnemy, _healerEnemy };

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        StartCoroutine(SpawnEnemies());
        InitializeEnemies();
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
                    _basicEnemy = BasicEnemyOP.Instance?.GetBasicEnemy();
                    break;
                case 1:
                    _energyEnemy = EnergyEnemyOP.Instance?.GetEnergyEnemy();
                    break;
                case 2:
                    _healerEnemy = HealerEnemyOP.Instance?.GetHealerEnemy();
                    break;
                case 3:
                    _explosionEnemy = ExplosionEnemyOP.Instance?.GetExplosionEnemy();
                    break;
            }


            foreach (GameObject enemy in enemiesToSpawn) {
                if (enemy != null) {
                    enemy.transform.position = spawnPoint.position;
                }
            }
        }
    }

    internal void InitializeEnemies() {
        enemies["Basic"] = _basicEnemy;
        enemies["Energy"] = _energyEnemy;
        enemies["Explosion"] = _explosionEnemy;
        enemies["Healer"] = _healerEnemy;
    }


    internal GameObject GetEnemy(string type) {
        return enemies.ContainsKey(type) ? enemies[type] : null;
    }

    // internal GameObject GetBasicEnemy() {
    //     return _basicEnemy;
    // }
    // internal GameObject GetEnergyEnemy() {
    //     return _energyEnemy;
    // }
    // internal GameObject GetExplosionEnemy() {
    //     return _explosionEnemy;
    // }
    // internal GameObject GetHealerEnemy() {
    //     return _healerEnemy;
    // }
}
