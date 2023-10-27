using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UHpBar : MonoBehaviour
{
    public Image healthBarFill;
    public float maxHP = 100f;

    public float currentHP;

    void Start()
    {  
        currentHP = maxHP;
    }

    private void Update()
    {
        float healthPercentage = currentHP / maxHP;
        Debug.Log("DD");
        healthBarFill.fillAmount = healthPercentage;
    }

    
}
