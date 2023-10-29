using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//挂载碰撞函数的脚本

public class HitPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // todo : 改变玩家生命值,现在还不是动态的
            GetPlayerModel.Instance.pm.ChangeHealth(-5);
            
        }
    }
}
