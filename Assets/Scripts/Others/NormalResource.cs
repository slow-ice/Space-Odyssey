

using Assets.Scripts.Utility.Pool;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Others {
    public class NormalResource : Resources {
        public ObjectPool parentPool;

        public float fadeDistance = 0.5f;
        public float moveSpeed = 1f;

        public override void OnAbsorbAction(Transform playerTrans) {
            if (!gameObject.activeSelf)
                return;
            StartCoroutine(MoveToCenter(playerTrans));
        }

        IEnumerator MoveToCenter(Transform center) {
            while (Vector2.Distance(transform.position, center.position) > fadeDistance) {
                transform.position = Vector2.Lerp(transform.position, center.position,
                    moveSpeed * Time.fixedDeltaTime);
                yield return null;
            }
            Debug.Log("fade");
            parentPool.Recycle(gameObject, () => {
                StopAllCoroutines();
            });
        }
    }
}
