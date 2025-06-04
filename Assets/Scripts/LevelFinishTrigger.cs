using System.Collections;
using UnityEngine;

public class LevelFinishTrigger : MonoBehaviour
{
    public GameObject finishPanel;
    public float delayBeforeFinish = 2f; // delay dalam detik

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ShowFinishPanelWithDelay());
        }
    }

    private IEnumerator ShowFinishPanelWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeFinish);
        finishPanel.SetActive(true);
        Time.timeScale = 0f; // Memberhentikan game
    }
}
