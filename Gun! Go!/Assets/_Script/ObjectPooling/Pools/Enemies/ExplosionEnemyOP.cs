using System;
using UnityEngine;

internal class ExplosionEnemyOP : ObjectPool<GameObject> {

    internal static ExplosionEnemyOP Instance { get; private set; }

    [SerializeField] private GameObject explosionEnemyPrefabs;

    // PlayerBulletOP(int poolSize) : base(poolSize) { }
    GameObject enemy;

    private void Awake() {
        Instance = this;
        InitializePool(5);

        for (int i = 0; i < poolSize; i++) {
            enemy = CreateNewObject();
            ReturnExplosionEnemy(enemy);
        }
    }

    internal GameObject GetExplosionEnemy(Vector2 position) {
        // GameObject bullet = GetObject();
        // BorrowObject(bullet);

        enemy.transform.position = position;
        return GetObject();
    }

    internal void ReturnExplosionEnemy(GameObject obj) {
        // RestoreObject(bullet);
        ReturnObject(obj);
    }

    protected override GameObject CreateNewObject() {
        return GameObject.Instantiate(explosionEnemyPrefabs, transform);
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
