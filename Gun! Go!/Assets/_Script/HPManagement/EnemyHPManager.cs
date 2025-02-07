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
    float timeWaitForEaseHP = 1.7f;
    float lastDMGTakeTime;

    [Header("---------- Components ----------")]
    IEnemy _enemy;

    private void Awake() {
        _enemy = GetComponentInParent<IEnemy>();

        currentHPSlider.maxValue = _enemy.GetEnemiesMaxHP();
        currentHPSlider.value = _enemy.GetEnemiesMaxHP();
        easeHPSlider.maxValue = _enemy.GetEnemiesMaxHP();
        easeHPSlider.value = _enemy.GetEnemiesMaxHP();
    }

    private void FixedUpdate() {
        if (currentHPSlider.value != _enemy.GetEnemiesCurrentHP()) {
            currentHPSlider.value = Mathf.Lerp(currentHPSlider.value, _enemy.GetEnemiesCurrentHP(), hpLerpSpeed);
        }

        if (Time.time - lastDMGTakeTime >= timeWaitForEaseHP && _enemy.GetEnemiesCurrentHP() != 0) {
            if (currentHPSlider.value != easeHPSlider.value) {
                easeHPSlider.value = Mathf.Lerp(easeHPSlider.value, _enemy.GetEnemiesCurrentHP(), easeLerpSpeed);
            }
        }
    }

    internal void SetLastDMGTakeDMG(float time) {
        lastDMGTakeTime = time;
    }
}
