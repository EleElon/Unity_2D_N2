using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyCollision : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerController.Instance.TakeDMG(10);
        }
    }
}
