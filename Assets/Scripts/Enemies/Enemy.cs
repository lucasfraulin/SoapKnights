using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;
    public int waterDamage = 10;
    public float knockbackForce = 5f;
    public float flashDuration = 0.1f;
    public AudioClip takeDamageClip;
    public AudioClip dieClip;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            Rigidbody2D playerRB = other.GetComponent<Rigidbody2D>();
            Vector2 direction = playerRB.position - (Vector2)transform.position;
            direction = direction.normalized;
            playerRB.velocity = Vector2.zero;
            playerRB.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(DisableMovement(other.gameObject));
        }
        else if (other.CompareTag("WaterParticle"))
        {
            audioSource.PlayOneShot(takeDamageClip);
            TakeDamage(waterDamage);
        }
    }

    private IEnumerator DisableMovement(GameObject player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            audioSource.PlayOneShot(dieClip);
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = defaultColor;
    }
}
