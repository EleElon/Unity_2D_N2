using TMPro;
using UnityEngine;

internal class UIsManager : MonoBehaviour {

    internal static UIsManager Instance { get; private set; }

    [Header("---------- Text ----------")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI bulletText;

    [Header("---------- Panels ----------")]
    [SerializeField] GameObject gamePausePanel;
    [SerializeField] GameObject gameSettingPanel;
    [SerializeField] GameObject gameOverPanel;

    void Awake() {
        Instance = this;
    }

    internal void SetGamePausePanel(bool isShow) {
        if (gamePausePanel)
            gamePausePanel.SetActive(isShow);
    }

    internal bool IsShowGamePausePanelActivated() {
        return gamePausePanel.activeInHierarchy;
    }

    internal void SetGameSettingPanel(bool isShow) {
        if (gameSettingPanel)
            gameSettingPanel.SetActive(isShow);
    }

    internal bool IsShowGameSettingPanelActivated() {
        return gameSettingPanel.activeInHierarchy;
    }

    internal void SetGameOverPanel(bool isShow) {
        if (gameOverPanel)
            gameOverPanel.SetActive(isShow);
    }

    internal bool IsShowGameOverPanelActivated() {
        return gameOverPanel.activeInHierarchy;
    }

    internal void SetLevelText(string txt) {
        if (levelText)
            levelText.text = txt;
    }

    internal void SetBulletText(string current, string max) {
        if (bulletText)
            bulletText.text = $"{current}{max}";
    }
}
