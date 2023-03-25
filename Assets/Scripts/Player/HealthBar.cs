using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerStats playerStats;
    public Image fillImage;

    private void Update()
    {
        float fillAmount = (float) playerStats.currentHealth / (float) playerStats.maxHealth;
        fillImage.fillAmount = fillAmount;
    }
}
