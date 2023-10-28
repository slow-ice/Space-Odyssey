
using UnityEngine;

namespace Assets.Scripts.Model.Enemy {
    [CreateAssetMenu(menuName = "Data/Enemy Data")]
    public class EnemyData_SO : ScriptableObject {
        public int health;
        public int damage;
        public int resourceNum;
    }
}
