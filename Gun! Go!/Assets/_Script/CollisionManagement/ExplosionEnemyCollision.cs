using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ExplosionEnemyCollision : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = GetComponent<Enemy>();

        if (other.CompareTag("Player")) {
            enemy.Die();
        }
    }
}
