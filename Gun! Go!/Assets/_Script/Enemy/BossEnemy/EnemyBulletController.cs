using System.Collections;
using UnityEngine;

internal class EnemyBulletController : MonoBehaviour {

    Vector3 moveDirection;
    int maxBulletHP = 15, currentBulletHP;
    float bulletSpeed = 2.3f;
    float timeToReturnBullet = 20f;

    void OnEnable() {
        StartCoroutine(TimeToReturn());
    }

    void Awake() {
        currentBulletHP = maxBulletHP;
    }

    void Update() {
        if (moveDirection == Vector3.zero) return;
        transform.position += moveDirection * Time.deltaTime;

        if (currentBulletHP <= 0) {
            EnemyBulletOP.Instance?.ReturnEnemyBullet(gameObject);
        }
    }

    internal void setMoveDireciton(Vector3 direction) {
        moveDirection = direction;
    }

    internal float GetBulletSpeed() {
        return bulletSpeed;
    }

    IEnumerator TimeToReturn() {
        yield return new WaitForSeconds(timeToReturnBullet);

        if (gameObject.activeSelf) {
            EnemyBulletOP.Instance?.ReturnEnemyBullet(gameObject);
        }
    }

    internal void EnemyBulletTakeDMG(int dmg) {
        currentBulletHP -= dmg;
    }
}
