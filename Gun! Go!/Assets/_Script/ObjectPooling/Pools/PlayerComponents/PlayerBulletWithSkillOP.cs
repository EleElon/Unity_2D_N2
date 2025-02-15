using System;
using UnityEngine;

internal class PlayerBulletWithSkillOP : ObjectPool<GameObject> {

    internal static PlayerBulletWithSkillOP Instance { get; private set; }

    [SerializeField] private GameObject bulletSkillPrefabs;

    // PlayerBulletOP(int poolSize) : base(poolSize) { }

    private void Awake() {
        Instance = this;
        InitializePool(20);

        for (int i = 0; i < poolSize; i++) {
            GameObject obj = CreateNewObject();
            ReturnBulletSkill(obj);
        }
    }

    internal GameObject GetBulletSkill() {
        // GameObject bullet = GetObject();
        // BorrowObject(bullet);
        return GetObject();
    }

    internal void ReturnBulletSkill(GameObject obj) {
        // RestoreObject(bullet);
        ReturnObject(obj);
    }

    protected override GameObject CreateNewObject() {
        return GameObject.Instantiate(bulletSkillPrefabs, transform);
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
