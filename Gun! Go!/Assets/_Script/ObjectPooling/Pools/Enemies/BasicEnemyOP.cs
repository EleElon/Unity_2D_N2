using System;
using UnityEngine;

internal class BasicEnemyOP : ObjectPool<GameObject> {

    internal static BasicEnemyOP Instance { get; private set; }

    [SerializeField] private GameObject basicEnemyPrefabs;

    // PlayerBulletOP(int poolSize) : base(poolSize) { }
    GameObject enemy;

    private void Awake() {
        Instance = this;
        InitializePool(5);

        for (int i = 0; i < poolSize; i++) {
            enemy = CreateNewObject();
            ReturnBasicEnemy(enemy);
        }
    }

    internal GameObject GetBasicEnemy(Vector2 position) {
        // GameObject bullet = GetObject();
        // BorrowObject(bullet);

        enemy.transform.position = position;
        return GetObject();
    }

    internal void ReturnBasicEnemy(GameObject obj) {
        // RestoreObject(bullet);
        ReturnObject(obj);
        Debug.Log("vcl");
    }

    protected override GameObject CreateNewObject() {
        return GameObject.Instantiate(basicEnemyPrefabs, transform);
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
