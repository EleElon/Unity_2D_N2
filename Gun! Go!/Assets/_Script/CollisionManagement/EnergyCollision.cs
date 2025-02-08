using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnergyCollision : MonoBehaviour {

    [Header("---------- Variables ----------")]
    int energy = 3;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerController.Instance.TakeEnergy(energy);

            EnergyOP.Instance.ReturnEnergy(gameObject);
        }
    }
}
