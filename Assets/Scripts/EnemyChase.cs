using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;
    public float moveSpeed = 2f;

    private Animator anim;
    private SpriteRenderer sprite;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            Debug.Log("Chasing player!");
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            anim.SetBool("isWalking", true);
            sprite.flipX = (player.position.x < transform.position.x);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
