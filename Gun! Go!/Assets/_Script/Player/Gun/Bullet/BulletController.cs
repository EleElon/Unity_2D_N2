using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

internal class BulletsController : MonoBehaviour {
    [Header("---------- Variable ----------")]
    float speed = 7;

    // [Header("---------- Component ----------")]
    // Rigidbody2D _rb;

    private void Update() {
        BulletMovement();
    }

    void BulletMovement() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
