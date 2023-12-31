using Assets.Scripts.Utility.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //对象池
    public ObjectPool pool;
    //生成时间
    public int minTime, maxTime;
    //生成的随机半径
    public float r;

    //计数器
    float time;
    //下次生成敌人的时间间隔
    float nextSpawnTime;
    //省时间
    Vector3 posCache;

    protected virtual void Start()
    {
        if (pool == null)
            Debug.LogError("the pool of this spawner is NULL!");

        time = 0;
        nextSpawnTime = Random.Range(minTime, maxTime);
    }

    protected virtual void Update()
    {
        time += Time.deltaTime;

        if (time >= nextSpawnTime)
        {
            time = 0;
            nextSpawnTime = Random.Range(minTime, maxTime);
            SpawnEnemy();
        }
    }

    protected virtual void SpawnEnemy()
    {
        posCache = GenerateRandomVec(r) + transform.position;

        pool.Spawn(posCache, transform.rotation, null);
    }


    //随机生成长为r的向量，且z = 0
    private Vector3 GenerateRandomVec(float r)
    {
        // 随机生成一个角度（0 到 360 度之间）
        float randomAngle = Random.Range(0f, 360f);

        // 将角度转化为弧度
        float radianAngle = randomAngle * Mathf.Deg2Rad;

        // 使用三角函数计算二维单位向量的 x 和 y 分量
        float x = Mathf.Cos(radianAngle);
        float y = Mathf.Sin(radianAngle);

        return new Vector3(x, y, 0) * r;
    }

    private void OnDrawGizmos()
    {
        // 设置Gizmos的颜色
        //Gizmos.color = Color.blue;

      
        // 可视化对象生成半径
        Gizmos.DrawWireSphere(transform.position, r);
    }
}
