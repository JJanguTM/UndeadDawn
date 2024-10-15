using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STM
{
    public class CharacterController : MonoBehaviour
    {
        public static CharacterController Instance;

        public Transform cameraPivot;
        public float cameraRotationSpeed = 30f;

        private float yaw;                        // yaw 값은 캐릭터의 y축 회전값을 의미한다.
        private float pitch;                      // pitch 값은 캐릭터의 x축 회전값을 의미한다.

        private CharacterBase characterBase;

        private void Awake()
        {
            Instance = this;
            characterBase = GetComponent<CharacterBase>();
        }

        private void Start()
        {
            STMInputSystem.Singleton.onJumpCallback += Jump; // Callback 함수에 Chain(체인)을 건다
            STMInputSystem.Singleton.onTab += OnCameraSideChange;
        }

        private void OnCameraSideChange()
        {
            if (characterBase.IsAlive)
            {
                CameraSystem.Instance.SetChangeCameraSide();
            }
        }

       

        private void Update()
        {
            Vector2 input = STMInputSystem.Singleton.moveInput;
           
            if (characterBase.IsAlive)
            {
                characterBase.Move(input, Camera.main.transform.rotation.eulerAngles.y);
                characterBase.Rotate(CameraSystem.Instance.AimingTargetPoint);// - Move() 함수로 input 값과, 현재 Main Camera의
                characterBase.IsStrafe = STMInputSystem.Singleton.isStrafe;
                characterBase.IsWalk = STMInputSystem.Singleton.isWalk;

                characterBase.IsAiming = STMInputSystem.Singleton.isAim;
                CameraSystem.Instance.SetActiveAimingCamera(STMInputSystem.Singleton.isAim);

            }
            else
            {
                characterBase.Move(Vector2.zero, Camera.main.transform.rotation.eulerAngles.y);
            }

        }

        private void LateUpdate()
        {
            Vector2 look = STMInputSystem.Singleton.look;  // - Input System에서 mouse look 값을 가져온다.
            CameraRotate(look);                           // - CameraRotate() 함수로 mouse look 값을 전달했음
        }

        private void Attack()
        {
            characterBase.Attack();
        }

        private void Fire(bool isFire)
        {
            characterBase.Fire(isFire);
        }

        private void Jump()
        {
            characterBase.Jump();
        }

        private void CameraRotate(Vector2 look)
        {
            yaw += look.x * cameraRotationSpeed * Time.deltaTime;      // yaw 값에 look.x * cameraRotationSpeed * Time.deltaTime 값을 더한다.
            pitch += look.y * cameraRotationSpeed * Time.deltaTime;    // pitch 값에 look.y * cameraRotationSpeed * Time.deltaTime 값을 더한다.
            pitch = Mathf.Clamp(pitch, -80, 80); // pitch 값의 범위를 -80 ~ 80 사이로 제한한다.

            cameraPivot.rotation = Quaternion.Euler(pitch, yaw, 0.0f); // CameraPivot 이라는 Transform을 회전시킨다.
        }
    }
}
