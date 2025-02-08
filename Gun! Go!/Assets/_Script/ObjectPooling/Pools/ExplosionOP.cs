using System;
using UnityEngine;

internal class ExplosionOP : ObjectPool<GameObject> {

    internal static ExplosionOP Instance { get; private set; }

    [SerializeField] private GameObject explosionPrefabs;

    // PlayerBulletOP(int poolSize) : base(poolSize) { }

    private void Awake() {
        Instance = this;
        InitializePool(10);

        for (int i = 0; i < poolSize; i++) {
            GameObject obj = CreateNewObject();
            ReturnExplosion(obj);
        }
    }

    internal GameObject GetExplosion() {
        // GameObject bullet = GetObject();
        // BorrowObject(bullet);
        return GetObject();
    }

    internal void ReturnExplosion(GameObject bullet) {
        // RestoreObject(bullet);
        ReturnObject(bullet);
    }

    protected override GameObject CreateNewObject() {
        return GameObject.Instantiate(explosionPrefabs, transform);
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
