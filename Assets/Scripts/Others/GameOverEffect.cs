using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float alpha;
    
    // Start is called before the first frame update
    void Start()
    {
        
        alpha = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (alpha > 0f)
        {
            alpha -= Time.deltaTime;
        }
        else
        {
            alpha = 0;
        }

        spriteRenderer.color = new Color(0f, 0f, 0f, alpha);

    }
}
