using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Boss health
    public int maxHealth = 100;
    public int currentHealth;

    private GameObject player;

    // Boss stages
    public int currentStage = 1;
    public GameObject stage1;
    public GameObject stage2;
    public float stage2Scale = 1.5f;

    // Boss movement
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
    public LayerMask groundLayer;

    public int playerAttackDamage = 20;

    public GameObject AttackParticle;

    // Animator controllers
    public RuntimeAnimatorController stage1AnimatorController;
    public RuntimeAnimatorController stage2AnimatorController;
    private Animator animator;
    
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    public Color stage2Color;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        canMove = true;

        setStage1();
    }

    // Update is called once per frame
    void Update()
    {

        // Prevent movement during the attack animation
        if (timeSinceAttackStarted >= attackDuration)
        {
            canMove = true;
            timeSinceAttackStarted = 0.0f;
        }

        // Move towards the player if within range
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange && timeSinceAttack >= attackDelay)
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
            
            Attack();

            if (currentStage == 1) 
            {
                Vector3 particleDirection = playerTransform.position - transform.position;
                particleDirection.Normalize();
                GameObject particle = Instantiate(AttackParticle, transform.position, Quaternion.identity);
                particle.GetComponent<Rigidbody2D>().velocity = particleDirection * 3.0f;
            }
            else if (currentStage == 2) 
            {
                // Damage the player if within range
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

    // OnTriggerEnter2D is called when the boss is hit by the player's attack
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WaterParticle"))
        {
            TakeDamage(playerAttackDamage);

            // Check if the boss should change to the next stage
            if (currentHealth <= 0 && currentStage == 1)
            {
                setStage2();
            }
            else if (currentHealth <= 0 && currentStage == 2)
            {
                // Boss defeated
                Die();
            }
        }
    }

    private void setStage1()
    {
        currentStage = 1;
        stage1.SetActive(true);
        stage2.SetActive(false);
        currentHealth = maxHealth;

        animator.runtimeAnimatorController = stage1AnimatorController;

        moveSpeed = 2.0f;
        rangeToPlayer = 6.0f;
        attackRange = 4f;
        attackDamage = 30;
        attackDelay = 1f;
    }

    private void setStage2()
    {
        currentStage = 2;
        stage1.SetActive(false);
        stage2.SetActive(true);
        currentHealth = maxHealth;

        animator.runtimeAnimatorController = stage2AnimatorController;
        spriteRenderer.color = stage2Color;

        moveSpeed = 3f;
        attackDamage = 25;
        attackDelay = 0.5f;
        attackDuration = 1f;
        attackRange = 1f;
    }

    private void Attack() 
    {
        canMove = false;
        animator.SetTrigger("Attack");
        timeSinceAttack = 0f;
        timeSinceAttackStarted = 0.0f;
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        StartCoroutine(FlashRed());
    }

    public void Die()
    {
        canMove = false;

        animator.SetTrigger("Die");

        // Disable collider
        GetComponent<Collider2D>().enabled = false;

        // Destroy game object after death animation is finished
        float deathAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, deathAnimationLength);
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
}
