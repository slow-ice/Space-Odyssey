

using Assets.Scripts.Utility.Pool;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Others {
    public class NormalResource : Resources {
        public ObjectPool parentPool;

        public float fadeDistance = 0.5f;
        public float SmoothTime = 1f;
        Vector2 workVelo;

        private new void Awake()
        {
            base.Awake();
            //获取对象池
            parentPool = GetComponentInParent<ObjectPool>();
        }

        public override void OnAbsorbAction(Transform playerTrans) {
            if (!gameObject.activeSelf)
                return;
            StartCoroutine(MoveToCenter(playerTrans));
        }

        IEnumerator MoveToCenter(Transform center) {
            while (Vector2.Distance(transform.position, center.position) > fadeDistance) {
                transform.position = Vector2.SmoothDamp(transform.position, center.position,
                    ref workVelo, SmoothTime);
                yield return null;
            }
            Debug.Log("fade");
            parentPool.Recycle(gameObject, () => {
                StopAllCoroutines();
            });
        }
    }
}
