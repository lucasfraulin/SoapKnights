using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    public static bool movementDisabled = false;

    public AudioClip jumpSound;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform groundCheck;
    private Animator animator;
    private AudioSource audioSource;

    static public bool isFacingRight = true;

    private bool isGrounded = false;

    public static bool isAttacking = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        movementDisabled = false;
    }

    private void Update()
    {
        if (movementDisabled) 
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            audioSource.PlayOneShot(jumpSound);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }


        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0) 
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void FixedUpdate()
    {
        if (movementDisabled)
            return;

        float moveDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if (moveDirection < 0)
        {
            spriteRenderer.flipX = true;
            isFacingRight = false;
        }
        else if (moveDirection > 0)
        {
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }
    }

    public IEnumerator DisableMovement(float time)
    {
        movementDisabled = true;
        yield return new WaitForSeconds(time);
        movementDisabled = false;
    }

    public static void setAttacking(bool attacking) {
        isAttacking = attacking;

    }
}
