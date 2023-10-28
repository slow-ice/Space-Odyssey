using Assets.Scripts.Utility.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//管理生成物的掉落
public class DropthingsManager :Singleton<DropthingsManager>
{
    //获取资源池的引用
    public ObjectPool dropthingsPool;
    
    //调用生成凋落物的函数
    public void createDropThings(Vector3 position,Quaternion rotation)
    {
        dropthingsPool.Spawn(position, rotation, null);
    }
}
