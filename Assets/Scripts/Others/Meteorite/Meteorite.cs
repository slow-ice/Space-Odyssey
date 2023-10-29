using Assets.Scripts.Character.Resource;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ��ʯ�ű������ڱ��������

public class Meteorite : EnemyBase , IAbsorb
{
    [SerializeField] float decreaseSpeed; //�����յ��ٶ�
    [SerializeField] float minHP; //��С����ֵ��С�ڴ���ʯ��ʧ
    [SerializeField] float minSpawnHP, maxSpawnHP;
    [SerializeField] float speed = 10;

    

    float maxHP;
    Vector3 initScale; //�洢��ʼ��С

    #region �����߼�
    public override void damaged(int dmg)
    {
        //���غ��� ��Ϊ��ʯ�����ӵ��˺�
    }

    public override void die()
    {
        int num = enemyData.resourceNum;


        for (int i = 0; i < num; i++)
        {
            //�����ɢ������
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


    //������ص���ʱ
    public void OnEnable()
    {
        //����
        maxHP = Random.Range(minSpawnHP,maxSpawnHP);
        
        currentHP = (int)maxHP;

        initScale = (maxHP / 50.0f) * Vector3.one;
        
        transform.localScale = initScale;
    }

    new void Update()
    {
        //�ж��Ƿ񱻼���,����ִ��
        //base.Update();

        //��������������С
        transform.localScale = (currentHP / maxHP) * initScale;

        transform.Translate(speed * Time.deltaTime, 0, 0);

        if(currentHP < minHP)
        {
            die();
        }
    }

    
    
    
}
