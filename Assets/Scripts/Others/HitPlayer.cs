using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ײ�����Ľű�

public class HitPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // todo : �ı��������ֵ,���ڻ����Ƕ�̬��
            GetPlayerModel.Instance.pm.ChangeHealth(-5);
            
        }
    }
}
