using System.Collections;
using UnityEngine;

public class LevelFinishTrigger : MonoBehaviour
{
    public GameObject finishPanel;
    public float delayBeforeFinish = 1f;
    public FinishDoor finishDoor; // Tambahkan referensi ke pintu

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && finishDoor.IsOpened())
        {
            StartCoroutine(ShowFinishPanelWithDelay());
        }
    }

    private IEnumerator ShowFinishPanelWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeFinish);
        finishPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
