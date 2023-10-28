using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utility.Pool {
    public class ObjectPool : MonoBehaviour, IPool<GameObject> {
        [SerializeField]
        private int poolSize = 25;
        private int activeNum;

        public float maxRecycleTime;

        [SerializeField]
        private GameObject poolObject;

        [SerializeField]
        private Transform poolRoot;

        public List<GameObject> Pool;

        private List<float> activeTimeList;

        void Start() {
            InitPool(poolSize, poolObject);
        }

        void MaxRecycleTime() {
            for (int i = 0; i < poolSize; i++) {
                if (Pool[i].activeSelf) {
                    activeTimeList[i] += Time.deltaTime;
                }
                if (activeTimeList[i] >= maxRecycleTime) {
                    Recycle(Pool[i], () => {
                        Debug.Log("该对象已回收!");

                    });
                    activeTimeList[i] = 0f;
                }
            }
        }

        public void InitPool(int poolSize, GameObject prefab) {
            Debug.Log(poolSize);
            Pool = new List<GameObject>(poolSize);
            Debug.Log(Pool.Count);
            GameObject gameObject;
            for (int i = 0; i < poolSize; i++) {
                gameObject = Instantiate(poolObject, poolRoot);
                gameObject.SetActive(false);
                Pool.Add(gameObject);
            }
            activeTimeList = Enumerable.Repeat(0f, poolSize).ToList();
        }

        public void Recycle(GameObject gameObject, Action onRecycleAction) {
            gameObject.SetActive(false);
            onRecycleAction?.Invoke();
        }

        public GameObject Spawn(Vector3 postion, Quaternion rotation, Action<GameObject> onObjectSpawn) {
            // 第一个未激活对象
            var firstInactiveObject = Pool.FirstOrDefault(x => x.activeSelf == false);
            if (firstInactiveObject != null) {
                activeNum++;
                firstInactiveObject.transform.position = postion;
                firstInactiveObject.transform.rotation = rotation;
                firstInactiveObject.SetActive(true);
                onObjectSpawn?.Invoke(firstInactiveObject);
                return firstInactiveObject;
            }

            return null;
        }
    }
}
