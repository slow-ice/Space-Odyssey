using Assets.Scripts.Character.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 可被吸收的资源
public class Resources : MonoBehaviour, IAbsorb
{
    // 该资源的energy
    [SerializeField] int energy;

    public int GetEnergy()
    {
        Debug.Log("吸收能量");
        return energy;
    }    
}
