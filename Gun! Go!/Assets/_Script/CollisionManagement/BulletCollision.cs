using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BulletCollision : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null) {
                enemy.TakeDMG(1);
                PlayerBulletOP.Instance.ReturnBullet(gameObject);

                EnemyHPManager _enemyHPManager = enemy.GetComponentInChildren<EnemyHPManager>();

                _enemyHPManager.SetLastDMGTakeDMG(Time.time);
            }
        }
    }
}
