using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
internal class PlayerHPManager : MonoBehaviour {

    internal static PlayerHPManager Instance { get; private set; }

    [Header("---------- Components ----------")]
    [SerializeField] Image currentHPImage;
    [SerializeField] Image easeHPImage;
    [SerializeField] Slider currentHPSlider;
    [SerializeField] Slider easeHPSlider;

    [Header("---------- Variables ----------")]
    float easeLerpSpeed = 0.01f;
    float hpLerpSpeed = 0.1f;
    float timeWaitForEaseHP = 1.7f;
    float lastDMGTakeTime;

    private void Awake() {
        Instance = this;

        currentHPSlider.maxValue = PlayerController.Instance.GetMaxHP();
        currentHPSlider.value = PlayerController.Instance.GetCurrentHP();
        easeHPSlider.maxValue = PlayerController.Instance.GetMaxHP();
        easeHPSlider.value = PlayerController.Instance.GetCurrentHP();

        UpdateHPBarUI();
    }

    private void FixedUpdate() {
        if (currentHPSlider.value != PlayerController.Instance.GetCurrentHP()) {
            currentHPSlider.value = Mathf.Lerp(currentHPSlider.value, PlayerController.Instance.GetCurrentHP(), hpLerpSpeed);
        }

        if (Time.time - lastDMGTakeTime >= timeWaitForEaseHP && PlayerController.Instance.GetCurrentHP() != 0) {
            if (currentHPSlider.value != easeHPSlider.value) {
                easeHPSlider.value = Mathf.Lerp(easeHPSlider.value, PlayerController.Instance.GetCurrentHP(), easeLerpSpeed);
            }
        }
        UpdateHPBarUI();
    }

    void UpdateHPBarUI() {
        float HPPercent = (float)PlayerController.Instance.GetCurrentHP() / (float)PlayerController.Instance.GetMaxHP();
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

        if (currentHPImage.color != targetColor) {
            currentHPImage.color = Color.Lerp(currentHPImage.color, targetColor, Time.deltaTime * 5f);
        }
    }

    internal void ResetHPBarState() {
        currentHPSlider.maxValue = PlayerController.Instance.GetMaxHP();
        easeHPSlider.maxValue = PlayerController.Instance.GetMaxHP();
    }

    internal void SetLastDMGTakeDMG(float time) {
        lastDMGTakeTime = time;
    }
}
