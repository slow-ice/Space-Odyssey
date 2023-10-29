
using UnityEngine;

namespace Assets.Scripts.Character.Bullet {
    [RequireComponent(typeof(CircleCollider2D))]
    public class BossBulletController : MonoBehaviour {

        public Transform targetTransfom;

        public float rotateSpeed;
        public float moveSpeed;

        [Header("Attribute")]
        public int damage;
        public int energy;

        private void Update() {
            MoveTrace();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.TryGetComponent<PlayerController>(out var player)) {
                player.CheckBulletHit(damage, energy);
                Destroy(gameObject);
            }
        }

        void MoveTrace() {
            transform.up = Vector3.Slerp(transform.up, targetTransfom.position - transform.position,
                1f / Vector2.Distance(transform.position, targetTransfom.position) * rotateSpeed);
            transform.position += transform.up * moveSpeed * Time.fixedDeltaTime;
        }
    }
}
