using Assets.Scripts.Model.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UHpBar : MonoBehaviour
{
    //��ȡ��ʼ����Ϣ
    public PlayerData_SO initData;

    public Image healthBarFill;
    
    float maxHP;

    float currentHP;

    void Start()
    {
        //�˴���ȡ�������
        maxHP = initData.health;
        currentHP = maxHP;
        
    }

    private void OnEnable() {
        currentHP = maxHP;
    }

    private void Update()
    {
        //�˴���ȡ��ǰ����
        currentHP = GetPlayerModel.Instance.hp;

        float healthPercentage = currentHP / maxHP;
       
        healthBarFill.fillAmount = healthPercentage;
    }

    
}
