using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public GameObject particlePrefab;
    public float particleSpeed = 3f;
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
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            SpawnParticle();
        }
    }

    private void Attack() 
    {
        animator.SetTrigger("Attack");
        audioSource.Play();
    }

    public void SpawnParticle()
    {
        if (PlayerMovement.isFacingRight == true)
        {
            foreach (Transform spawnPoint in rightSpawnPoints) {
                Vector2 spawnPos = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
                GameObject particle = Instantiate(particlePrefab, spawnPos, Quaternion.identity);

                direction = transform.right;

                Rigidbody2D particleRB = particle.GetComponent<Rigidbody2D>();
                if (particleRB != null)
                {
                    particleRB.velocity = direction * particleSpeed;
                }
            }
        }
        else
        {
            foreach (Transform spawnPoint in leftSpawnPoints) {
                Vector2 spawnPos = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
                GameObject particle = Instantiate(particlePrefab, spawnPos, Quaternion.identity);

                direction = transform.right * -1;

                Rigidbody2D particleRB = particle.GetComponent<Rigidbody2D>();
                if (particleRB != null)
                {
                    particleRB.velocity = direction * particleSpeed;
                }
            }
        }

    }
}
