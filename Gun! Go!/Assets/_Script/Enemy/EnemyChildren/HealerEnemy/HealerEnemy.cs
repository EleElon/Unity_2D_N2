using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HealerEnemy : Enemy, IEnemy {

    [Header("---------- Variables ----------")]
    // protected float moveSpeedOfBasicEnemy = 0.2f;
    protected override float moveSpeed { get; } = 3;
    protected override int damageDeal { get; } = 4;
    protected int maxHP = 12;
    protected int currentHP;
    int healValue = 10;

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

    internal override void Die() {
        PlayerController.Instance?.Heal(healValue);

        GameObject hpBall = HPBallOP.Instance?.GetHPBall();
        hpBall.transform.position = transform.position;

        HealerEnemyOP.Instance?.ReturnHealerEnemy(transform.parent.gameObject);
    }

    internal override void TakeDMG(int dmg) {
        currentHP -= dmg;
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

    internal override float GetMoveSpeed() {
        return moveSpeed;
    }

    internal override int GetDamageDeal() {
        return damageDeal;
    }
}
