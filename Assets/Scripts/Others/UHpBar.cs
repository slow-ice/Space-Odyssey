using Assets.Scripts.Model.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UHpBar : MonoBehaviour
{
    //获取初始化信息
    public PlayerData_SO initData;

    public Image healthBarFill;
    
    float maxHP;

    float currentHP;

    void Start()
    {
        //此处获取最大生命
        maxHP = initData.health;
        currentHP = maxHP;
        
    }

    private void OnEnable() {
        currentHP = maxHP;
    }

    private void Update()
    {
        //此处获取当前生命
        currentHP = GetPlayerModel.Instance.hp;

        float healthPercentage = currentHP / maxHP;
       
        healthBarFill.fillAmount = healthPercentage;
    }

    
}
