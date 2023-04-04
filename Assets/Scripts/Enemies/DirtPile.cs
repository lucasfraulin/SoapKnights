using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPile : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth = 5;

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth--;
        UpdateSprite();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateSprite()
    {
        // Calculate the sprite index based on the current health and the number of available sprites
        int spriteIndex = Mathf.Clamp((int)(((float)currentHealth / (float)maxHealth) * sprites.Length), 0, sprites.Length - 1);

        // Set the sprite to the appropriate index
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
