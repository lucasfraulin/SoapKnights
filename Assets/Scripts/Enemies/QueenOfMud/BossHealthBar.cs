using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public BossController boss;
    public Image fillImage;

    private void Update()
    {
        float fillAmount = (float) boss.currentHealth / (float) boss.maxHealth;
        fillImage.fillAmount = fillAmount;
    }
}