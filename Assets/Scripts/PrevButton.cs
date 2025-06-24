using UnityEngine;
using UnityEngine.SceneManagement;

public class PrevButton : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu"; // Ganti dengan nama scene-mu

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
