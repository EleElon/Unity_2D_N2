using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class Enemy : MonoBehaviour {

    [Header("---------- Variables ----------")]
    protected virtual float moveSpeed { get; } = 2;
    // protected float moveSpeed = 3f;
    protected virtual int damageDeal { get; } = 10;

    private void OnEnable() {
        ResetEnemyState();
        // _enemyHPManager.ResetEnemyHPBarState();
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

    internal virtual float GetMoveSpeed() {
        return moveSpeed;
    }

    internal virtual int GetDamageDeal() {
        return damageDeal;
    }

    // internal virtual void Die() {
    //     Destroy(gameObject);
    // }

    internal abstract void Die();

    internal abstract void TakeDMG(int dmg);

    protected abstract void ResetEnemyState();

    protected abstract void ResetGameObjectPosition();

    protected abstract void ResetParentGameObjectPosition();
}
