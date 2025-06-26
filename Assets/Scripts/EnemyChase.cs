using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header("Player Detection")]
    public Transform player;
    public float detectionRadius = 7f;
    public float chaseSpeed = 4f;

    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1.0f;
    private float lastAttackTime;

    [Header("Patrol Settings")]
    public float patrolSpeed = 2f;
    public Transform leftPoint;
    public Transform rightPoint;
    public float pauseDuration = 0.5f;

    [Header("Animation Parameters")]
    public string walkParam = "isWalking";
    public string attackParam = "isAttacking";
    public string burnParam = "isBurning";
    public string dieTrigger = "Die";
    public string belowTheWickTrigger = "belowthewick";

    private Vector3 patrolTarget;
    private bool movingRight = true;
    private bool isPaused = false;
    private bool isBurning = false;

    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (player == null || leftPoint == null || rightPoint == null)
        {
            Debug.LogError("Player atau patrol points belum di-assign.");
            enabled = false;
            return;
        }

        patrolTarget = rightPoint.position;
    }

    private void Update()
    {
        if (isBurning) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                anim.SetBool(attackParam, true);
                anim.SetBool(walkParam, false);
                lastAttackTime = Time.time;
            }
        }
        else if (distance <= detectionRadius)
        {
            anim.SetBool(attackParam, false);
            ChasePlayer();
        }
        else
        {
            anim.SetBool(attackParam, false);
            if (!isPaused)
                Patrol();
            else
                anim.SetBool(walkParam, false);
        }
    }

    void Patrol()
    {
        float distance = Vector2.Distance(transform.position, patrolTarget);
        transform.position = Vector3.MoveTowards(transform.position, patrolTarget, patrolSpeed * Time.deltaTime);

        anim.SetBool(walkParam, true);
        sprite.flipX = (patrolTarget.x < transform.position.x);

        if (distance < 0.1f && !isPaused)
        {
            anim.SetBool(walkParam, false);
            isPaused = true;
            Invoke(nameof(SwitchDirection), pauseDuration);
        }
    }

    void SwitchDirection()
    {
        movingRight = !movingRight;
        patrolTarget = movingRight ? rightPoint.position : leftPoint.position;
        isPaused = false;
    }

    void ChasePlayer()
    {
        // Mengejar secara horizontal saja (tidak mengikuti Y)
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);

        anim.SetBool(walkParam, true);
        sprite.flipX = (player.position.x < transform.position.x);
    }

    public void TriggerBurning()
    {
        isBurning = true;

        if (!string.IsNullOrEmpty(burnParam))
            anim.SetBool(burnParam, true);

        if (!string.IsNullOrEmpty(belowTheWickTrigger))
            anim.SetTrigger(belowTheWickTrigger);

        anim.SetBool(walkParam, false);
        anim.SetBool(attackParam, false);

        Destroy(gameObject, 1.5f);
    }

    public void DieFromBomb()
    {
        anim.SetTrigger(dieTrigger);
        Destroy(gameObject, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
