using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 引用外部命名空间
using Assets.Scripts.Character;

public class Enemy_track : MonoBehaviour
{
    [SerializeField] float speed = 10;

    // TODO : 获取玩家的Transform
    Transform pTrasfrom;

    void Start()
    {
        pTrasfrom = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        //改变位置进行追踪
        if (pTrasfrom)
        {
            transform.up = pTrasfrom.position - transform.position;
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("找不到玩家，玩家死亡");
        }
        
    }
}
