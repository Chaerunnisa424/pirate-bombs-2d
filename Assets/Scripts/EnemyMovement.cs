using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform rightPoint;
    public Transform leftPoint;
    public float pauseDuration = 0.5f;

    private Vector3 targetPosition;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool movingRight = true;
    private bool isPaused = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (leftPoint == null || rightPoint == null)
        {
            Debug.LogError("LeftPoint dan RightPoint belum di-assign.");
            enabled = false;
            return;
        }

        targetPosition = rightPoint.position;
        movingRight = true;
    }

    private void Update()
    {
        if (!isPaused)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        float distance = Vector2.Distance(transform.position, targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (distance < 0.1f)
        {
            anim.SetBool("isWalking", false);
            isPaused = true;
            Invoke(nameof(SwitchDirection), pauseDuration);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        sprite.flipX = !movingRight;
    }

    private void SwitchDirection()
    {
        movingRight = !movingRight;
        targetPosition = movingRight ? rightPoint.position : leftPoint.position;
        isPaused = false;
    }
}
