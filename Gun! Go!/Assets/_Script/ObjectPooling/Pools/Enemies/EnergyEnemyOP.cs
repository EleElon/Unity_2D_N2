using System;
using UnityEngine;

internal class EnergyEnemyOP : ObjectPool<GameObject> {

    internal static EnergyEnemyOP Instance { get; private set; }

    [SerializeField] private GameObject energyEnemyPrefabs;

    // PlayerBulletOP(int poolSize) : base(poolSize) { }
    GameObject enemy;

    private void Awake() {
        Instance = this;
        InitializePool(5);

        for (int i = 0; i < poolSize; i++) {
            enemy = CreateNewObject();
            ReturnEnergyEnemy(enemy);
        }
    }

    internal GameObject GetEnergyEnemy(Vector2 position) {
        // GameObject bullet = GetObject();
        // BorrowObject(bullet);

        enemy.transform.position = position;
        return GetObject();
    }

    internal void ReturnEnergyEnemy(GameObject obj) {
        // RestoreObject(bullet);
        ReturnObject(obj);
    }

    protected override GameObject CreateNewObject() {
        return GameObject.Instantiate(energyEnemyPrefabs, transform);
    }

    protected override void RestoreObject(GameObject obj) {
        obj.SetActive(false);
    }

    protected override void BorrowObject(GameObject obj) {
        obj.SetActive(true);
    }

    protected override void DestroyObject(GameObject obj) {
        GameObject.Destroy(obj);
    }
}
