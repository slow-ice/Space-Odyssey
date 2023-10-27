
using UnityEngine;

namespace Assets.Utility {
    public class ConfigInfo {
        public static LayerMask PlayerLayer = LayerMask.NameToLayer("Player");
        public static LayerMask EnemyLayer = LayerMask.NameToLayer("Enemy");
    }

    
    public enum PlayerEnumStates {
        Root, Move, 
    }
}
