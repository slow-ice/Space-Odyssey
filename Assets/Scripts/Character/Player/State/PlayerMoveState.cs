

using Assets.Scripts.Refactoring;
using Assets.Utility;
using UnityEngine;

namespace Assets.Scripts.Character.Player.State {
    public class PlayerMoveState : PlayerState {

        bool changestate;
        public PlayerMoveState(PlayerEnumStates type) : base(type) {
        }

        public override void OnInit() {
            base.OnInit();

        }

        public override void OnEnter() {
            base.OnEnter();
            Debug.Log("Enter move");
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();

        }
    }
}
