using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 陨石脚本，用于被玩家吸收

public class Meteorite : MonoBehaviour
{
    [SerializeField]  float HP; //根据生命决定大小
    [SerializeField] float minHP; //最小生命值，小于此陨石消失
    float current_HP;

    Vector3 initScale; //存储初始大小
 
    void Start()
    {
        current_HP = HP;
        initScale = transform.localScale;
    }

    
    void Update()
    {
        //根据生命决定大小
        transform.localScale = (current_HP / HP) * initScale;
    }

    void DestoryMe()
    {

    }
    
    /// <summary>
    /// 由玩家脚本调用，减少生命值
    /// </summary>
    /// <param name="dmg"></param>
    public void damaged(float dmg)
    {
        current_HP -= dmg;
        if(current_HP < minHP)
        {
            DestoryMe();
        }
    }
}
