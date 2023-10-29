

using Assets.Scripts.Character.Bullet;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Others.Enemy {
    public class E_BossController : EnemyBase {
        public GameObject bulletPrefab;

        Vector2 randomPartrolPoint = Vector2.zero;

        public float randomPartrolTime = 2f;
        public float minPlayerDistance = 5f;
        float lastPatrolTime;
        public float patrolCoolDown = 2f;
        [Header("Move")]
        public float moveSpeed = 2f;

        [Header("Fire")]
        public int fireNum = 10;
        public float fireInterval = 0.1f;
        private int fireCntTmp;
        public float rotationRange = 60f;
        public float fireCoolDown = 3f;
        private float lastFireTime;

        bool patrolFlag;

        public Transform mPlayerTrans;

        private void Awake() {
            patrolFlag = false;
            mPlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        }

        new void Start() {
            SetRandomPatrol();
        }

        new void Update() {
            FindPatrol();
            Move();
            TakeFire();

            Debug.Log(currentHP);
            if (currentHP <= 0) {
                die();
            }
        }

        void Move() {
            if ( Vector2.Distance(transform.position, randomPartrolPoint) < 1f)
            {
                return;
            }
            lastPatrolTime = Time.time;
            transform.position = Vector2.MoveTowards(transform.position, randomPartrolPoint,
                moveSpeed * Time.deltaTime);
        }

        void FindPatrol() {
            if (patrolFlag) {
                patrolFlag = false;
                SetRandomPatrol();
            }
            SetPatrolFlag();
        }

        void SetRandomPatrol() {
            var tx = Random.Range(-1f, 1f) > 0 ?
                Random.Range(-9.5f, -5f) :
                Random.Range(5f, 9.5f);
            var ty = Random.Range(-1f, 1f) > 0 ?
                Random.Range(-4.1f, -2.2f) :
                Random.Range(2.2f, 4.1f);
            randomPartrolPoint.Set(tx, ty);
            if (Vector2.Distance(randomPartrolPoint, mPlayerTrans.position) < minPlayerDistance ) {
                patrolFlag = true;
            }
        }

        void SetPatrolFlag() {
            if (patrolFlag == false) {
                if (Time.time > lastPatrolTime + patrolCoolDown) {
                    patrolFlag = true;
                }
            }
        }

        void TakeFire() {
            if (Time.time > lastFireTime + fireCoolDown) {
                fireCntTmp = 0;
                InvokeRepeating("InstanciateBullet", 0f, fireInterval);
                lastFireTime = Time.time;
            }
            if (fireCntTmp > fireNum) {
                fireCntTmp = 0;
                CancelInvoke("InstanciateBullet");
            }
        }

        IEnumerator fireCorotine(float time) {
            for (int i = 0; i < fireNum; i++) {
                Debug.Log("instantiate");
                yield return new WaitForSeconds(time);
                InstanciateBullet();
            }
            Debug.Log("end");
        }

        void InstanciateBullet() {
            fireCntTmp++;
            var targetDir = mPlayerTrans.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90;
            angle += Random.Range(-rotationRange, rotationRange);
            var targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            var go = Instantiate(bulletPrefab, transform.position, targetRotation);
            go.GetComponent<BossBulletController>().targetTransfom = mPlayerTrans;
            go.SetActive(true);
        }

        public override void die() {
            Destroy(gameObject);
        }
    }
}
