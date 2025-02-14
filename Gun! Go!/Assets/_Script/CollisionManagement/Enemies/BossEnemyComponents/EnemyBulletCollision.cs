using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyBulletCollision : MonoBehaviour {

    [Header("---------- Variables ----------")]
    int damage;

    internal void SetBulletDamage(int dmg) {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {

            if (PlayerController.Instance != null) {
                PlayerController.Instance.TakeDMG(damage);

                EnemyBulletOP.Instance.ReturnEnemyBullet(gameObject);

                // EnemyHPManager _enemyHPManager = enemy.GetComponentInChildren<EnemyHPManager>();

                // _enemyHPManager.SetLastDMGTakeDMG(Time.time);
            }
        }
    }
}
