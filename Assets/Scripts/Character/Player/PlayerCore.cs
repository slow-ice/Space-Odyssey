

using UnityEngine;

namespace Assets.Scripts.Character {
    public class PlayerCore {
        public Transform mTransform { get; private set; }
        public Rigidbody2D mRigidbody { get; private set; }
        public PlayerController mController { get; private set; }


        public PlayerCore(PlayerController playerController) { 
            mController = playerController;
            mTransform = mController.GetComponent<Transform>();
        }

        public void OnInit() {

        }

        public void Move() {

        }
    }
}
