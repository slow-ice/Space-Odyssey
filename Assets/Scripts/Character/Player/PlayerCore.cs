

using Assets.Scripts.Character.Bullet;
using Assets.Scripts.Model.Player;
using Assets.Scripts.Utility.Input_System;
using Assets.Scripts.Utility.Pool;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Character {
    public class PlayerCore {
        public Transform mTransform { get; private set; }
        public Rigidbody2D mRigidbody { get; private set; }
        public PlayerController mController { get; private set; }
        public PlayerData_SO mPlayerData { get; private set; }
        public PlayerModel mModel { get; set; }
        public ObjectPool mPool { get; private set; }

        Vector2 mouseWorldPos = Vector2.zero;
        Vector2 moveDir = Vector2.zero;
        Vector2 workSpace = Vector2.zero;

        float curSpeed;
        float workSpeed;

        public bool isOnAttack { get; private set; }
        public bool canAttack { get; private set; } = true;

        float attackStopTime;

        public BulletStrategy bulletStrategy { get; private set; } = new BulletStrategy();

        public PlayerCore(PlayerController playerController) { 
            mController = playerController;
            mTransform = mController.GetComponent<Transform>();
        }

        public void OnInit() {
            mPlayerData = mController.PlayerData;
            mRigidbody = mController.GetComponent<Rigidbody2D>();
            mPool = mTransform.parent.GetComponentInChildren<ObjectPool>();
        }

        public void Move() {
            RotateToMouse();

            if (InputManager.Instance.Move) {
                mRigidbody.velocity = Vector2.SmoothDamp(mRigidbody.velocity, moveDir * mPlayerData.maxMoveSpeed,
                    ref workSpace, mPlayerData.accelerationTime);
            }
            else {
                mRigidbody.velocity = Vector2.Lerp(mRigidbody.velocity, Vector2.zero, mPlayerData.decelerationTime * Time.fixedDeltaTime);
            }
        }

        public void RotateToMouse() {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
            moveDir = mouseWorldPos - (Vector2)mTransform.position;
            moveDir.Normalize();

            if (moveDir != Vector2.zero) {
                float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90;
                var targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                mTransform.rotation = Quaternion.Slerp(mTransform.rotation, targetRotation, mPlayerData.rotateSpeed * Time.fixedDeltaTime);
            }
        }

        public void TakeFire() {
            if (InputManager.Instance.Fire && canAttack) {
                isOnAttack = true;
                if (mModel.Energy.Value > 0) {
                    FireTrace();
                }
                else {
                    FireForward();
                }
                canAttack = false;
                mController.StartCoroutine(SetAttackCoolDown(mPlayerData.attackCoolDown));
            }
            else {
                if (isOnAttack) {
                    attackStopTime = Time.time;
                    isOnAttack = false;
                }
                if (Time.time >  attackStopTime + mPlayerData.resetTime) {
                    //Debug.Log("can absorb");
                }
            }
        }

        void FireForward() {
            Debug.Log("Fire forward");
            //bulletStrategy.SetMoveMode()
            mPool.Spawn(mTransform.position, mTransform.rotation, null);
        }

        void FireTrace() {
            Debug.Log("Fire trace");
        }

        IEnumerator ResetIsOnAttack(float time) {
            yield return new WaitForSeconds(time);
            isOnAttack = false;
        }

        IEnumerator SetAttackCoolDown(float time) {
            if (canAttack) {
                yield break;
            }
            yield return new WaitForSeconds(time);
            canAttack = true;
        }

        //void BulletMoveForword(GameObject gameObject) {
        //    var trans = gameObject.transform
        //}

        public void CheckAbsorb() {

        }

        void Absorb() {
            //var colls = Physics2D.OverlapCircleAll(mTransform.position, mPlayerData.absorbRadius);
            //foreach (var coll in colls) {
            //    if (coll.TryGetComponent<IAbsorbale>(out var absorbale)) {
            //        absorbale.Absorb();
            //    }
            //}
        }
    }
}
