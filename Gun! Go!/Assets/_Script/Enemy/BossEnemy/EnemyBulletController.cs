using System.Collections;
using UnityEngine;

internal class EnemyBulletController : MonoBehaviour {

    Vector3 moveDirection;
    float bulletSpeed = 2.3f;

    void OnEnable() {
        StartCoroutine(TimeToReturn());
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

    IEnumerator TimeToReturn() {
        yield return new WaitForSeconds(20f);

        if (gameObject.activeSelf) {
            EnemyBulletOP.Instance.ReturnEnemyBullet(gameObject);
        }
    }
}
