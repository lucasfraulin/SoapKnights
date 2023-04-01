using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Boss health
    public int maxHealth = 100;
    private int currentHealth;

    // Boss stages
    public int currentStage = 1;
    public GameObject stage1;
    public GameObject stage2;

    // Boss movement
    public float moveSpeed = 2.0f;
    public float rangeToPlayer = 5.0f;
    public float attackRange = 1.5f;
    public int attackDamage = 20;
    public float attackDelay = 1.0f;
    public float timeSinceAttack = 1.0f;
    private Transform playerTransform;

    public GameObject AttackParticle;

    // Animator controllers
    public RuntimeAnimatorController stage1AnimatorController;
    public RuntimeAnimatorController stage2AnimatorController;
    private Animator animator;
    
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        setStage1();
    }

    // Update is called once per frame
    void Update()
    {
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
        }
        else if (distanceToPlayer > attackRange && distanceToPlayer <= rangeToPlayer) 
        {
            // Calculate the direction to move towards the player, but only horizontally
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            // Move the boss horizontally towards the player
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Flip the sprite horizontally depending on movement direction
            if (direction.x > 0)
            {
                spriteRenderer.flipX = true; // sprite defaults to face left
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        timeSinceAttack += Time.deltaTime;
    }

    // OnTriggerEnter2D is called when the boss is hit by the player's attack
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            currentHealth -= 10;

            // Check if the boss should change to the next stage
            if (currentHealth <= 0 && currentStage == 1)
            {
                setStage2();
            }
            else if (currentHealth <= 0 && currentStage == 2)
            {
                // Boss defeated
                Destroy(gameObject);
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
        rangeToPlayer = 5.0f;
        attackRange = 4f;
        attackDamage = 30;
        attackDelay = 2.0f;
    }

    private void setStage2()
    {
        currentStage = 2;
        stage1.SetActive(false);
        stage2.SetActive(true);
        currentHealth = maxHealth;

        animator.runtimeAnimatorController = stage2AnimatorController;

        moveSpeed *= 2f;
        attackDamage = 25;
        attackDelay = 0.5f;
        attackRange = 1f;
    }

    private void Attack() 
    {
        animator.SetTrigger("Attack");
        timeSinceAttack = 0f;

        if (currentStage == 1) 
        {
            Vector3 particleDirection = playerTransform.position - transform.position;
            particleDirection.Normalize();
            GameObject particle = Instantiate(AttackParticle, transform.position, Quaternion.identity);
            particle.GetComponent<Rigidbody2D>().velocity = particleDirection * 5.0f;
        }
        else if (currentStage == 2) 
        {
            Debug.Log("Attack stage 2");
        }
    }
}
