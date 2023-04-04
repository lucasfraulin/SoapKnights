using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;
    public int sword_and_waterDamage = 10;
    public float knockbackForce = 5f;
    public float flashDuration = 0.1f;
    public bool moving;
    public bool isRanged;
    public GameObject bullet; //bullet prefab
    public float fireRate = 5000f; //Fire every 3 seconds
    public float shootingPower = 5.0f; //projectile force
    public int enemyNum;
    public AudioClip takeDamageClip;
    public AudioClip dieClip;

    private float[] maxDistance = new float[3] {0f,-4.90f, 3.85f};
    private float[] minDistance = new float[3] {0f,-8.50f, 0.75f};
    private float velocity = 0.01f;
    private int direction = 1;
    private float shootingTime = 0.0f; //to ensure enemy shoots every 5 seconds
    private GameObject playerObj;
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
        playerObj = GameObject.Find("Player");
    }

    private void Update()
    {
        // ranged enemy will constantly fire
        if (isRanged == true)
        {
            Fire();
        }
        
    }
    private void FixedUpdate()
    {
        // for moving
        if (moving == true)
        {
            var currentPosition = transform.position;
            
            switch (direction)
            {
                case -1:
                    // Moving Left
                    if (currentPosition.x > minDistance[enemyNum])
                    {
                        currentPosition.x -= velocity;
                        transform.position = currentPosition;                  
                    }
                    else
                    {
                        // switch direction when reaching boundary
                        currentPosition.x = minDistance[enemyNum];
                        transform.position = currentPosition; 
                        direction = 1;
                    }
                    break;
                case 1:
                    // Moving Right
                    if (currentPosition.x < maxDistance[enemyNum])
                    {
                        currentPosition.x += velocity;
                        transform.position = currentPosition;
                    }
                    else{
                        // switch direction when reaching boundary
                        currentPosition.x = maxDistance[enemyNum];
                        transform.position = currentPosition;
                        direction = -1;
                    }
                    break;
            }
        }           
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
            StartCoroutine(other.GetComponent<PlayerMovement>().DisableMovement(0.5f));
        }
        else if (other.CompareTag("WaterParticle") || other.CompareTag("Sword"))
        {
            audioSource.PlayOneShot(takeDamageClip);
            TakeDamage(sword_and_waterDamage);
        }
    }


    public void TakeDamage(int damageAmount)
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

    private void Fire()
    {
        if (Time.time > shootingTime)
        {
            shootingTime = Time.time + fireRate / 1000; //set shooting time to current time of shooting
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            GameObject projectile = Instantiate(bullet, myPos, Quaternion.identity); //create our bullet
            Vector2 playerPos = new Vector2(playerObj.transform.position.x, playerObj.transform.position.y);
            Vector2 direction = (playerPos - myPos).normalized; //get the direction to the target, normalized for constant velocity
            projectile.GetComponent<Rigidbody2D>().velocity = direction * shootingPower; //shoot the bullet
            Object.Destroy(projectile, 2.0f);
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
