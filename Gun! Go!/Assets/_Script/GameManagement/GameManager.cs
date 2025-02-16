using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class GameManager : MonoBehaviour {
    internal static GameManager Instance { get; private set; }

    [Header("---------- Variables ----------")]
    bool isGamePaused = false;
    bool isGameOver = false;

    int gameProgress, maxGameProgress = 50;

    void Awake() {
        Instance = this;
    }

    void Update() {
        HandleEscapeInput();

        if (!isGameOver) {
            if (gameProgress == maxGameProgress && EnemySpawnManager.Instance.gameObject.activeSelf == true) {
                CallBoss();
            }

            if (EnemySpawnManager.Instance?.gameObject.activeSelf == false) {
                BossEnemyOP.Instance.GetBossEnemy();
            }
        }
        else {
            GameOver();
        }
    }

    void HandleEscapeInput() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            HandleEscapeKey();
        }
    }

    void HandleEscapeKey() {
        if (isGameOver) {
            return;
        }
        else if (!UIsManager.Instance.IsShowGamePausePanelActivated() && !isGamePaused) {
            UIsManager.Instance.SetGamePausePanel(true);

            Pause(true);
        }
        else if (UIsManager.Instance.IsShowGameSettingPanelActivated()) {
            UIsManager.Instance.SetGameSettingPanel(false);
            UIsManager.Instance.SetGamePausePanel(true);
        }
        else if (isGamePaused) {
            UIsManager.Instance.SetGamePausePanel(false);

            Pause(false);
        }
    }

    void CallBoss() {
        EnemySpawnManager.Instance?.gameObject.SetActive(false);
    }

    internal void IncreGameProgress() {
        gameProgress++;
    }

    internal int SetGameProgress(int progress) {
        return gameProgress = progress;
    }

    void GameOver() {
        UIsManager.Instance.SetGameOverPanel(true);
        Pause(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene("GamePlay");
        Pause(false);
        isGameOver = false;
    }

    public void Continue() {
        UIsManager.Instance.SetGamePausePanel(false);
        Pause(false);
    }

    public void Setting() {
        UIsManager.Instance.SetGamePausePanel(false);
        UIsManager.Instance.SetGameOverPanel(false);
        UIsManager.Instance.SetGameSettingPanel(true);
    }

    public void BackToMenu() {

    }

    void Pause(bool state) {
        SetGamePause(state);
        Time.timeScale = state ? 0 : 1;
    }

    void SetGamePause(bool state) {
        isGamePaused = state;
    }

    internal bool IsGamePaused() {
        return isGamePaused;
    }

    internal void SetGameOver(bool state) {
        isGameOver = state;
    }

    internal bool IsGameOver() {
        return isGameOver;
    }

    internal int GetMaxGameProgress() {
        return maxGameProgress;
    }

    internal int GetCurrentGameProgress() {
        return gameProgress;
    }
}
