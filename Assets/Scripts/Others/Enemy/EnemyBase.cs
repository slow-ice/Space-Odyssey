using Assets.Scripts.Model.Enemy;
using Assets.Scripts.Utility.Pool;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    public EnemyData_SO enemyData;
    //���˶����
    public ObjectPool pool;

    protected float currentHP;

    //�����ʼ������
    protected void Start()
    {
        //�Զ��Ӹ������л�ȡ����ؽű�
        pool = transform.parent.GetComponent<ObjectPool>();
        
        currentHP = enemyData.health;
    }

    private void OnEnable() {
        currentHP = enemyData.health;
    }

    //ע���ڶ������,����ָ���󱻽���,��������������ִ��
    public void Update()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
    }

    /// <summary>
    /// ����ҽű����ã���������ֵ
    /// </summary>
    /// <param name="dmg"></param>
    public virtual void damaged(int dmg)
    {
        currentHP -= dmg;
    }

    /// <summary>
    /// ������ִ������߼�,ע���ڶ������,����ָ���󱻽���,��������������ִ��
    /// </summary>
    public virtual void die() 
    { 
         
    }

    
    
}
