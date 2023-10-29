using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSlowMove : MonoBehaviour
{
    public float xspeed;
    public float yspeed;

    public float leftX;
    public float RightX;

    public float upY;
    public float downY;

    int direct;
    int Ydirect;

    // Start is called before the first frame update
    void Start()
    {
        direct = 1;
        Ydirect = -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direct * xspeed * Time.deltaTime, 0, 0);
        if (transform.position.x > RightX)
        {
            transform.position = new Vector3(RightX, transform.position.y, transform.position.z);
            direct = -1;
        }
        else if(transform.position.x < leftX)
        {
            transform.position = new Vector3(leftX,transform.position.y, transform.position.z);
            direct = 1;
        }

        transform.Translate(0, Ydirect * yspeed * Time.deltaTime , 0);
        if (transform.position.y > upY)
        {
            transform.position = new Vector3(transform.position.x, upY, transform.position.z);
            Ydirect = -1;
        }
        else if (transform.position.y < downY)
        {
            transform.position = new Vector3(transform.position.x, downY, transform.position.z);
            Ydirect = 1;
        }
    }
}
