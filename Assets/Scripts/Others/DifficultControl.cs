using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultControl : Singleton<DifficultControl>
{
    //获取点位
    public GameObject[] sp;

    public float time;

    bool harder;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        time = 0;   
        harder = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 10f && index < sp.Length)
        {
            time = 0;

            sp[index].gameObject.SetActive(true);
            index++;
        }
    }

   

}
