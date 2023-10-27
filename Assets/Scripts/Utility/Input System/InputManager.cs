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

        bool moveInput;
        public bool Move { get { return moveInput; } private set { } }

        Vector2 mouseInput;
        public Vector2 MousePosition {  get { return mouseInput; } private set {  } }

        private void OnEnable() {
            inputActions = new InputControl();

            inputActions.Player.Mouse.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();

            inputActions.Player.Move.started += ctx => moveInput = true;
            inputActions.Player.Move.canceled += ctx => moveInput = false;

            inputActions.Player.FIre.started += ctx => fireInput = true;
            inputActions.Player.FIre.canceled += ctx => fireInput = false;

            inputActions.Enable();
        }

    }
}
