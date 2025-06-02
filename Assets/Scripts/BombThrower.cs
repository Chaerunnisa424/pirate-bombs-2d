using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform throwPoint;
    public int maxBombs = 2;

    private int currentBombCount = 0;

    // Misal kamu punya cara track arah player, contoh:
    public bool playerFacingRight = true;

    void Update()
    {
        // Update arah player berdasarkan scale.x
        playerFacingRight = transform.localScale.x > 0;

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (currentBombCount < maxBombs)
            {
                ThrowBomb();
            }
            else
            {
                Debug.Log("Bomb limit reached, wait for explosions");
            }
        }
    }

    void ThrowBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, throwPoint.position, Quaternion.identity);

        Bomb bombScript = bomb.GetComponent<Bomb>();
        if (bombScript != null)
        {
            bombScript.onExplosion += OnBombExploded;
        }

        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float throwForceX = playerFacingRight ? 8f : -8f;   // Sesuaikan kecepatan lemparan horizontal
            rb.AddForce(new Vector2(throwForceX, 3.5f), ForceMode2D.Impulse);  // Kurangi gaya vertikal supaya gak terlalu tinggi
        }

        currentBombCount++;
    }


    void OnBombExploded()
    {
        currentBombCount--;
        if (currentBombCount < 0) currentBombCount = 0;
    }
}
