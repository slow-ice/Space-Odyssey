using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfTurn : MonoBehaviour
{
    public float turnSpeed;

   
    void Update()
    {
        transform.Rotate(0 , 0 ,turnSpeed *  Time.deltaTime);   
    }
}
