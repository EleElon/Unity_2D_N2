using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnergyEnemyCollision : EnemyCollision {

    [Header("---------- Variables ----------")]
    float timeToAttack = 1.5f;
    Coroutine attackCoroutine;

    [Header("---------- Components ----------")]
    EnergyEnemy _energyEnemy;

    protected override void Awake() {
        _energyEnemy = GetComponent<EnergyEnemy>();
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isCollision = true;
            // InvokeRepeating("DealDamage", 0, 1.5f);

            attackCoroutine = StartCoroutine(DealDamageAfterCalculate());
        }
        IfCollisionWithBullet(other);
    }

    protected override void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isCollision = false;
            // CancelInvoke("DealDamage");

            if (attackCoroutine != null) {
                StopCoroutine(DealDamageAfterCalculate());
                attackCoroutine = null;
            }
        }
    }

    IEnumerator DealDamageAfterCalculate() {
        while (isCollision) {
            yield return StartCoroutine(CalculateHit());
            yield return new WaitForSeconds(timeToAttack);
        }
    }

    IEnumerator CalculateHit() {
        for (int i = 0; i < _energyEnemy.GetHit(); i++) {
            if (!isCollision) yield break;

            PlayerController.Instance?.TakeDMG(_energyEnemy.GetDamageDeal());

            yield return new WaitForSeconds(_energyEnemy.GetDelayHit());
        }
    }
}
