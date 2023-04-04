using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private AudioSource audioSource;
    [SerializeField] private AudioClip attack1Sound;
    [SerializeField] private AudioClip attack2Sound;
    [SerializeField] private AudioClip takeDamageSound;
    [SerializeField] private AudioClip transformSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip entranceSound;
    [SerializeField] private AudioClip bossStartSound;
    [SerializeField] private AudioClip cackleSound;
    [SerializeField] private AudioClip stage2Music;
    [SerializeField] private BackgroundMusic backgroundMusic;

    private bool isAttacking = false;

    private GameObject player;

    public int currentStage = 1;
    public GameObject stage1;
    public GameObject stage2;
    public float stage2Scale = 1.5f;

    public float moveSpeed = 2.0f;
    public float rangeToPlayer = 5.0f;
    public float attackRange = 1.5f;
    public int attackDamage = 20;
    public float attackDelay = 1.0f;
    public float attackDuration = 1f;
    public float timeSinceAttack = 1.0f;
    private float timeSinceAttackStarted = 0.0f;
    private Transform playerTransform;
    private bool canMove;
    private bool canAttack;
    public LayerMask groundLayer;

    public int playerAttackDamage = 20;

    public GameObject AttackParticle;

    public RuntimeAnimatorController stage1AnimatorController;
    public RuntimeAnimatorController stage2AnimatorController;
    private Animator animator;
    
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    public Color stage2Color;
    private bool bossEntered = false;

    private bool bossTransitionActive = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        canMove = true;
        canAttack = false;
        bossEntered = false;
    }

    void Update()
    {
        if (bossTransitionActive)
            return;

        if (!bossEntered) 
        {
            StartCoroutine(EnterBoss());
            return;
        }
            

        // Prevent movement during the attack animation
        if (timeSinceAttackStarted >= attackDuration)
        {
            canMove = true;
            timeSinceAttackStarted = 0.0f;
        }

        // Move towards the player if within range
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (!isAttacking && distanceToPlayer <= attackRange && timeSinceAttack >= attackDelay)
        {
            if (playerTransform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            animator.SetBool("Moving", false);
            
            if (canAttack)
                StartCoroutine(Attack());
            
        }
        else if (distanceToPlayer > attackRange && distanceToPlayer <= rangeToPlayer && canMove) 
        {
            if (currentStage == 2)
            {
                // Check for line of sight to the player in the second stage before moving
                Vector2 viewDirection = playerTransform.position - transform.position;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, viewDirection, viewDirection.magnitude, groundLayer);
                if (hit.collider != null)
                {
                    // There is ground in the way, don't move
                    animator.SetBool("Moving", false);
                    timeSinceAttack += Time.deltaTime;
                    timeSinceAttackStarted += Time.deltaTime;
                    return;
                }
            }

            // Calculate the direction to move towards the player, but only horizontally
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            // Move the boss horizontally towards the player
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Flip the sprite horizontally depending on movement direction
            if (playerTransform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = false; // sprite defaults to face left
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        timeSinceAttack += Time.deltaTime;
        timeSinceAttackStarted += Time.deltaTime;
    }

    private void setStage1()
    {
        currentStage = 1;
        stage1.SetActive(true);
        stage2.SetActive(false);
        currentHealth = maxHealth;

        animator.runtimeAnimatorController = stage1AnimatorController;

        moveSpeed = 1.0f;
        rangeToPlayer = 6.0f;
        attackRange = 4f;
        attackDamage = 30;
        attackDelay = 1f;

        canAttack = true;
    }

    private void setStage2()
    {
        currentStage = 2;
        stage1.SetActive(false);
        stage2.SetActive(true);
        currentHealth = maxHealth;

        backgroundMusic.setBackgroundMusic(stage2Music);
        animator.runtimeAnimatorController = stage2AnimatorController;
        spriteRenderer.color = stage2Color;

        moveSpeed = 2.0f;
        attackDamage = 25;
        attackDelay = 1f;
        attackDuration = 1f;
        attackRange = 1f;

        canAttack = true;
    }

    private IEnumerator Attack() 
    {
        isAttacking = true;
        canAttack = false;
        canMove = false;
        audioSource.PlayOneShot(currentStage == 1 ? attack1Sound : attack2Sound);
        animator.SetTrigger("Attack");

        timeSinceAttack = 0f;
        timeSinceAttackStarted = 0.0f;

        yield return new WaitForSeconds(0.1f);

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (currentStage == 1) 
        {
            Vector3 particleDirection = playerTransform.position - transform.position;
            particleDirection.Normalize();
            GameObject particle = Instantiate(AttackParticle, transform.position, Quaternion.identity);
            particle.GetComponent<Rigidbody2D>().velocity = particleDirection * 3.0f;
        }
        else if (currentStage == 2) 
        {
            yield return new WaitForSeconds(0.1f);
            if (distanceToPlayer <= attackRange)
            {
                player.GetComponent<PlayerStats>().TakeDamage(attackDamage);
                Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
                Vector2 direction = playerRB.position - (Vector2)transform.position;
                direction = direction.normalized;
                playerRB.velocity = Vector2.zero;
                playerRB.AddForce(direction * 5f, ForceMode2D.Impulse);
                StartCoroutine(player.GetComponent<PlayerMovement>().DisableMovement(0.25f));
            }
        }

        yield return new WaitForSeconds(attackDelay - timeSinceAttackStarted);
        isAttacking = false;
        canAttack = true;
        canMove = true;
    }

    public void TakeDamage(int damageAmount)
    {
        if (bossTransitionActive)
            return;

        Debug.Log(damageAmount);

        audioSource.PlayOneShot(takeDamageSound);
        animator.SetTrigger("Hurt");
        currentHealth -= damageAmount;
        StartCoroutine(FlashRed());

        if (currentHealth <= 0 && currentStage == 1)
        {
            StartCoroutine(TransformBoss());
        }
        else if (currentHealth <= 0 && currentStage == 2)
        {
            StartCoroutine(BossDefeated());
        }
    }

    public void Transform() 
    {
        StartCoroutine(TransformBoss());
    }

    public void Die()
    {
        canMove = false;

        animator.SetTrigger("Die");

        GetComponent<Collider2D>().enabled = false;
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        if (currentStage == 2)
        {
            spriteRenderer.color = stage2Color;
        }
        else
        {
            spriteRenderer.color = defaultColor;
        }
        
    }

    private IEnumerator EnterBoss() 
    {
        bossEntered = true;
        audioSource.PlayOneShot(entranceSound);
        bossTransitionActive = true;
        yield return new WaitForSeconds(2.0f);
        bossTransitionActive = false;
        setStage1();
        audioSource.PlayOneShot(bossStartSound);
    }

    private IEnumerator TransformBoss()
    {
        animator.SetTrigger("Die");
        audioSource.PlayOneShot(transformSound);
        bossTransitionActive = true;
        yield return new WaitForSeconds(4.0f);
        bossTransitionActive = false;
        setStage2();
        audioSource.PlayOneShot(bossStartSound);
    }

    private IEnumerator BossDefeated()
    {
        Die();
        audioSource.PlayOneShot(dieSound);
        bossTransitionActive = true;
        yield return new WaitForSeconds(3.0f);
        bossTransitionActive = false;
        Destroy(gameObject);
    }
}
