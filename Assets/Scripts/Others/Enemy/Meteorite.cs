using Assets.Scripts.Character.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ʯ�ű������ڱ��������

public class Meteorite : EnemyBase
{
    
    [SerializeField] float minHP; //��С����ֵ��С�ڴ���ʯ��ʧ

    float maxHP;
    Vector3 initScale; //�洢��ʼ��С

    #region �����߼�
    public override void damaged(int dmg)
    {
        //���غ���
    }

    public override void die()
    {
        base.die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // todo : ��ʱ
            GetPlayerModel.Instance.pm.ChangeEnergy(30);
            Debug.Log($"������ң���һ������");
            
        }
    }

    #endregion

    new void Start()
    {
        base.Start();
        initScale = transform.localScale;
        maxHP = enemyData.health;
    }

    
    void Update()
    {
        //��������������С
        transform.localScale = (currentHP / maxHP) * initScale;
    }

    
    
    
}