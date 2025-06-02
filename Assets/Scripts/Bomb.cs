using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Delegate untuk mengirim sinyal ke BombThrower setelah bom meledak
    public delegate void BombEvent();
    public event BombEvent onExplosion;

    [Header("Explosion Settings")]
    public GameObject explosionPrefab;   // Prefab ledakan
    public float timer = 3f;             // Detik sebelum meledak
    public float explosionForce = 500f;  // Gaya dorong ke player
    public float explosionRadius = 3f;   // Radius efek ledakan

    private bool exploded = false;       // Mencegah meledak dua kali

    void Start()
    {
        // Panggil fungsi ledakan setelah delay
        Invoke(nameof(Explode), timer);
    }

    void Explode()
    {
        if (exploded) return;
        exploded = true;

        // Spawn animasi ledakan jika ada
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Cek objek dalam radius ledakan
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Player"))
            {
                Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = (rb.transform.position - transform.position).normalized;
                    rb.AddForce(direction * explosionForce);
                    // Di sini kamu bisa menambahkan efek mati / damage
                    Debug.Log("Player terkena ledakan!");
                }
            }
        }

        // Panggil event (misalnya untuk BombThrower agar bisa lempar lagi)
        onExplosion?.Invoke();

        // Hancurkan bom ini setelah ledakan
        Destroy(gameObject);
    }

    // Untuk menampilkan radius ledakan saat object dipilih di Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
