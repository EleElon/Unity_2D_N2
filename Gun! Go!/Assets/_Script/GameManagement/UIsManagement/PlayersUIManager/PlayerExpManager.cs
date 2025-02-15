using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
internal class PlayerExpManager : MonoBehaviour {

    [Header("---------- Elements ----------")]
    [SerializeField] Slider currentExpSlider;

    [Header("---------- Variables ----------")]
    float expLerpSpeed = 0.1f;

    private void Awake() {

        currentExpSlider.maxValue = PlayerController.Instance.GetMaxExp();
        currentExpSlider.value = PlayerController.Instance.GetCurrentExp();
    }

    private void FixedUpdate() {
        if (currentExpSlider.value != PlayerController.Instance.GetCurrentExp()) {
            currentExpSlider.value = Mathf.Lerp(currentExpSlider.value, PlayerController.Instance.GetCurrentExp(), expLerpSpeed);
        }
    }
}
