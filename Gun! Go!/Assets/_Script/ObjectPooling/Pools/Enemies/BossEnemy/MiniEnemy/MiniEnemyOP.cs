using System;
using UnityEngine;

internal class MiniEnemyOP : ObjectPool<GameObject> {

    internal static MiniEnemyOP Instance { get; private set; }

    [SerializeField] private GameObject miniEnemyPrefabs;

    // PlayerBulletOP(int poolSize) : base(poolSize) { }
    // [Header("---------- Variables ----------")]
    // string childName = "BasicEnemyModel";
    // GameObject basicEnemyChild;
    // Transform parentTransform;

    private void Awake() {
        Instance = this;
        InitializePool(36);

        for (int i = 0; i < poolSize; i++) {
            GameObject obj = CreateNewObject();
            ReturnMiniEnemy(obj);
        }
    }

    internal GameObject GetMiniEnemy() {
        // GameObject bullet = GetObject();
        // BorrowObject(bullet);
        return GetObject();
    }

    internal void ReturnMiniEnemy(GameObject obj) {
        // RestoreObject(bullet);
        ReturnObject(obj);
    }

    protected override GameObject CreateNewObject() {
        // GameObject prefabInstance = Instantiate(basicEnemyPrefabs, transform);
        // basicEnemyChild = prefabInstance.transform.Find(childName)?.gameObject;
        // return basicEnemyChild;
        return Instantiate(miniEnemyPrefabs, transform);
    }

    protected override void RestoreObject(GameObject obj) {
        // parentTransform = obj.transform.parent;
        obj.SetActive(false);
        // obj.transform.SetParent(transform);
    }

    protected override void BorrowObject(GameObject obj) {
        obj.SetActive(true);
    }

    protected override void DestroyObject(GameObject obj) {
        GameObject.Destroy(obj);
    }
}
