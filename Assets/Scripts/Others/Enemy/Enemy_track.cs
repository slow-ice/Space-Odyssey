using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 引用外部命名空间
using Assets.Scripts.Character;

public class Enemy_track : EnemyBase
{
    [SerializeField] float speed = 10;
    [SerializeField] float rotateSpeed = 5f;

    // TODO : 获取玩家的Transform
    Transform pTrasfrom;

    #region 基本逻辑
    public override void damaged(int dmg)
    {
        base.damaged(dmg);
    }

    public override void die()
    {
        Debug.Log("敌人死亡");
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {          
            GetPlayerModel.Instance.pm.ChangeHealth(-enemyData.damage);
            Debug.Log($"碰到玩家，玩家生命流失 {enemyData.damage}");
            die();
        }
    }
    #endregion 

    new void Start()
    {
        base.Start();
        pTrasfrom = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        //改变位置进行追踪
        if (pTrasfrom)
        {
            transform.up = Vector3.Slerp(transform.up, pTrasfrom.position - transform.position
                , rotateSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("找不到玩家，玩家死亡");
        }

        if (currentHP <= 0)
        {
            die();
        }

    }
}
