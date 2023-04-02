﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    public int damage = 10;
    public float knockbackForce = 5f;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private LayerMask groundLayer;

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
            StartCoroutine(other.GetComponent<PlayerMovement>().DisableMovement(0.3f));
        }
        else if (other.gameObject.layer == 9) 
        {
            Destroy(gameObject);
        }
    }

    // private IEnumerator DisableMovement(GameObject player)
    // {
    //     player.GetComponent<PlayerMovement>().enabled = false;
    //     yield return new WaitForSeconds(0.5f);
    //     player.GetComponent<PlayerMovement>().enabled = true;
    // }
}
