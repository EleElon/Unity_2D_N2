using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
internal class GameProgressManager : MonoBehaviour {

    [Header("---------- Components ----------")]
    [SerializeField] Slider currentGameProgressSlider;

    [Header("---------- Variables ----------")]
    float gameProgressLerpSpeed = 0.1f;

    private void Start() {

        currentGameProgressSlider.maxValue = GameManager.Instance.GetMaxGameProgress();
        currentGameProgressSlider.value = GameManager.Instance.GetCurrentGameProgress();
    }

    private void FixedUpdate() {
        if (currentGameProgressSlider.value != PlayerController.Instance.GetCurrentExp()) {
            currentGameProgressSlider.value = Mathf.Lerp(currentGameProgressSlider.value, GameManager.Instance.GetCurrentGameProgress(), gameProgressLerpSpeed);
        }
    }
}
