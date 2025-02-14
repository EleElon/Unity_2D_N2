using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HPBallOP : ObjectPool<GameObject> {

    internal static HPBallOP Instance { get; private set; }

    [SerializeField] private GameObject hpBallPrefabs;

    // PlayerBulletOP(int poolSize) : base(poolSize) { }

    private void Awake() {
        Instance = this;
        InitializePool(10);

        for (int i = 0; i < poolSize; i++) {
            GameObject obj = CreateNewObject();
            ReturnHPBall(obj);
        }
    }

    internal GameObject GetHPBall() {
        // GameObject bullet = GetObject();
        // BorrowObject(bullet);
        return GetObject();
    }

    internal void ReturnHPBall(GameObject obj) {
        // RestoreObject(bullet);
        ReturnObject(obj);
    }

    protected override GameObject CreateNewObject() {
        return GameObject.Instantiate(hpBallPrefabs, transform);
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
