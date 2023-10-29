
using Assets.Scripts.Utility.Input_System;
using UnityEngine;

namespace Assets.Scripts.Utility.CursorManage {
    public class CursorController : MonoBehaviour {
        public Texture2D CursorTexture;
        SpriteRenderer SpriteRenderer;
        Vector2 cursorPosition = Vector2.zero;

        Transform player;

        private void Awake() {
            //SetCursorVisibility();
            UnityEngine.Cursor.visible = false;
            SpriteRenderer = GetComponent<SpriteRenderer>();
            player = transform.parent.GetChild(0);
        }

        private void Update() {
            SetPosition();
            SetRotation();
        }

        void SetPosition() {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
            transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
        }

        void SetRotation() {
            var dir = transform.position - player.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        public void SetCursorVisibility() {
            UnityEngine.Cursor.SetCursor(CursorTexture, cursorPosition, CursorMode.Auto);
        }
    }
}
