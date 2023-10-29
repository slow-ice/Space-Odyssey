using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultControl : Singleton<DifficultControl>
{
    //获取刷怪点位
    public GameObject[] sp;
    //难度矩阵
    public int[] HardArray; 

    int score; //当前分数
    
    int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score = GetPlayerModel.Instance.pm.scoreCount;

        //难度提升
        if(index <  HardArray.Length  && score > HardArray[index])
        {
            Debug.Log("难度提升");
            
            sp[index].gameObject.SetActive(true);

            index++;
        }
        

        
        
    }

   

}
