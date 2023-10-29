using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 引用外部命名空间
using Assets.Scripts.Character;

public class Enemy_track : EnemyBase
{
    [SerializeField] float rotateSpeed = 5f;

    [SerializeField] float speed = 10;

    bool isBeDestroyedByPlayer; //判断是否被玩家击杀

    // TODO : 获取玩家的Transform
    Transform pTrasfrom;

    #region 基本逻辑
    public override void damaged(int dmg)
    {
        base.damaged(dmg);
    }

    public override void die()
    {
        //只有被玩家击杀后才有50%概率爆金币
        if (isBeDestroyedByPlayer && Random.Range(0f,1f) < 0.5f)
        {
            DropthingsManager.Instance.createDropThings(transform.position, transform.rotation);
        }
        pool.Recycle(gameObject,null);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out var player))
        {
            var core = player.mCore;
            if (core.canAbsorb) {
                GetPlayerModel.Instance.pm.ChangeHealth(-enemyData.damage);
            }
            else {
                if (GetPlayerModel.Instance.pm.Energy.Value > 0) {
                    GetPlayerModel.Instance.pm.ChangeEnergy(-enemyData.damage * 2);
                }
                else {
                    GetPlayerModel.Instance.pm.ChangeHealth(-enemyData.damage);
                }
            }
            isBeDestroyedByPlayer = false;
            die();
        }
    }
    #endregion 

    new void Start()
    {
        base.Start();
        pTrasfrom = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // 由对象池重新生成时执行
    private void OnEnable()
    {
        //重新生成时回满血
        currentHP = enemyData.health;
    }

    new void Update()
    {
        base.Update();

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
            isBeDestroyedByPlayer = true;
            die();
        }

    }
}
