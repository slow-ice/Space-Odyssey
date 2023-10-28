using Assets.Scripts.Model.Enemy;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    public EnemyData_SO enemyData;

    protected int currentHP;

    //�����ʼ������
    protected void Start()
    {
        currentHP = enemyData.health;
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
    /// ������ִ������߼�
    /// </summary>
    public virtual void die() { }

    
    
}
