using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class Enemy : MonoBehaviour {

    [Header("---------- Variables ----------")]
    protected virtual float moveSpeed { get; } = 2;
    // protected float moveSpeed = 3f;
    protected virtual int damageDeal { get; } = 10;

    protected int level, baseExp = 2;

    [Header("---------- Components -----------")]
    protected EnemyLevelUIManager _enemyLevelUIManager;

    protected virtual void OnEnable() {
        if (_enemyLevelUIManager == null) {
            _enemyLevelUIManager = GetComponentInChildren<EnemyLevelUIManager>();
        }

        ResetEnemyState();

        SetLevel();
        SetBaseExp();
        SetDamageDeal();
        SetMaxHP();

        _enemyLevelUIManager.SetEnemyLevelText("Lv: " + level);
    }

    protected virtual void Awake() {
        _enemyLevelUIManager = GetComponentInChildren<EnemyLevelUIManager>();
    }

    protected virtual void Update() {
        MoveToPlayer();
    }

    // protected void MoveToPlayer() {
    //     if (PlayerController.Instance != null) {
    //         transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.transform.position, GetMoveSpeed() * Time.deltaTime);
    //         FlipEnemy();
    //     }
    // }

    protected void MoveToPlayer() {
        if (PlayerController.Instance != null) {
            Vector2 direction = (PlayerController.Instance.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.3f, LayerMask.GetMask("Wall"));

            if (hit.collider == null) {
                transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.transform.position, GetMoveSpeed() * Time.deltaTime);
            }
            else {
                AvoidObstacle();
            }

            FlipEnemy();
        }
    }

    void AvoidObstacle() {
        Vector2 alternativeDirection = new Vector2(-1, 0);
        transform.position += (Vector3)alternativeDirection * GetMoveSpeed() * Time.deltaTime;
    }


    protected virtual void FlipEnemy() {
        if (PlayerController.Instance != null) {
            transform.localScale = new Vector3(PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    protected virtual void FlipEnemyLevel() {
        if (PlayerController.Instance != null) {
            _enemyLevelUIManager.transform.localScale = new Vector3(PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    internal virtual float GetMoveSpeed() {
        return moveSpeed;
    }

    internal virtual int GetDamageDeal() {
        return damageDeal;
    }

    void SetLevel() {
        if (PlayerController.Instance == null) {
            level = 1;
            return;
        }
        int rdWhenPlayerlevelLow = Random.Range(1, 5);
        level = Random.Range(PlayerController.Instance.GetPlayerLevel() > 4 ? PlayerController.Instance.GetPlayerLevel() - 3 : rdWhenPlayerlevelLow, PlayerController.Instance.GetPlayerLevel() + 5);
    }

    int SetBaseExp() {
        baseExp = Mathf.RoundToInt(baseExp + (baseExp * 1.1f));
        return baseExp;
    }

    internal int GetEnemyLevel() {
        return level;
    }

    internal virtual void Die() {
        PlayerController.Instance.GainExp(baseExp);
        GameManager.Instance?.IncreGameProgress();
    }

    internal abstract void TakeDMG(int dmg);

    protected abstract void ResetEnemyState();

    protected abstract void ResetGameObjectPosition();

    protected abstract void ResetParentGameObjectPosition();

    protected abstract int SetDamageDeal();

    protected abstract int SetMaxHP();
}
