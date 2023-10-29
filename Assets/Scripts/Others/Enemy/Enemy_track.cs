using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ⲿ�����ռ�
using Assets.Scripts.Character;

public class Enemy_track : EnemyBase
{
    [SerializeField] float rotateSpeed = 5f;

    [SerializeField] float speed = 10;

    bool isBeDestroyedByPlayer; //�ж��Ƿ���һ�ɱ

    // TODO : ��ȡ��ҵ�Transform
    Transform pTrasfrom;

    #region �����߼�
    public override void damaged(int dmg)
    {
        base.damaged(dmg);
    }

    public override void die()
    {
        //ֻ�б���һ�ɱ�����50%���ʱ����
        if (isBeDestroyedByPlayer && Random.Range(0f,1f) < 0.5f)
        {
            DropthingsManager.Instance.createDropThings(transform.position, transform.rotation);
        }
        pool.Recycle(gameObject,null);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {          
            GetPlayerModel.Instance.pm.ChangeHealth(-enemyData.damage);
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

    // �ɶ������������ʱִ��
    private void OnEnable()
    {
        //��������ʱ����Ѫ
        currentHP = enemyData.health;
    }

    new void Update()
    {
        base.Update();

        //�ı�λ�ý���׷��
        if (pTrasfrom)
        {
            transform.up = Vector3.Slerp(transform.up, pTrasfrom.position - transform.position
            , rotateSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("�Ҳ�����ң��������");
        }

        if (currentHP <= 0)
        {
            isBeDestroyedByPlayer = true;
            die();
        }

    }
}
