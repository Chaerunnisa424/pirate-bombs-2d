using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    private void Start()
    {
        // Pastikan game berjalan normal saat start
        Time.timeScale = 1f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
        else
            Debug.LogWarning("PauseMenuUI belum di-assign di Inspector!");
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseMenuUI == null)
        {
            Debug.LogError("PauseMenuUI tidak ditemukan!");
            return;
        }

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
        }

        Debug.Log("TogglePause dipanggil. Game paused: " + isPaused);
    }
}
