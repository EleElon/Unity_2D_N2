using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BulletCollision : MonoBehaviour {

    [Header("---------- Variables ----------")]
    int damage;

    internal void SetBulletDamage(int dmg) {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null) {
                enemy.TakeDMG(damage);
                PlayerBulletOP.Instance?.ReturnBullet(gameObject);

                EnemyHPManager _enemyHPManager = enemy.GetComponentInChildren<EnemyHPManager>();

                _enemyHPManager.SetLastDMGTakeDMG(Time.time);
            }
        }

        if (other.CompareTag("EnemyBullet")) {
            EnemyBulletController _enemyBulletController = other.GetComponent<EnemyBulletController>();

            if (_enemyBulletController != null) {
                _enemyBulletController.EnemyBulletTakeDMG(damage);

                PlayerBulletOP.Instance?.ReturnBullet(gameObject);
            }
        }
    }
}
