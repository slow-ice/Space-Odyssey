using Assets.Scripts.Utility.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQueueSpawn : MonoBehaviour
{
    //∂‘œÛ≥ÿ
    public ObjectPool pool;

    [SerializeField]
    Transform[] vecs;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            SpawnEnemyQueue();
        }
    }

    private void SpawnEnemyQueue()
    {
        foreach (var v in vecs)
        {
            pool.Spawn(v.position, transform.rotation, null);
        }
    }
}
