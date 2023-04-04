using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class swordDamage : MonoBehaviour
{
    private int damage = 10;
    public void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().TakeDamage(damage);  
            //currentHealth.TakeDamage(damage);
        }
    }
}
