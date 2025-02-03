using System.Collections.Generic;
using UnityEngine;

internal class ObjectPooll<T> where T : Component {
    protected readonly Queue<T> _pool;
    protected readonly System.Func<T> _objectGenerator;
    protected int _maxSize;

    // Constructor yêu cầu maxSize phải được cung cấp
    internal ObjectPooll(System.Func<T> objectGenerator, int maxSize) {
        _objectGenerator = objectGenerator;
        _maxSize = maxSize;
        _pool = new Queue<T>();

        // Tạo các đối tượng ban đầu cho pool
        for (int i = 0; i < maxSize; i++) {
            T obj = _objectGenerator();
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    // Lấy đối tượng từ pool
    internal virtual T GetObject() {
        if (_pool.Count > 0) {
            T obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else {
            // Tạo đối tượng mới khi hết đối tượng trong pool
            return _objectGenerator();
        }
    }

    // Trả lại đối tượng vào pool
    internal virtual void ReturnObject(T obj) {
        obj.gameObject.SetActive(false);

        if (_pool.Count < _maxSize) {
            _pool.Enqueue(obj);
        }
        else {
            Object.Destroy(obj.gameObject);
        }
    }

    // Hàm này có thể được sử dụng để thay đổi maxSize trong các lớp con
    public virtual void SetMaxSize(int maxSize) {
        _maxSize = maxSize;
    }
}
