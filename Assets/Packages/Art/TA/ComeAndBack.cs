using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeAndBack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject referenceCycle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(referenceCycle.transform.position.x, transform.position.y, transform.position.z);
    }
}
