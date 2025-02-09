using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ExplosionCollison : MonoBehaviour {

    [Header("---------- Variables ----------")]
    int baseDamageExplosion = 20;

    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.GetComponent<Enemy>();

        if (other.CompareTag("Player")) {
            PlayerController.Instance.TakeDMG(baseDamageExplosion);
        }

        if (other.CompareTag("Enemy")) {
            enemy.TakeDMG(baseDamageExplosion);
        }
    }
}
