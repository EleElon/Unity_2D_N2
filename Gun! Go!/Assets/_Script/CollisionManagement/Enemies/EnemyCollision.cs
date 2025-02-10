using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyCollision : MonoBehaviour {

    [Header("---------- Variables ----------")]
    protected bool isCollision;

    [Header("---------- Components ----------")]
    protected Enemy _enemy;

    protected virtual void Awake() {
        _enemy = GetComponent<Enemy>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isCollision = true;
            InvokeRepeating("DealDamage", 0, 1.5f);
        }
        IfCollisionWithBullet(other);
    }

    protected void IfCollisionWithBullet(Collider2D col) {
        if (col.CompareTag("Bullet")) {
            GameObject bloods = BloodOP.Instance.GetBlood();
            bloods.transform.position = gameObject.transform.position;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isCollision = false;
            CancelInvoke("DealDamage");
        }
    }

    protected virtual void DealDamage() {
        if (isCollision) {
            PlayerController.Instance.TakeDMG(_enemy.GetDamageDeal());
        }
    }
}
