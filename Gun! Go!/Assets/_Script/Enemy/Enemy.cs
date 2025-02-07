using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class Enemy : MonoBehaviour {
    
    [Header("---------- Variables ----------")]
    protected virtual float moveSpeed { get; } = 2f;
    // protected float moveSpeed = 3f;

    protected virtual void Start() {

    }

    protected virtual void Update() {
        MoveToPlayer();
    }

    protected void MoveToPlayer() {
        if (PlayerController.Instance != null) {
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.transform.position, GetMoveSpeed() * Time.deltaTime);
            FlipEnemy();
        }
    }
    protected virtual void FlipEnemy() {
        if (PlayerController.Instance != null) {
            transform.localScale = new Vector3(PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    protected virtual float GetMoveSpeed() {
        return moveSpeed;
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }

    internal abstract void TakeDMG(int dmg);
}
