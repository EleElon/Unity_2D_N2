using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ExplosionEnemyCollision : EnemyCollision {

    protected override void OnTriggerEnter2D(Collider2D other) {
        // Enemy enemy = GetComponent<Enemy>();

        if (other.CompareTag("Player")) {
            _enemy.Die();
        }
        IfCollisionWithBullet(other);
    }
}
