using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.Player {
    [CreateAssetMenu(menuName = "Player/Player Data")]
    public class PlayerData_SO : ScriptableObject {
        [Header("Attributes")]
        public int health = 100;
        public int maxEnergy = 350;
        public float absorbRadius = 3f;
        public float absorbEdgeMoveSpeed = 0.5f;
        public float trailParticleFadeSpeed = 1f;

        [Header("Move params")]
        public float maxMoveSpeed = 3f;
        public float accelerationTime = 1f;
        public float decelerationTime = 1f;
        public float rotateSpeed = 3f;


        [Header("Attack Params")]
        public int attackValue = 1;
        public int attackCost = 1;
        public float attackCoolDown = 0.1f;
        public float resetTime = 0.5f;
    }
}
