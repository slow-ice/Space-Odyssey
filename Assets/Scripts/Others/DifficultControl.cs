using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultControl : Singleton<DifficultControl>
{
    //��ȡˢ�ֵ�λ
    public GameObject[] sp;
    //�ѶȾ���
    public int[] HardArray; 

    int score; //��ǰ����
    
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

        //�Ѷ�����
        if(index <  HardArray.Length  && score > HardArray[index])
        {
            Debug.Log("�Ѷ�����");
            
            sp[index].gameObject.SetActive(true);

            index++;
        }
        

        
        
    }

   

}
