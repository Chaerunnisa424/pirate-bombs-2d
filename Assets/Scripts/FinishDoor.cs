using UnityEngine;
using UnityEngine.SceneManagement;  // hanya jika kamu ingin load scene

public class FinishDoor : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            isOpened = true;
            animator.SetBool("Open", true);  // trigger animasi buka
            Invoke("LoadNextScene", 1f);     // delay sesuai durasi animasi
        }
    }

    void LoadNextScene()
    {
        Debug.Log("Level selesai!");
        // SceneManager.LoadScene("NextLevel"); // aktifkan jika mau pindah scene
    }
}
