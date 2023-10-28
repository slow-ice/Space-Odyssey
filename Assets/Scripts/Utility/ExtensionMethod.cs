using System;
using UnityEngine;

namespace Assets.Scripts.Utility {
    public static class ExtensionMethod {

        public static Vector2 ForwardPosition(this Transform mTrans, float distance) {
            return mTrans.position + mTrans.up * distance;
        }
    }
}
