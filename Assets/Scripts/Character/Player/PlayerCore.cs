

using Assets.Scripts.Character.Bullet;
using Assets.Scripts.Character.Resource;
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
        public SpriteRenderer mSpriteRenderer { get; private set; }
        public PlayerController mController { get; private set; }
        public PlayerData_SO mPlayerData { get; private set; }
        public PlayerModel mModel { get; set; }
        public ObjectPool mPool { get; private set; }
        public ParticleSystem mParticleSystem { get; private set; }

        Vector2 mouseWorldPos = Vector2.zero;
        Vector2 moveDir = Vector2.zero;
        Vector2 workSpace = Vector2.zero;

        Color particleColor = new Color(1, 1, 1, 0);
        float particleAlpha = 0f;

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
            mSpriteRenderer = mController.GetComponent<SpriteRenderer>();
            mParticleSystem = mController.GetComponentInChildren<ParticleSystem>();
            mPool = mTransform.parent.GetComponentInChildren<ObjectPool>();
        }

        public void Move() {
            RotateToMouse();

            if (InputManager.Instance.Move) {
                CheckPlayParticle();
                mRigidbody.velocity = Vector2.SmoothDamp(mRigidbody.velocity, moveDir * mPlayerData.maxMoveSpeed,
                    ref workSpace, mPlayerData.accelerationTime);
            }
            else {
                CheckStopParticle();
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

        void CheckPlayParticle() {
            SetParticleFade(1f);
            if (!mParticleSystem.isPlaying) {
                mParticleSystem.Play();
            }
        }
        void CheckStopParticle() {
            if (mParticleSystem.isPlaying) {
                SetParticleFade(0f);
            }
            if (particleAlpha == 0f) {
                mParticleSystem.Stop();
            }
        }

        void SetParticleFade(float target) {
            particleAlpha = Mathf.MoveTowards(particleAlpha, target,
                    mPlayerData.trailParticleFadeSpeed * Time.fixedDeltaTime);
            var color = new Color(1, 1, 1, particleAlpha);
            var particleMain = mParticleSystem.main;
            particleMain.startColor = color;
        }

        #region Fire
        public void TakeFire() {
            if (InputManager.Instance.Fire && canAttack) {
                outAttack = false;
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
                    outAttack = true;
                }
            }
        }

        void FireForward() {
            //bulletStrategy.SetMoveMode()
            mPool.Spawn(mTransform.position, mTransform.rotation, null);
        }

        void FireTrace() {
            Debug.Log("Fire trace");
            mPool.Spawn(mTransform.position, mTransform.rotation, BulletTrace);
        }

        void BulletTrace(GameObject go) {
            var controller = go.GetComponent<BulletController>();
            controller.IsTracingMode = true;
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

        #endregion

        #region Absorb

        bool outAttack = true;
        public bool canAbsorb {
            get {
                return outAttack &&
                    mRigidbody.velocity.magnitude < mPlayerData.attackToAbsorbTime;
            }
        }

        public void CheckAbsorb() {
            if (canAbsorb) {
                Absorb();
                Debug.Log("absorbing!");
            }
        }

        void Absorb() {
            var colls = Physics2D.OverlapCircleAll(mTransform.position, mPlayerData.absorbRadius);
            foreach (var coll in colls) {
                if (coll.TryGetComponent<IAbsorb>(out var absorbale)) {
                    absorbale.OnAbsorbAction(mTransform);
                    mModel.ChangeEnergy(absorbale.GetEnergy());
                }
            }
        }
        #endregion

        public void CheckBounds() {
            var viewportPos = Camera.main.WorldToViewportPoint(mTransform.position);
            if (viewportPos.x < 0) {
                mTransform.position = Camera.main.ViewportToWorldPoint(new Vector3(
                    viewportPos.x + 1, viewportPos.y, viewportPos.z));
            }
            else if (viewportPos.x > 1 ) {
                mTransform.position = Camera.main.ViewportToWorldPoint(new Vector3(
                    viewportPos.x - 1, viewportPos.y, viewportPos.z));
            }
            if (viewportPos.y < 0) {
                mTransform.position = Camera.main.ViewportToWorldPoint(new Vector3(
                    viewportPos.x, viewportPos.y + 1, viewportPos.z));
            }
            else if (viewportPos.y > 1) {
                mTransform.position = Camera.main.ViewportToWorldPoint(new Vector3(
                    viewportPos.x, viewportPos.y - 1, viewportPos.z));
            }
            
        }
    }
}
