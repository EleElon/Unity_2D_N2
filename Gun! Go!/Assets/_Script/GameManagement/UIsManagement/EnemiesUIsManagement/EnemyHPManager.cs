using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
internal class EnemyHPManager : MonoBehaviour {

    [Header("---------- Components ----------")]
    [SerializeField] Image currentHPImage;
    [SerializeField] Image easeHPImage;
    [SerializeField] Slider currentHPSlider;
    [SerializeField] Slider easeHPSlider;
    IEnemy _enemy;

    [Header("---------- Variables ----------")]
    float easeLerpSpeed = 0.01f;
    float hpLerpSpeed = 0.1f;
    float timeWaitForEaseHP = 1.7f;
    float lastDMGTakeTime;

    void OnEnable() {
        _enemy = GetComponentInParent<IEnemy>();

        currentHPSlider.maxValue = _enemy.GetEnemiesMaxHP();
        currentHPSlider.value = _enemy.GetEnemiesMaxHP();
        easeHPSlider.maxValue = _enemy.GetEnemiesMaxHP();
        easeHPSlider.value = _enemy.GetEnemiesMaxHP();
    }

    // private void Awake() {
    //     _enemy = GetComponentInParent<IEnemy>();

    //     currentHPSlider.maxValue = _enemy.GetEnemiesMaxHP();
    //     currentHPSlider.value = _enemy.GetEnemiesMaxHP();
    //     easeHPSlider.maxValue = _enemy.GetEnemiesMaxHP();
    //     easeHPSlider.value = _enemy.GetEnemiesMaxHP();

    //     UpdateHPBarUI();
    // }

    private void FixedUpdate() {
        if (currentHPSlider.value != _enemy.GetEnemiesCurrentHP()) {
            currentHPSlider.value = Mathf.Lerp(currentHPSlider.value, _enemy.GetEnemiesCurrentHP(), hpLerpSpeed);
        }

        if (Time.time - lastDMGTakeTime >= timeWaitForEaseHP && _enemy.GetEnemiesCurrentHP() != 0) {
            if (currentHPSlider.value != easeHPSlider.value) {
                easeHPSlider.value = Mathf.Lerp(easeHPSlider.value, _enemy.GetEnemiesCurrentHP(), easeLerpSpeed);
            }
        }
        UpdateHPBarUI();
    }

    void UpdateHPBarUI() {
        float HPPercent = (float)_enemy.GetEnemiesCurrentHP() / (float)_enemy.GetEnemiesMaxHP();
        Color targetColor;

        if (HPPercent > 0.5f) {
            targetColor = new Color32(14, 255, 0, 255);
        }
        else if (HPPercent > 0.25f) {
            targetColor = new Color32(255, 136, 0, 255);
        }
        else {
            targetColor = new Color32(255, 0, 0, 255);
        }

        if (currentHPImage.color != targetColor) {
            currentHPImage.color = Color.Lerp(currentHPImage.color, targetColor, Time.deltaTime * 5f);
        }
    }

    internal void ResetEnemyHPBarState() {
        easeHPSlider.value = _enemy.GetEnemiesCurrentHP();
    }

    internal void SetLastDMGTakeDMG(float time) {
        lastDMGTakeTime = time;
    }
}
