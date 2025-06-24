using UnityEngine;
using UnityEngine.SceneManagement; // Memungkinkan penggunaan SceneManager

public class LevelManager : MonoBehaviour
{
    // Fungsi untuk memuat level berikutnya
    public void LoadNextLevel()
    {
        // Ambil scene saat ini
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Muat scene berikutnya berdasarkan index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
