using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

internal class BulletOP : MonoBehaviour {
    internal static BulletOP Instance { get; private set; }

    [SerializeField] GameObject bulletPrefab;
    int poolSize = 20;
    Queue<GameObject> pool;

    private void Awake() {
        Instance = this;

        pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++) {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    internal GameObject GetObject() {
        if (pool.Count > 0) {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else {
            // GameObject obj = Instantiate(bulletPrefab);
            // return obj;
            return null;
        }
    }

    internal void ReturnObject(GameObject obj) {
        // obj.SetActive(false);
        // if (pool.Count < poolSize) {
        //     pool.Enqueue(obj);
        // }
        // else {
        //     Destroy(obj);
        // }

        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    // internal List<GameObject> GetAllObjects() {
    //     return pool.ToList();
    // }
}
