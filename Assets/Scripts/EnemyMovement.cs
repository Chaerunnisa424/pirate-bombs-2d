using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public Transform rightPoint;
    public Transform leftPoint;

    private Vector3 targetPosition;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool movingRight = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (leftPoint == null || rightPoint == null)
        {
            Debug.LogError("LeftPoint dan RightPoint belum di-assign di Inspector.");
            enabled = false;
            return;
        }

        // Mulai bergerak ke kanan dulu
        targetPosition = rightPoint.position;
        movingRight = true;
        anim.SetBool("isWalking", true);
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        // Gerak menuju target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Jika sudah sampai target, ganti arah
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingRight = !movingRight;  // toggle arah
            targetPosition = movingRight ? rightPoint.position : leftPoint.position;
        }

        // Flip sprite sesuai arah
        sprite.flipX = !movingRight;

        anim.SetBool("isWalking", true);
    }
}
