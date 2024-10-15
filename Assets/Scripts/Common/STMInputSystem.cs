using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STM
{
    public class STMInputSystem : SingletonBase<STMInputSystem>
    {
        public Vector2 moveInput;
        public Vector2 look;
        public bool isStrafe;
        public bool isWalk;
        public bool isAim;
        public bool isFire;


        public delegate void OnJumpCallback();
        public OnJumpCallback onJumpCallback;
        public System.Action onTab;

        public System.Action<bool> onChangeFired;
        public System.Action onAttack;
        public System.Action onInteract;
        public System.Action<float> onMouseWheel;

        private Vector2 lastMousePosition;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                onTab?.Invoke();
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            moveInput = new Vector2(horizontal, vertical);
          

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            look = new Vector2(mouseX, mouseY);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                onJumpCallback();
            }

            isStrafe = Input.GetMouseButton(1);
            isAim = Input.GetMouseButton(1);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isWalk = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isWalk = false;
            }

            if (Input.GetMouseButtonDown(0)) // Mouse 왼쪽 버튼이 눌러졌다면
            {
                onAttack?.Invoke();
                onChangeFired?.Invoke(true);
                Debug.Log("Mouse Left Button Clicked");
            }

            if (Input.GetMouseButtonUp(0))
            {
                onChangeFired?.Invoke(false);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                onInteract?.Invoke();
            }

            float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseWheel > 0f)
            {
                onMouseWheel?.Invoke(mouseWheel);
            }
            else if (mouseWheel < 0f)
            {
                onMouseWheel?.Invoke(mouseWheel);
            }
        }
    }
}
