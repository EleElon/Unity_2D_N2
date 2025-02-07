using System;
using UnityEngine;

internal class EnergyOP : ObjectPool<GameObject> {

    internal static EnergyOP Instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab;

    // PlayerBulletOP(int poolSize) : base(poolSize) { }

    private void Awake() {
        Instance = this;
        InitializePool(10);

        for (int i = 0; i < poolSize; i++) {
            GameObject obj = CreateNewObject();
            ReturnEnergy(obj);
        }
    }

    internal GameObject GetEnergy() {
        // GameObject bullet = GetObject();
        // BorrowObject(bullet);
        return GetObject();
    }

    internal void ReturnEnergy(GameObject bullet) {
        // RestoreObject(bullet);
        ReturnObject(bullet);
    }

    protected override GameObject CreateNewObject() {
        return GameObject.Instantiate(bulletPrefab, transform);
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
