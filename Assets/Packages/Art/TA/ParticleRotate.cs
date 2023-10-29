using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotate : MonoBehaviour
{
    private ParticleSystem particle;
    public GameObject follow;
    // Start is called before the first frame update
    void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        particle.startRotation3D = follow.transform.rotation.eulerAngles ;
        transform.rotation = follow.transform.rotation;
        Debug.Log(follow.transform.rotation);
    }
}
