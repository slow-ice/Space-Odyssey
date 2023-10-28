using Assets.Scripts.Model.Enemy;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    public EnemyData_SO enemyData;

    protected int currentHP;

    //父类初始化生命
    protected void Start()
    {
        currentHP = enemyData.health;
    }


    /// <summary>
    /// 由玩家脚本调用，减少生命值
    /// </summary>
    /// <param name="dmg"></param>
    public virtual void damaged(int dmg)
    {
        currentHP -= dmg;
    }

    /// <summary>
    /// 死亡并执行相关逻辑
    /// </summary>
    public virtual void die() { }

    
    
}
