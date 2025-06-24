using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 20;
    public float knockbackForce = 5f;

    public int health = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        KnockBack knockBack = collision.GetComponent<KnockBack>();

        if (player != null && knockBack != null)
        {
            Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;

            player.TakeDamage(damage, knockbackDir);
            knockBack.GetKnockedBack(transform, knockbackForce);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Tambahkan animasi/efek kematian jika perlu
        Destroy(gameObject);
    }
}
