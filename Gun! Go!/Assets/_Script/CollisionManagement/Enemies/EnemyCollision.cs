using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyCollision : MonoBehaviour {

    [Header("---------- Variables ----------")]
    bool isCollision;

    [Header("---------- Components ----------")]
    Enemy _enemy;

    private void Awake() {
        _enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isCollision = true;
            InvokeRepeating("DealDamage", 0, 1.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isCollision = false;
            CancelInvoke("DealDamage");
        }
    }

    void DealDamage() {
        if (isCollision) {
            PlayerController.Instance.TakeDMG(_enemy.GetDamageDeal());
        }
    }
}
