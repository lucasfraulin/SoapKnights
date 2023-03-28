using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPile : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth = 10;

    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WaterParticle"))
        {
            currentHealth--;
            UpdateSprite();
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
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
