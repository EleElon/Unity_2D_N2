using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
internal class EnemyHPManager : MonoBehaviour {

    [Header("---------- Elements ----------")]
    [SerializeField] Slider currentHPSlider;
    [SerializeField] Slider easeHPSlider;

    [Header("---------- Variables ----------")]
    float easeLerpSpeed = 0.01f;
    float hpLerpSpeed = 1.1f;
    float timeWaitForEaseHP = 3f;
    float lastDMGTakeTime;

    [Header("---------- Components ----------")]
    BasicEnemy _enemy;

    private void Awake() {
        currentHPSlider.maxValue = _enemy.GetEnemyMaxHP();
        currentHPSlider.value = _enemy.GetEnemyMaxHP();
        easeHPSlider.maxValue = _enemy.GetEnemyMaxHP();
        easeHPSlider.value = _enemy.GetEnemyCurrentHP();
    }

    private void FixedUpdate() {
        if (currentHPSlider.value != _enemy.GetEnemyCurrentHP()) {
            currentHPSlider.value = Mathf.Lerp(currentHPSlider.value, _enemy.GetEnemyCurrentHP(), hpLerpSpeed);
        }

        if (Time.time - lastDMGTakeTime >= timeWaitForEaseHP && _enemy.GetEnemyCurrentHP() != 0) {
            if (currentHPSlider.value != easeHPSlider.value) {
                easeHPSlider.value = Mathf.Lerp(easeHPSlider.value, _enemy.GetEnemyCurrentHP(), easeLerpSpeed);
            }
        }
    }

    internal void SetEnemy(BasicEnemy enemy) {
        _enemy = enemy;
    }

    internal void SetLastDMGTakeDMG(float time) {
        lastDMGTakeTime += time;
    }
}
