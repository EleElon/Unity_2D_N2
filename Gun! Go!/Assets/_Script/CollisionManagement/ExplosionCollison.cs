using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ExplosionCollison : MonoBehaviour {

    [Header("---------- Variables ----------")]
    int dmg = 20;

    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.GetComponent<Enemy>();

        if (other.CompareTag("Player")) {
            PlayerController.Instance.TakeDMG(dmg);
        }

        if (other.CompareTag("Enemy")) {
            enemy.TakeDMG(dmg);
        }
    }
}
