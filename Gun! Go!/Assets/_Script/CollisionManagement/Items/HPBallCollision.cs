using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HPBallCollision : MonoBehaviour {

    [Header("---------- Variables ----------")]
    int healingAmount = 20;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerController.Instance?.TakeHPBall(healingAmount);

            HPBallOP.Instance?.ReturnHPBall(gameObject);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.GetItemSound());
        }
    }
}
