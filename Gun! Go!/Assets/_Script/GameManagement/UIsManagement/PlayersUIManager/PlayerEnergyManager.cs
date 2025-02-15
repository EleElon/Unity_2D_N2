using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
internal class PlayerEnergyManager : MonoBehaviour {

    [Header("---------- Elements ----------")]
    [SerializeField] Slider currentEnergySlider;
    [SerializeField] Slider easeEnergySlider;

    [Header("---------- Variables ----------")]
    float easeLerpSpeed = 0.01f;
    float energyLerpSpeed = 0.1f;

    private void Awake() {

        currentEnergySlider.maxValue = PlayerController.Instance.GetMaxEnergy();
        currentEnergySlider.value = PlayerController.Instance.GetCurrentEnergy();
        easeEnergySlider.maxValue = PlayerController.Instance.GetMaxEnergy();
        easeEnergySlider.value = PlayerController.Instance.GetMaxEnergy();
    }

    private void FixedUpdate() {
        if (currentEnergySlider.value != PlayerController.Instance.GetCurrentEnergy()) {
            currentEnergySlider.value = Mathf.Lerp(currentEnergySlider.value, PlayerController.Instance.GetCurrentEnergy(), energyLerpSpeed);
        }

            if (currentEnergySlider.value != easeEnergySlider.value) {
                easeEnergySlider.value = Mathf.Lerp(easeEnergySlider.value, PlayerController.Instance.GetCurrentEnergy(), easeLerpSpeed);
            }
    }
}
