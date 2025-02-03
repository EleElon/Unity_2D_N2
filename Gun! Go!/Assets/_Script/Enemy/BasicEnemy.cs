using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BasicEnemy : Enemy {
    protected int maxHP = 30;
    protected int currentHP;

    EnemyHPManager _enemyHPManager;

    private void Awake() {
        currentHP = maxHP;

        _enemyHPManager = GetComponent<EnemyHPManager>();

        if (_enemyHPManager != null) {
            _enemyHPManager.SetEnemy(this); 
        } else {
            Debug.LogError("EnemyHPManager không tồn tại trên enemy: " + gameObject.name);
        }
    }

    protected override void Update() {
        base.Update();
        if (currentHP <= 0) {
            Die();
        }
    }

    internal override void TakeDMG(int dmg) {
        currentHP -= dmg;
    }

    internal int GetEnemyCurrentHP() {
        return currentHP;
    }

    internal int GetEnemyMaxHP() {
        return maxHP;
    }
}
