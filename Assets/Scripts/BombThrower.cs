using UnityEngine;

public class BombThrower : MonoBehaviour
{
    [Header("Bomb Settings")]
    public GameObject bombPrefab;
    public Transform throwPoint;
    public int maxBombs = 2;

    private int currentBombCount = 0;

    [Header("Player Direction")]
    public bool playerFacingRight = true;

    void Update()
    {
        // Update arah pemain
        playerFacingRight = transform.localScale.x > 0;

        // Keyboard input
        if (Input.GetKeyDown(KeyCode.B))
        {
            ThrowBomb(); // Sama dengan tombol UI, pakai fungsi ini
        }
    }

    // Fungsi ini bisa dipanggil dari tombol UI juga
    public void ThrowBomb()
    {
        // Cek limit bomb
        if (currentBombCount >= maxBombs)
        {
            Debug.Log("Bomb limit reached. Tunggu bom meledak dulu.");
            return;
        }

        //// Instantiate bom
        //GameObject bomb = Instantiate(bombPrefab, throwPoint.position, Quaternion.identity);
        GameObject bomb = Instantiate(bombPrefab, throwPoint.position + Vector3.up * 0.2f, Quaternion.identity);


        // Pasang callback saat bom meledak
        Bomb bombScript = bomb.GetComponent<Bomb>();
        if (bombScript != null)
        {
            bombScript.onExplosion += OnBombExploded;
        }

        // Tambahkan gaya lempar
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float throwForceX = playerFacingRight ? 6f : -6f;
            rb.AddForce(new Vector2(throwForceX, 3.5f), ForceMode2D.Impulse);
        }

        currentBombCount++;
        Debug.Log("Bomb thrown! Total bombs: " + currentBombCount);
    }

    // Dipanggil dari script Bomb saat meledak
    void OnBombExploded()
    {
        currentBombCount--;
        if (currentBombCount < 0) currentBombCount = 0;
        Debug.Log("Bomb exploded. Remaining bombs: " + currentBombCount);
    }
}
