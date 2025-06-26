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
        // Ledakkan bom setelah waktu tertentu
        Invoke(nameof(Explode), timer);
    }

    void Explode()
    {
        if (exploded) return;
        exploded = true;

        // Buat prefab ledakan
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Deteksi objek dalam radius ledakan
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D obj in colliders)
        {
            // Ledakan ke Player
            if (obj.CompareTag("Player"))
            {
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                Vector2 dir = (obj.transform.position - transform.position).normalized;

                if (rb != null)
                {
                    rb.AddForce(dir * explosionForce);
                }

                PlayerMovement player = obj.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.TakeDamage(20, dir);
                }

                Debug.Log("Player terkena ledakan!");
            }

            // Ledakan ke Enemy
            if (obj.CompareTag("Enemy"))
            {
                EnemyChase enemy = obj.GetComponent<EnemyChase>();
                if (enemy != null)
                {
                    enemy.TriggerBurning(); // Mainkan animasi meniup sumbu
                }

                Debug.Log("Enemy terkena ledakan!");
            }
        }

        // Beritahu BombThrower bahwa bom sudah meledak
        onExplosion?.Invoke();

        // Hancurkan objek bom
        Destroy(gameObject);
    }

    // Visualisasi radius di Scene View
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
