using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ExplosionEnemy : Enemy, IEnemy {

    [Header("---------- Variables ----------")]
    // protected float moveSpeedOfBasicEnemy = 0.2f;
    protected override float moveSpeed { get; } = 2.3f;
    protected override int damageDeal { get; } = 0;
    int explosionDMG = 20;
    protected int maxHP = 15;
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

    internal override void Die() {
        GameObject explosion = ExplosionOP.Instance.GetExplosion();
        explosion.transform.position = gameObject.transform.position;

        base.Die();
    }

    internal override void TakeDMG(int dmg) {
        currentHP -= dmg;
    }

    protected override float GetMoveSpeed() {
        return moveSpeed;
    }

    internal int GetExplosionDMG() {
        return explosionDMG;
    }
}
