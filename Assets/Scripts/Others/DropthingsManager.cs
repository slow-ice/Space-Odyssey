using Assets.Scripts.Utility.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����������ĵ���
public class DropthingsManager :Singleton<DropthingsManager>
{
    //��ȡ��Դ�ص�����
    public ObjectPool dropthingsPool;
    
    //�������ɵ�����ĺ���
    public void createDropThings(Vector3 position,Quaternion rotation)
    {
        dropthingsPool.Spawn(position, rotation, null);
    }
}
