using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 20;
    public float knockbackForce = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        KnockBack knockBack = collision.GetComponent<KnockBack>();

        if (player != null && knockBack != null)
        {
            Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;

            player.TakeDamage(damage, knockbackDir); // Kurangi HP
            knockBack.GetKnockedBack(transform, knockbackForce); // Terapkan knockback
        }
    }
}

