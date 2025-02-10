using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnergyEnemyCollision : EnemyCollision {

    [Header("---------- Variables ----------")]
    Coroutine attackCoroutine;

    [Header("---------- Components ----------")]
    EnergyEnemy _enemy;

    protected override void Awake() {
        _enemy = GetComponent<EnergyEnemy>();
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isCollision = true;
            // InvokeRepeating("DealDamage", 0, 1.5f);

            attackCoroutine = StartCoroutine(DealDamageAfterCalculate());
        }
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
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator CalculateHit() {
        for (int i = 0; i < _enemy.GetHit(); i++) {
            if (!isCollision) yield break;

            PlayerController.Instance.TakeDMG(_enemy.GetDamageDeal());

            yield return new WaitForSeconds(_enemy.GetDelayHit());
        }
    }
}
