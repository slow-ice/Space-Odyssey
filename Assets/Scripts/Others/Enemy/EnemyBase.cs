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
    //敌人对象池
    public ObjectPool pool;

    protected float currentHP;

    //父类初始化生命
    protected void Start()
    {
        //自动从父物体中获取对象池脚本
        pool = transform.parent.GetComponent<ObjectPool>();
        
        currentHP = enemyData.health;
    }

    private void OnEnable() {
        currentHP = enemyData.health;
    }

    //注意在对象池中,死亡指对象被禁用,其死亡代码仍能执行
    public void Update()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
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
    /// 死亡并执行相关逻辑,注意在对象池中,死亡指对象被禁用,其死亡代码仍能执行
    /// </summary>
    public virtual void die() 
    { 
         
    }

    
    
}
