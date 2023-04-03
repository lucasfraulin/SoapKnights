using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform[] rightSpawnPoints;
    public Transform[] leftSpawnPoints;

    public AudioClip attackSound;
    
    [SerializeField] private Animator animator;

    private Vector2 direction;
    private AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = attackSound;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
            
        }
    }

    private void Attack() 
    {
        animator.SetTrigger("Attack");
        audioSource.Play();
    }

    
}
