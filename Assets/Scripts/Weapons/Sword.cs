using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private int damageToDirt = 2;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask dirtLayer;

    public float attackOffset = 0.5f;

    private AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(1)) && !PlayerMovement.isAttacking)
        {
            swordAttack();
            StartCoroutine(Attack());
        }
    }

    private void swordAttack() 
    {
        animator.SetTrigger("SwordAttack");
        audioSource.PlayOneShot(attackSound);
    }

    public IEnumerator Attack()
    {
        PlayerMovement.setAttacking(true); 
        Vector2 attackDirection = PlayerMovement.isFacingRight ? Vector2.right : Vector2.left;
        Vector2 attackPos = (Vector2)transform.position + attackDirection * attackOffset; 

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRange, enemyLayer);
        Collider2D[] hitDirt = Physics2D.OverlapCircleAll(attackPos, attackRange, dirtLayer);

        yield return new WaitForSeconds(0.1f);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy != null)
            {
                Enemy enemyHealth = enemy.GetComponent<Enemy>();
                if (enemyHealth != null){
                    enemyHealth.TakeDamage(attackDamage);
                }
                else {
                    BossController boss = enemy.GetComponent<BossController>();
                    if (boss != null) 
                    {
                        boss.TakeDamage(attackDamage);
                    }
                }
            }
        }

        foreach (Collider2D dirt in hitDirt)
        {
            if (dirt != null)
                dirt.GetComponent<DirtPile>().TakeDamage(damageToDirt); 
        }

        yield return new WaitForSeconds(0.2f);

        PlayerMovement.setAttacking(false); 
    }

    
}
