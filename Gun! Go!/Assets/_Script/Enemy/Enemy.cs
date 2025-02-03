using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class Enemy : MonoBehaviour {
    protected float moveSpeed = 2f;

    protected virtual void Start() {

    }

    protected virtual void Update() {
        MoveToPlayer();
    }

    protected void MoveToPlayer() {
        if (PlayerController.Instance != null) {
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.transform.position, moveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }
    protected void FlipEnemy() {
        if (PlayerController.Instance != null) {
            transform.localScale = new Vector3(PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }

    internal abstract void TakeDMG(int dmg);
}
