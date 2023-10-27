
using Assets.Scripts.Utility.Input_System;
using UnityEngine;

namespace Assets.Scripts.Utility.CursorManage {
    public class CursorController : MonoBehaviour {
        public Texture2D CursorTexture;
        Vector2 cursorPosition = Vector2.zero;

        public SpriteRenderer SpriteRenderer;

        private void Awake() {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SetCursorVisibility();
        }

        private void Start() {
            SetCursorVisibility();
            //Cursor.visible = true;
        }

        private void Update() {
            cursorPosition = InputManager.Instance.MousePosition;
            var worldPos = Camera.main.ScreenToWorldPoint(cursorPosition);
            //Cursor.SetCursor(CursorTexture, cursorPosition, CursorMode.Auto);
            Debug.Log("cursor position" + cursorPosition);
            Debug.Log("world position: " + worldPos);
        }

        public void SetCursorVisibility() {
            Cursor.SetCursor(CursorTexture, cursorPosition, CursorMode.Auto);
        }
    }
}
