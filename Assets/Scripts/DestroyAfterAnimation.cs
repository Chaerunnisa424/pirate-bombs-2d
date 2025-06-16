using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    public float delay = 0.5f;
    public float explosionRadius = 1.5f;
    public float knockBackForce = 10f;
    public int damage = 100;

    void Start()
    {
        // Langsung ledakkan saat animasi dimulai
        Explode();
        // Hancurkan efek ledakan setelah delay
        Destroy(gameObject, delay);
    }

    void Explode()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hitEnemies)
        {
            if (hit.CompareTag("Enemy"))
            {
                KnockBack kb = hit.GetComponent<KnockBack>();
                if (kb != null)
                {
                    kb.GetKnockedBack(transform, knockBackForce);
                }

                Destroy(hit.gameObject); // Hancurkan enemy
            }
        }
    }

    // Untuk bantu lihat radius di editor Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
