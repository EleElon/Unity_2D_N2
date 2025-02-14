using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HPBallCollision : MonoBehaviour {

    [Header("---------- Variables ----------")]
    int healingAmount = 20;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerController.Instance.Heal(healingAmount);

            EnergyOP.Instance.ReturnEnergy(gameObject);
        }
    }
}
