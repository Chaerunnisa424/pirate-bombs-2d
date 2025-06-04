using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public GameObject finishPanel; // Assign dari Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishPanel.SetActive(true);
            Time.timeScale = 0f; // Pause game (opsional)
        }
    }
}
