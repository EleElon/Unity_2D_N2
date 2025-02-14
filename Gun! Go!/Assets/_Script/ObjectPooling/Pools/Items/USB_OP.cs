using System;
using UnityEngine;

internal class USB_OP : ObjectPool<GameObject> {

    internal static USB_OP Instance { get; private set; }

    [SerializeField] private GameObject usbPrefabs;

    // PlayerBulletOP(int poolSize) : base(poolSize) { }

    private void Awake() {
        Instance = this;
        InitializePool(1);

        for (int i = 0; i < poolSize; i++) {
            GameObject obj = CreateNewObject();
            ReturnUSB(obj);
        }
    }

    internal GameObject GetUSB() {
        // GameObject bullet = GetObject();
        // BorrowObject(bullet);
        return GetObject();
    }

    internal void ReturnUSB(GameObject obj) {
        // RestoreObject(bullet);
        ReturnObject(obj);
    }

    protected override GameObject CreateNewObject() {
        return GameObject.Instantiate(usbPrefabs, transform);
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
