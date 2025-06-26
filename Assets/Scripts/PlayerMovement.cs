using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private PlayerController playerController;

    private float mobileInputX = 0f;
    private Vector2 moveInput;

    private enum MovementState { idle, walk, jump, fall }

    [Header("Ground Check")]
    [SerializeField] private LayerMask jumpableGround;
    private BoxCollider2D coll;

    [Header("Health System")]
    public GameObject[] hearts; // drag 3 image heart
    private int maxLives = 3;
    private int currentLives;

    [Header("Knockback Settings")]
    [SerializeField] private float knockBackTime = 0.2f;
    [SerializeField] private float knockBackThrust = 10f;
    private bool isKnockedBack = false;

    [Header("UI")]
    public GameObject gameOverPanel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        playerController = new PlayerController();

        currentLives = maxLives;
        UpdateHeartUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        playerController.Enable();
        playerController.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerController.Movement.Move.canceled += ctx => moveInput = Vector2.zero;
        playerController.Movement.Jump.performed += ctx => Jump();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    private void Update()
    {
        // Hanya update input jika di mobile, untuk UI buttons
        if (Application.isMobilePlatform)
        {
            moveInput = new Vector2(mobileInputX, 0f);
        }
    }

    private void FixedUpdate()
    {
        if (isKnockedBack) return;

        // Gabungkan input dari keyboard + tombol mobile
        float horizontal = moveInput.x + mobileInputX;
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        UpdateAnimation(horizontal);
    }

    private void UpdateAnimation(float horizontal)
    {
        MovementState state;

        if (horizontal > 0f)
        {
            state = MovementState.walk;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontal < 0f)
        {
            state = MovementState.walk;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
            state = MovementState.jump;
        else if (rb.velocity.y < -0.1f)
            state = MovementState.fall;

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // Tombol Mobile: Panggil dari UI Button
    public void MoveRight(bool isPressed)
    {
        mobileInputX = isPressed ? 1f : 0f;
    }

    public void MoveLeft(bool isPressed)
    {
        mobileInputX = isPressed ? -1f : 0f;
    }

    public void MobileJump()
    {
        if (isGrounded())
        {
            Jump();
        }
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        if (isKnockedBack) return;

        currentLives -= 1;
        UpdateHeartUI();

        if (currentLives <= 0)
        {
            currentLives = 0;
            Die();
        }
        else
        {
            StartCoroutine(HandleKnockback(direction.normalized));
        }
    }

    private void Die()
    {
        Debug.Log("Player Mati");
        rb.velocity = Vector2.zero;
        this.enabled = false;
        anim.SetTrigger("die");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    private void UpdateHeartUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentLives);
        }
    }

    private IEnumerator HandleKnockback(Vector2 direction)
    {
        isKnockedBack = true;
        rb.velocity = Vector2.zero;

        Vector2 force = direction * knockBackThrust * rb.mass;
        rb.AddForce(force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
}
