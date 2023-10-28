
using System;
using UnityEngine;

namespace Assets.Scripts.Utility.Pool {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public interface IPool<T> {

        public T Spawn(Vector3 postion, Quaternion rotation, Action<T> onObjectSpawn);

        public void Recycle(T gameObject, Action onRecycleAction);

        public void InitPool(int poolSize, GameObject prefab);

    }
}
