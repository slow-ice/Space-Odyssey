using Assets.Scripts.Utility.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //�����
    public ObjectPool pool;
    //����ʱ��
    public int minTime, maxTime;
    //���ɵ�����뾶
    public float r;

    //������
    float time;
    //�´����ɵ��˵�ʱ����
    float nextSpawnTime;
    //ʡʱ��
    Vector3 posCache;

    private void Start()
    {
        time = 0;
        nextSpawnTime = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time >= nextSpawnTime)
        {
            time = 0;
            nextSpawnTime = Random.Range(minTime, maxTime);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        posCache = GenerateRandomVec(r) + transform.position;

        pool.Spawn(posCache, transform.rotation, null);
    }


    //������ɳ�Ϊr����������z = 0
    private Vector3 GenerateRandomVec(float r)
    {
        // �������һ���Ƕȣ�0 �� 360 ��֮�䣩
        float randomAngle = Random.Range(0f, 360f);

        // ���Ƕ�ת��Ϊ����
        float radianAngle = randomAngle * Mathf.Deg2Rad;

        // ʹ�����Ǻ��������ά��λ������ x �� y ����
        float x = Mathf.Cos(radianAngle);
        float y = Mathf.Sin(radianAngle);

        return new Vector3(x, y, 0) * r;
    }
}
