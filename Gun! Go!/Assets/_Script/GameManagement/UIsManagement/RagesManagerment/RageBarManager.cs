using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
internal class RageBarManager : MonoBehaviour {

    [Header("---------- Elements ----------")]
    [SerializeField] Image currentRageImage;
    [SerializeField] Slider currentRageSlider;

    [Header("---------- Variables ----------")]
    float rageLerpSpeed = 0.1f;

    [Header("---------- Components ----------")]
    IBossEnemy _enemy;

    private void Awake() {
        _enemy = GetComponentInParent<IBossEnemy>();

        currentRageSlider.maxValue = _enemy.GetBossEnemyMaxRage();
        currentRageSlider.value = _enemy.GetBossEnemyRage();

        UpdateHPBarUI();
    }

    private void FixedUpdate() {
        if (currentRageSlider.value != _enemy.GetBossEnemyRage()) {
            currentRageSlider.value = Mathf.Lerp(currentRageSlider.value, _enemy.GetBossEnemyRage(), rageLerpSpeed);
        }
        UpdateHPBarUI();
    }

    void UpdateHPBarUI() {
        float HPPercent = (float)_enemy.GetBossEnemyRage() / (float)_enemy.GetBossEnemyMaxRage();
        Color targetColor;

        if (HPPercent > 0.7f) {
            targetColor = new Color32(171, 0, 0, 255);
        }
        else if (HPPercent > 0.5f) {
            targetColor = new Color32(244, 255, 69, 255);
        }
        else {
            targetColor = new Color32(195, 194, 194, 255);
        }

        if (currentRageImage.color != targetColor) {
            currentRageImage.color = Color.Lerp(currentRageImage.color, targetColor, Time.deltaTime * 5f);
        }
    }
}
