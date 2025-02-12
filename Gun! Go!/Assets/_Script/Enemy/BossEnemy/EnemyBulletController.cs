using UnityEngine;

internal class EnemyBulletController : MonoBehaviour {


    Vector3 moveDirection;
    float bulletSpeed = 1.6f;
    float bulletCircleSpeed = 0.8f;

    void OnEnable() {
        Destroy(gameObject, 10f);
    }

    void Update() {
        if (moveDirection == Vector3.zero) return;
        transform.position += moveDirection * Time.deltaTime;
    }

    internal void setMoveDireciton(Vector3 direction) {
        moveDirection = direction;
    }

    internal float GetBulletSpeed() {
        return bulletSpeed;
    }

    internal float GetBulletCircleSpeed() {
        return bulletCircleSpeed;
    }
}
