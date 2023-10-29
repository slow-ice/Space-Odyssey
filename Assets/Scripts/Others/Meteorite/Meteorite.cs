using Assets.Scripts.Character.Resource;
using Assets.Scripts.Utility.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 陨石脚本，用于被玩家吸收

public class Meteorite : EnemyBase , IAbsorb
{
    [SerializeField] float decreaseSpeed; //被吸收的速度
    [SerializeField] float minHP; //最小生命值，小于此陨石消失
    [SerializeField] float minSpawnHP, maxSpawnHP;
    [SerializeField] float speed = 10;
    [SerializeField] float turnSpeed = 5f;
    

    float maxHP;
    Vector3 initScale; //存储初始大小

    #region 基本逻辑
    public override void damaged(int dmg)
    {
        //隐藏函数 因为陨石不受子弹伤害
    }

    public override void die()
    {
        int num = enemyData.resourceNum;


        for (int i = 0; i < num; i++)
        {
            //随机扩散凋落物
            Vector3 pos = Utils.GenerateRandomVec(0.5f);
            DropthingsManager.Instance.createDropThings(transform.position + pos, transform.rotation);
        }

        pool.Recycle(gameObject, null);
    }

    public int GetEnergy()
    {
        return 0;
    }

    public void OnAbsorbAction(Transform playerTrans)
    {
        if(currentHP >= minHP)
            currentHP -= (Time.fixedDeltaTime * decreaseSpeed);
    }

    #endregion

    new void Start()
    {
        //自动从父物体中获取对象池脚本
        pool = transform.parent.GetComponent<ObjectPool>();
    }

    //被对象池调用时
    public void OnEnable()
    {
        //生成
        maxHP = Random.Range(minSpawnHP,maxSpawnHP);
        
        currentHP = (int)maxHP;

        initScale = (maxHP / 50.0f) * Vector3.one;
        
        transform.localScale = initScale;
       
    }

    new void Update()
    {
        //判断是否被激活,否则不执行
        //base.Update();

        //根据生命决定大小
        transform.localScale = (currentHP / maxHP) * initScale;

        //修改绝对位置
        transform.Translate(speed * Time.deltaTime, 0, 0,Space.World);
        transform.Rotate(0,0,10f * Time.deltaTime);

        if(currentHP < minHP)
        {
            die();
        }

        
    }

    
    
    
}
