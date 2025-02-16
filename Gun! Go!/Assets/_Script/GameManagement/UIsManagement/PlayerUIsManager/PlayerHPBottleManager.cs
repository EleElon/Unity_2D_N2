using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
internal class PlayerHPBottleManager : MonoBehaviour {

    [Header("---------- Components ----------")]
    [SerializeField] Image currentHPBottleImage;
    [SerializeField] Image easeHPBottleImage;
    [SerializeField] Slider currentHPBottleSlider;
    [SerializeField] Slider easeHPBottleSlider;

    [Header("---------- Variables ----------")]
    float easeLerpSpeed = 0.01f;
    float hpLerpSpeed = 0.1f;

    private void Awake() {

        currentHPBottleSlider.maxValue = PlayerController.Instance.GetMaxHPBottle();
        currentHPBottleSlider.value = PlayerController.Instance.GetCurrentHPBottle();
        easeHPBottleSlider.maxValue = PlayerController.Instance.GetMaxHPBottle();
        easeHPBottleSlider.value = currentHPBottleSlider.value;

        UpdateHPBarUI();
    }

    private void FixedUpdate() {
        if (currentHPBottleSlider.value != PlayerController.Instance.GetCurrentHPBottle()) {
            currentHPBottleSlider.value = Mathf.Lerp(currentHPBottleSlider.value, PlayerController.Instance.GetCurrentHPBottle(), hpLerpSpeed);
        }

        if (currentHPBottleSlider.value != easeHPBottleSlider.value) {
            easeHPBottleSlider.value = Mathf.Lerp(easeHPBottleSlider.value, PlayerController.Instance.GetCurrentHPBottle(), easeLerpSpeed);
        }

        UpdateHPBarUI();
    }

    void UpdateHPBarUI() {
        float HPPercent = (float)PlayerController.Instance.GetCurrentHPBottle() / (float)PlayerController.Instance.GetMaxHPBottle();
        Color targetColor;

        if (HPPercent > 0.5f) {
            targetColor = new Color32(37, 255, 0, 255);
        }
        else if (HPPercent > 0.25f) {
            targetColor = new Color32(255, 136, 0, 255);
        }
        else {
            targetColor = new Color32(255, 0, 0, 255);
        }

        if (currentHPBottleImage.color != targetColor) {
            currentHPBottleImage.color = Color.Lerp(currentHPBottleImage.color, targetColor, Time.deltaTime * 5f);
        }
    }

    internal void ResetEnemyHPBarState() {
        easeHPBottleSlider.value = PlayerController.Instance.GetCurrentHPBottle();
    }
}
