using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Dipanggil oleh GoldCollector saat semua koin terkumpul
    public void OpenFinishDoor()
    {
        if (!isOpened)
        {
            isOpened = true;
            animator.SetBool("Open", true); // Mainkan animasi pintu
        }
    }

    // Ini dipanggil saat player menyentuh pintu SETELAH terbuka
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isOpened)
        {
            Debug.Log("Player sudah mencapai pintu setelah pintu terbuka.");
            // Jangan pindah scene! Biarkan script lain (misal LevelFinishTrigger) yang handle tampilan panel.
            // Bisa kasih event / trigger di sini kalau mau.
        }
    }

    // Getter agar bisa dicek status terbukanya dari luar
    public bool IsOpened()
    {
        return isOpened;
    }
}
