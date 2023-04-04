using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public AudioClip attackSound;
    
    [SerializeField] private Animator animator;

    public Transform SwordRange;

     [SerializeField] public float swordReach = 0.5f;

    private Vector2 direction;
    private AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = attackSound;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !PlayerMovement.isShooting)
        {
            swordAttack();
            
        }
    }

    private void swordAttack() 
    {
        animator.SetTrigger("SwordAttack");
        audioSource.Play();
    }

    
}
