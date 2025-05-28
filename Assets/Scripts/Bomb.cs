using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;   // prefab ledakan
    public float fuseTime = 2f;          // waktu jeda sebelum meledak (detik)

    void Start()
    {
        // Jalankan fungsi Explode setelah fuseTime detik
        Invoke("Explode", fuseTime);
    }

    void Explode()
    {
        // Jika prefab ledakan tersedia, munculkan efek ledakan
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Hancurkan objek bom
        Destroy(gameObject);
    }
}
