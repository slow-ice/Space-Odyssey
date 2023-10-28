using Assets.Scripts.Model.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ֶ�ʽ������
public class UMpBars : MonoBehaviour
{
    //��ȡ��ʼ����Ϣ
    public PlayerData_SO initData;

    //�洢4��������
    public Image[] mpbars;
    //һ���׶ε�MPҪ��������
    public float MP;

    float currentMP;

    int level;
    float level_MP;

    void Start()
    {
        //TODO : �������model��mpֵ
        MP = initData.maxEnergy / 4;
        currentMP = 0;
    }

    void Update()
    {
        FillMPBar();
    }
     
    void FillMPBar()
    {
        //��ȡmodel�ĵ�ǰ����ֵ
        currentMP = GetPlayerModel.Instance.pm.Energy;

        level = (int)(currentMP / MP);
        
        level_MP = currentMP - level  * MP;

        //����֮ǰ����ȫ������
        FullFill(level);

        //��ǰ�����
        Fill();

        //֮��������
        ClearFill(level);
    }

    void FullFill(int level)
    {
        if(level >= 4)
            level = 4;

        for( int i = 0; i < level; i++ )
        {
            mpbars[i].fillAmount = 1;
        }
    }

    void Fill()
    {
        if (level < 4)
        {
            mpbars[level].fillAmount = level_MP / MP;
        }
    }

    void ClearFill(int level)
    {
        for (int i = level + 1; i < mpbars.Length; i++)
        {
            mpbars[i].fillAmount = 0;
        }
    }
}
