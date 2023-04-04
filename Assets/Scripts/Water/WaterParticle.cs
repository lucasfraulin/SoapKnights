using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int waterDamage = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9) //ground layer
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy")) 
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
            }
            else {
                BossController boss = other.gameObject.GetComponent<BossController>();
                if (boss != null) 
                {
                    boss.TakeDamage(20);
                }
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Dirt")) 
        {
            other.gameObject.GetComponent<DirtPile>().TakeDamage(waterDamage);
            Destroy(gameObject);
        }
    }
}
