using Assets.Scripts.Character.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 可被吸收的资源
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class Resources : MonoBehaviour, IAbsorb {
    // 该资源的energy
    [SerializeField] int energy;

    protected virtual void Awake() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public int GetEnergy()
    {
        Debug.Log("吸收能量");
        return energy;
    }

    public virtual void OnAbsorbAction(Transform playerTrans) {
        
    }
}
