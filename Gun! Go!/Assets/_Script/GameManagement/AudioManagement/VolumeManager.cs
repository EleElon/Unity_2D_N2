using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

internal class VolumeManager : MonoBehaviour {

    [Header("---------- UI References ----------")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] TextMeshProUGUI masterText;
    [SerializeField] TextMeshProUGUI musicText;
    [SerializeField] TextMeshProUGUI sfxText;

    [Header("---------- Audio Mixer")]
    [SerializeField] AudioMixer _audioMixer;

    const string MasterKey = "MasterVolume";
    const string MusicKey = "MusicVolume";
    const string SFXKey = "SFXVolume";

    void Start() {
        InitializeSlider(masterSlider, MasterKey, "MasterVolume", masterText);
        InitializeSlider(musicSlider, MusicKey, "MusicVolume", musicText);
        InitializeSlider(sfxSlider, SFXKey, "SFXVolume", sfxText);
    }

    void InitializeSlider(Slider slider, string key, string exposedParam, TextMeshProUGUI text) {
        float savedVolume = PlayerPrefs.GetFloat(key, 0.75f);
        slider.value = savedVolume;
        ApplyVolume(exposedParam, savedVolume, text);

        slider.onValueChanged.AddListener((value) => OnVolumeSliderChanged(value, key, exposedParam, text));
    }

    void OnVolumeSliderChanged(float value, string key, string exposedParam, TextMeshProUGUI text) {
        ApplyVolume(exposedParam, value, text);
        SaveVolume(key, value);
    }

    void ApplyVolume(string exposedParam, float value, TextMeshProUGUI text) {
        float boostdB = 2f;
        float volumeIndB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20 + boostdB * value;
        _audioMixer.SetFloat(exposedParam, volumeIndB);

        if (text != null) {
            text.text = $"{exposedParam.Replace("Volume", "")}: {(int)(value * 100)}%";
        }
    }

    void SaveVolume(string key, float value) {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }
}
