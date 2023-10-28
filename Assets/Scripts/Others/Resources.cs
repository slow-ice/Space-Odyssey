using Assets.Scripts.Character.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ɱ����յ���Դ
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class Resources : MonoBehaviour, IAbsorb {
    // ����Դ��energy
    [SerializeField] int energy;

    protected virtual void Awake() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public int GetEnergy()
    {
        Debug.Log("��������");
        return energy;
    }

    public virtual void OnAbsorbAction(Transform playerTrans) {
        
    }
}
