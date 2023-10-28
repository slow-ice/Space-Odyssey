

using System;
using UnityEngine;

namespace Assets.Scripts.Character.Bullet {
    public class BulletStrategy {
        public Action<GameObject> moveMode;
        public float moveSpeed;

        public BulletStrategy(Action<GameObject> moveMode = null, float moveSpeed = 0f) {
            this.moveMode = moveMode;
            this.moveSpeed = moveSpeed;
        }

        public void SetMoveMode(Action<GameObject> moveMode) {
            this.moveMode = moveMode;
        }

        public void SetMoveSpeed(float moveSpeed) {
            this.moveSpeed = moveSpeed;
        }
    }
}
