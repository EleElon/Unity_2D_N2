using System.Collections.Generic;
using UnityEngine;

internal abstract class ObjectPool<T> : MonoBehaviour where T : class {
    
    protected Queue<T> pool;
    protected int poolSize;

    protected void InitializePool(int size) {
        poolSize = size;
        pool = new Queue<T>(size);
    }

    protected T GetObject() {
        if (pool.Count > 0) {
            T obj = pool.Dequeue();
            BorrowObject(obj);
            return obj;
        }
        else {
            // T obj = CreateNewObject();
            // return obj;
            return null;
        }
    }

    protected void ReturnObject(T obj) {
        // ResetObject(obj);  
        // if (pool.Count < poolSize) {
        //     pool.Enqueue(obj);
        // }
        // else {
        //     DestroyObject(obj); 
        // }
        RestoreObject(obj);
        pool.Enqueue(obj);
    }

    protected abstract T CreateNewObject();

    protected abstract void RestoreObject(T obj);

    protected abstract void BorrowObject(T obj);

    protected abstract void DestroyObject(T obj);
}
