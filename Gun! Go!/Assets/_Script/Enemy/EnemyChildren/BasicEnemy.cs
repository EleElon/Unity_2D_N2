using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BasicEnemy : Enemy, IEnemy {

    [Header("---------- Variables ----------")]
    protected override float moveSpeed { get; } = 0.7f;
    // protected float moveSpeedOfBasicEnemy = 0.2f;
    protected override int damageDeal { get; } = 8;
    protected int maxHP = 30;
    protected int currentHP;

    public int GetEnemiesMaxHP() => maxHP;
    public int GetEnemiesCurrentHP() => currentHP;

    [Header("---------- Components ----------")]
    EnemyHPManager _enemyHPManager;

    private void Awake() {
        currentHP = maxHP;

        _enemyHPManager = GetComponentInChildren<EnemyHPManager>();
    }

    protected override void Update() {
        base.Update();

        if (currentHP <= 0) {
            Die();
        }
    }

    protected override void FlipEnemy() {
        base.FlipEnemy();

        if (PlayerController.Instance != null) {
            _enemyHPManager.transform.localScale = new Vector3(PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    internal override void TakeDMG(int dmg) {
        currentHP -= dmg;
    }

    internal override float GetMoveSpeed() {
        return moveSpeed;
    }

    internal override int GetDamageDeal() {
        return damageDeal;
    }

    internal override void Die() {
        BasicEnemyOP.Instance.ReturnBasicEnemy(transform.parent.gameObject);
    }

    protected override void ResetEnemyState() {
        currentHP = maxHP;
        ResetParentGameObjectPosition();
        ResetGameObjectPosition();
    }

    protected override void ResetGameObjectPosition() {
        transform.position = Vector2.zero;
    }

    protected override void ResetParentGameObjectPosition() {
        if (transform.parent != null) {
            transform.parent.position = Vector2.zero;
        }
    }
}
