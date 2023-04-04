using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Animator animator;

    public AudioClip attackSound;
    public GameObject particlePrefab;
    public float particleSpeed = 5f;

    private Vector2 direction;
    private AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = attackSound;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PlayerMovement.isAttacking)
        {
            gunAttack();
            SpawnParticle();
        }
    }

    private void gunAttack() 
    {
        animator.SetTrigger("Shoot");
        audioSource.Play();
    }

    public void SpawnParticle()
    {
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        PlayerMovement.setAttacking(true); 

        yield return new WaitForSeconds(0.3f);

        GameObject particle;

        if (PlayerMovement.isFacingRight == true)
        {
            Vector2 spawnPos = new Vector2(rightSpawnPoint.position.x, rightSpawnPoint.position.y);
            particle = Instantiate(particlePrefab, spawnPos, Quaternion.identity);
            direction = transform.right;
        }
        else
        {
            Vector2 spawnPos = new Vector2(leftSpawnPoint.position.x, leftSpawnPoint.position.y);
            particle = Instantiate(particlePrefab, spawnPos, Quaternion.identity);
            direction = transform.right * -1;
        }

        Rigidbody2D particleRB = particle.GetComponent<Rigidbody2D>();
        if (particleRB != null)
            particleRB.velocity = direction * particleSpeed;

        yield return new WaitForSeconds(0.3f);

        PlayerMovement.setAttacking(false); 
    }
}
