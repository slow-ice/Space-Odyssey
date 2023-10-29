using Assets.Scripts.Character.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 陨石脚本，用于被玩家吸收

public class Meteorite : EnemyBase , IAbsorb
{
    
    [SerializeField] float minHP; //最小生命值，小于此陨石消失

    float maxHP;
    Vector3 initScale; //存储初始大小

    #region 基本逻辑
    public override void damaged(int dmg)
    {
        //隐藏函数 因为陨石不受子弹伤害
    }

    public override void die()
    {

    }

    public int GetEnergy()
    {
        throw new System.NotImplementedException();
    }

    public void OnAbsorbAction(Transform playerTrans)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // todo : 临时
            GetPlayerModel.Instance.pm.ChangeEnergy(30);
            Debug.Log($"碰到玩家，玩家获得能量");
            
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
        //根据生命决定大小
        transform.localScale = (currentHP / maxHP) * initScale;
    }

    
    
    
}
