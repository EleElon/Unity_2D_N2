using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MiniEnemy : Enemy, IEnemy {

    [Header("---------- Variables ----------")]
    protected new float moveSpeed { get; set; } = 0f;
    protected new int damageDeal { get; set; } = 1;
    protected int maxHP = 8;
    protected int currentHP;

    public int GetEnemiesMaxHP() => maxHP;
    public int GetEnemiesCurrentHP() => currentHP;

    [Header("---------- Components ----------")]
    EnemyHPManager _enemyHPManager;

    protected override void OnEnable() {
        base.OnEnable();

        currentHP = maxHP;

        SetMiniEnemySpeed();
    }

    protected override void Awake() {
        base.Awake();

        _enemyHPManager = GetComponentInChildren<EnemyHPManager>();
    }

    protected override void Update() {
        base.Update();
        FlipEnemyLevel();

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

    void SetMiniEnemySpeed() {
        float newSpeed = Random.Range(2f, 3.1f);

        moveSpeed = newSpeed;
    }

    internal override void TakeDMG(int dmg) {
        currentHP -= dmg;
    }

    internal override int GetDamageDeal() {
        return damageDeal;
    }

    internal override void Die() {
        base.Die();

        MiniEnemyOP.Instance?.ReturnMiniEnemy(transform.parent.gameObject);
    }

    internal override float GetMoveSpeed() {
        return moveSpeed;
    }

    protected override int SetDamageDeal() {
        int newDamageDeal = Mathf.RoundToInt(damageDeal + ((damageDeal + (level * 1.25f)) * 1.2f));
        return damageDeal = newDamageDeal;
    }

    protected override int SetMaxHP() {
        return maxHP = Mathf.RoundToInt(maxHP + (maxHP * 1.3f) + level);
    }

    protected override void ResetEnemyState() {
        currentHP = maxHP;
        // ResetParentGameObjectPosition();
        // ResetGameObjectPosition();
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
