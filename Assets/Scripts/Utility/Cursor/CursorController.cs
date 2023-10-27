
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

        public void SetCursorVisibility() {
            Cursor.SetCursor(CursorTexture, cursorPosition, CursorMode.Auto);
        }
    }
}
