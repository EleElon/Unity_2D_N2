using System;
using UnityEngine;

internal class PlayerBulletOP : InitializePool<GameObject> {
    internal static PlayerBulletOP Instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab;

    // PlayerBulletOP(int poolSize) : base(poolSize) { }

    private void Awake() {
        Instance = this;
        InitializeObjectPool(20);

        for (int i = 0; i < poolSize; i++) {
            GameObject obj = CreateNewObject();
            ReturnBullet(obj);
        }
    }

    internal GameObject GetBullet() {
        // GameObject bullet = GetObject();
        // BorrowObject(bullet);
        return GetObject();
    }

    internal void ReturnBullet(GameObject bullet) {
        // RestoreObject(bullet);
        ReturnObject(bullet);
    }

    protected override GameObject CreateNewObject() {
        return GameObject.Instantiate(bulletPrefab);
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
