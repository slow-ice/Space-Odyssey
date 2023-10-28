using Assets.Scripts.Utility.Pool;
using System;
using UnityEngine;

namespace Assets.Scripts.Character.Bullet {
    public class BulletController : MonoBehaviour {
        public BulletStrategy BulletStrategy { get; private set; }
        public float moveSpeed;
        public Quaternion rotation;

        public ObjectPool parentPool;

        private void Awake() {
            parentPool = GetComponentInParent<ObjectPool>();
        }

        private void OnEnable() {
        }

        private void Update() {
            CheckDestroy();
        }

        private void FixedUpdate() {
            MoveForward();
        }

        void MoveForward() {
            transform.position += moveSpeed * transform.up * Time.fixedDeltaTime;
        }

        void CheckDestroy() {
            if (!IsInScreen()) {
                parentPool.Recycle(gameObject, null);
            }
        }

        bool IsInScreen() {
            Vector2 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1) {
                return true;
            }
            return false;
        }

        public void SetTraceMode() {

        }
    }
}
