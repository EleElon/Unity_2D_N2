using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

internal class BulletsController : MonoBehaviour {
    [Header("---------- Variable ----------")]
    float speed = 7;

    // [Header("---------- Component ----------")]
    // Rigidbody2D _rb;

    private void OnEnable() {
        StartCoroutine(TimeToDestroy());
    }

    private void Update() {
        BulletMovement();
    }

    void BulletMovement() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyBullet() {
        BulletOP.Instance.ReturnObject(gameObject);
    }

    IEnumerator TimeToDestroy() {
        yield return new WaitForSeconds(4f);

        if (gameObject.activeSelf) {
            BulletOP.Instance.ReturnObject(gameObject);
        }
    }
}
