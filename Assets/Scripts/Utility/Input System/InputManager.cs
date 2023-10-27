using UnityEngine;

namespace Assets.Scripts.Utility.Input_System {
    public class InputManager : Singleton<InputManager> {
        InputControl inputActions;

        bool fireInput;
        public bool Fire { get {
                return fireInput;
            }
            private set { }
        }

        Vector2 mouseInput;
        public Vector2 MousePosition {  get { return mouseInput; } private set {  } }

        private void OnEnable() {
            inputActions = new InputControl();

            inputActions.Player.Mouse.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();

            inputActions.Enable();
        }

    }
}
