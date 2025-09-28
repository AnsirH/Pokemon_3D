using Pokemon3D.ScriptableObj;
using UnityEngine;

namespace Pokemon3D.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("컴포넌트")]
        [SerializeField] CharacterController characterController;

        // 개인 변수들
        private PlayerData playerData;
        private float currentSpeed;
        private Vector3 moveDirection;

        // 이동 인터페이스
        public interface IMoveable
        {
            void Move(Vector3 direction);
            void SetSpeed(float speed);
            bool CanMove(Vector3 direction);
            void SetRunning(bool running);
        }

        private void Awake()
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();
        }

        public void Initialize(PlayerData playerData)
        {
            this.playerData = playerData;
        }

        public void Move(Vector3 direction, bool isRunning)
        {
            moveDirection = direction;

            if (direction.magnitude >= 0.1f)
            {
                // 목표 속도 계산
                float targetSpeed = isRunning ? playerData.RunSpeed : playerData.WalkSpeed;
                currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, (isRunning ? playerData.Acceleration : playerData.Deceleration) * Time.deltaTime);

                // 이동 적용
                Vector3 movement = moveDirection * currentSpeed * Time.deltaTime;
                characterController.Move(movement);

                // 회전 처리
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, playerData.RotationSpeed * Time.deltaTime);
            }
            else
            {
                // 이동하지 않을 때 감속
                float deceleration = playerData.Deceleration;
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
            }
        }

        public bool CanMove(Vector3 direction)
        {
            // 이동 방향의 장애물 체크
            Vector3 checkPosition = transform.position + direction.normalized * 0.5f;

            // 작은 구체를 사용하여 충돌 체크
            Collider[] colliders = Physics.OverlapSphere(checkPosition, 0.3f);

            foreach (var collider in colliders)
            {
                if (collider.gameObject != gameObject && !collider.isTrigger)
                {
                    return false;
                }
            }

            return true;
        }

        public float GetCurrentSpeed()
        {
            return currentSpeed;
        }

        public Vector3 GetMoveDirection()
        {
            return moveDirection;
        }

        // 고급 이동 메서드들

        public void SetPosition(Vector3 position)
        {
            characterController.enabled = false;
            transform.position = position;
            characterController.enabled = true;
        }
    }

    // Character Movement 구현
    [System.Serializable]
    public class CharacterMovement : PlayerMovement.IMoveable
    {
        private CharacterController controller;
        private float speed;
        private bool isRunning;

        public CharacterMovement(CharacterController controller)
        {
            this.controller = controller;
            this.speed = 3.0f;
            this.isRunning = false;
        }

        public void Move(Vector3 direction)
        {
            if (controller != null)
            {
                controller.Move(direction * speed * Time.deltaTime);
            }
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public bool CanMove(Vector3 direction)
        {
            // 간단한 충돌 체크
            Vector3 checkPosition = controller.transform.position + direction.normalized * 0.5f;
            Collider[] colliders = Physics.OverlapSphere(checkPosition, 0.3f);

            foreach (var collider in colliders)
            {
                if (collider.gameObject != controller.gameObject && !collider.isTrigger)
                {
                    return false;
                }
            }

            return true;
        }

        public void SetRunning(bool running)
        {
            isRunning = running;
            speed = running ? 4.5f : 3.0f;
        }

    }
}