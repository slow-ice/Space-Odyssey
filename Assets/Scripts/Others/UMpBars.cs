using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//分段式能量条
public class UMpBars : MonoBehaviour
{
    //存储4个能量条
    public Image[] mpbars;
    //一个阶段的MP要多少能量
    public float MP = 100f;

    //方便调试
    public float currentMP;

    int level;
    float level_MP;

    void Start()
    {
        //TODO : 接受玩家model的mp值
        currentMP = 50;
    }

    void Update()
    {
        FillMPBar();
    }
     
    void FillMPBar()
    {
        level = (int)(currentMP / MP);
        level_MP = currentMP - level  * MP;

        //在其之前的条全部填满
        FullFill(level);

        //当前条填充
        Fill();

        //之后的条清空
        ClearFill(level);
    }

    void FullFill(int level)
    {
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
