using UnityEngine;
using System.Collections;

public class DestroyAfterAnimation : MonoBehaviour
{
    public float delay = 0.5f;
    public float explosionRadius = 1.5f;
    public float knockBackForce = 10f;
    public int damage = 100;

    void Start()
    {
        // Mulai proses ledakan selama delay detik
        StartCoroutine(ExplodeDuringLifetime());
    }

    IEnumerator ExplodeDuringLifetime()
    {
        float timer = 0f;

        while (timer < delay)
        {
            Explode();
            timer += Time.deltaTime;
            yield return null;
        }

        // Hancurkan efek ledakan
        Destroy(gameObject);
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

                // Jika musuh memiliki health system
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                else
                {
                    // Jika tidak, langsung dihancurkan
                    Destroy(hit.gameObject);
                }
            }
        }
    }

    // Menampilkan radius ledakan di editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
