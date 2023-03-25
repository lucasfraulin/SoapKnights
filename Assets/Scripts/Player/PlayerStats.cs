using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioClip takeDamageClip;

    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = takeDamageClip;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        audioSource.Play();
        animator.SetTrigger("TakeDamage");
        StartCoroutine(FlashScreenRed());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
    }

    private IEnumerator FlashScreenRed()
    {
        Image redOverlay = GameObject.Find("RedOverlay").GetComponent<Image>();

        redOverlay.color = new Color(1f, 0f, 0f, 0.5f);

        yield return new WaitForSeconds(0.1f);

        for (float t = 0.5f; t >= 0; t -= Time.deltaTime)
        {
            redOverlay.color = new Color(1f, 0f, 0f, t);
            yield return null;
        }
    }
}
