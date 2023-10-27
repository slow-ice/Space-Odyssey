using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.Player {
    [CreateAssetMenu(menuName = "Player/Player Data")]
    public class PlayerData_SO : ScriptableObject {
        public int health;

        [Header("Move params")]
        public float maxMoveSpeed = 3f;
        public float accelerationTime = 1f;
        public float decelerationTime = 1f;

        public float rotateSpeed = 3f;
    }
}
