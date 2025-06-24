using UnityEngine;

public class GoldCollector : MonoBehaviour
{
    public int totalGold = 5;
    public int collectedGold = 0;
    public FinishDoor finishDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gold"))
        {
            collectedGold++;
            Destroy(collision.gameObject);

            if (collectedGold >= totalGold)
            {
                finishDoor.OpenFinishDoor(); // Hanya buka pintu
            }
        }
    }
}
