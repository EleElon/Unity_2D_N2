using UnityEngine;
using UnityEngine.SceneManagement;

internal class MenuManager : MonoBehaviour {
    public void NewGame() {
        SceneManager.LoadScene("GamePlay");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
