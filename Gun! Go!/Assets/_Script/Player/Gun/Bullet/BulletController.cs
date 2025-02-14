using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

internal class BulletsController : MonoBehaviour {

    [Header("---------- Variables ----------")]
    float speed = 7;
    Vector2 targetPosition;

    // [Header("---------- Component ----------")]
    // Rigidbody2D _rb;

    private void OnEnable() {
        StartCoroutine(TimeToDestroy());
    }

    private void Update() {
        BulletMovement();
    }

    void BulletMovement() {
        // transform.Translate(Vector2.right * speed * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.001f) {
            PlayerBulletOP.Instance?.ReturnBullet(gameObject);
        }
    }

    internal void SetTarget(Vector2 target) {
        targetPosition = target;
    }

    IEnumerator TimeToDestroy() {
        yield return new WaitForSeconds(4f);

        if (gameObject.activeSelf) {
            PlayerBulletOP.Instance?.ReturnBullet(gameObject);
        }
    }
}
