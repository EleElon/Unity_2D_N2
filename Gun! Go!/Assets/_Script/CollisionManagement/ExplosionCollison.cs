using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ExplosionCollison : MonoBehaviour {

    [Header("---------- Variables ----------")]
    int dmg = 20;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerController.Instance.TakeDMG(dmg);
        }
    }
}
