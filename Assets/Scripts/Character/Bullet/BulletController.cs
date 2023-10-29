using Assets.Scripts.Utility.Pool;
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Character.Bullet {
    public class BulletController : MonoBehaviour {
        public BulletStrategy BulletStrategy { get; private set; }
        public Quaternion rotation;
        public ObjectPool parentPool;
        Collider2D Collider2D;
        TrailRenderer TrailRenderer;
        public ParticleSystem HitParticle;

        public float moveSpeed;
        public float rotateSpeed = 1f;
        public float maxInitAngle;

        Vector3 initEulerAngle;

        public int damage = 50;


        public bool IsTracingMode { get; set; }

        Transform targetTransfom;
        List<Transform> transCaches;

        private void Awake() {
            parentPool = GetComponentInParent<ObjectPool>();
            TrailRenderer = GetComponent<TrailRenderer>();
            HitParticle = GetComponentInChildren<ParticleSystem>();
        }

        private void OnEnable() {
            TrailRenderer.enabled = true;
            TrailRenderer.Clear();
            transCaches = null;
            targetTransfom = null;
            initEulerAngle = transform.eulerAngles;
            transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, maxInitAngle) *
                    (UnityEngine.Random.Range(-1f, 1f) > 0 ? 1 : -1);
        }

        private void Update() {
            CheckDestroy();
        }

        private void FixedUpdate() {
            CheckMoveMode();
        }

        // TODO: 待更改
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.TryGetComponent<EnemyBase>(out var enemyBase)) {
                enemyBase.damaged(damage);
                HitParticle.Play(true);
                parentPool.Recycle(gameObject, null);
            }
        }

        void CheckMoveMode() {
            if (IsTracingMode) {
                if (targetTransfom != null && !targetTransfom.gameObject.activeSelf) {
                    IsTracingMode = false;
                    initEulerAngle = transform.eulerAngles;
                }
                MoveTrace();
            }
            else {
                MoveForward();
            }
        }

        void MoveForward() {
            transform.eulerAngles = initEulerAngle;
            transform.position += moveSpeed * transform.up * Time.fixedDeltaTime;
        }

        void MoveTrace() {
            FindTarget();
            if (targetTransfom == null) {
                TrailRenderer.enabled = false;
                parentPool.Recycle(gameObject, () => {
                    IsTracingMode = false;
                    transCaches = null;
                });
                return;
            }
            transform.up = Vector3.Slerp(transform.up, targetTransfom.position - transform.position,
                1f / Vector2.Distance(transform.position, targetTransfom.position) * rotateSpeed);
            transform.position += transform.up * moveSpeed * Time.fixedDeltaTime;
        }

        void FindTarget() {
            if (transCaches == null) {
                transCaches = GameObject.FindGameObjectsWithTag("Enemy").
                    Select(go => go.transform).ToList();
                targetTransfom = null;
                var minDis = 100000f;
                foreach (Transform t in transCaches) {
                    var dis = Vector3.Distance(t.position, transform.position);
                    if (dis < minDis) {
                        minDis = dis;
                        targetTransfom = t;
                    }
                }
            }
        }

        void CheckDestroy() {
            if (!IsInScreen()) {
                TrailRenderer.enabled = false;
                parentPool.Recycle(gameObject, () => {
                    IsTracingMode = false;
                    transCaches = null;
                });
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
