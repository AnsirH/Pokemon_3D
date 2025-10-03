using System;
using UnityEngine;

namespace Pokemon3D.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [Header("애니메이션 설정")]
        public float animationSmoothTime = 0.1f;
        public float rotationSmoothTime = 0.1f;

        [Header("애니메이션 파라미터")]
        public string speedParameter = "Speed";
        public string isRunningParameter = "IsRunning";
        public string isMovingParameter = "IsMoving";
        public string xDirectionParameter = "XDirection";
        public string zDirectionParameter = "ZDirection";

        [Header("컴포넌트")]
        public Animator animator;

        public event Action OnFootstepEvent;

        // 개인 변수들
        private float speedVelocity;
        private Vector2 currentDirection;
        private Vector2 directionVelocity;

        private void Awake()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            if (animator == null)
                animator = GetComponent<Animator>();
        }

        public void MoveAnim(bool isMoving, Vector3 direction, float moveSpeed = 0.0f, bool isRun = false)
        {
            if (isMoving == false)
            {
                SetAnimationParameters(isMoving, false, moveSpeed);
            }
            else
            {
                // 플레이어의 회전을 고려한 상대적 이동 방향 계산
                Vector2 targetDirection = GetRelativeMovementDirection(direction);

                // 부드러운 방향 변화
                currentDirection = Vector2.SmoothDamp(currentDirection, targetDirection, ref directionVelocity, rotationSmoothTime);

                // 부드러운 속도 변화
                float smoothedSpeed = Mathf.SmoothDamp(animator.GetFloat(speedParameter), moveSpeed, ref speedVelocity, animationSmoothTime);

                // 애니메이터 파라미터 설정
                SetAnimationParameters(isMoving, isRun, smoothedSpeed);
            }
        }

        private Vector2 GetRelativeMovementDirection(Vector3 direction)
        {
            // 플레이어의 right, forward 벡터를 사용하여 상대적 방향 계산
            Vector3 relativeDirection = transform.InverseTransformDirection(direction);

            // XZ 평면에서의 방향 벡터 (Y는 무시)
            return new Vector2(relativeDirection.x, relativeDirection.z).normalized;
        }

        private void SetAnimationParameters(bool isMoving, bool isRunning, float speed)
        {
            // 속도 파라미터
            animator.SetFloat(speedParameter, speed);

            // 불린 파라미터들
            animator.SetBool(isRunningParameter, isRunning);
            animator.SetBool(isMovingParameter, isMoving);

            // 8방향 이동을 위한 방향 파라미터들
            animator.SetFloat(xDirectionParameter, currentDirection.x);
            animator.SetFloat(zDirectionParameter, currentDirection.y);
        }

        public void SetIdleAnimation()
        {
            if (animator == null)
                return;

            animator.SetFloat(speedParameter, 0f);
            animator.SetBool(isMovingParameter, false);
            animator.SetBool(isRunningParameter, false);
            animator.SetFloat(xDirectionParameter, 0f);
            animator.SetFloat(zDirectionParameter, 0f);
        }


        public void SetSpeed(float speed)
        {
            if (animator != null)
            {
                animator.SetFloat(speedParameter, speed);
            }
        }

        public void SetRunning(bool running)
        {
            if (animator != null)
            {
                animator.SetBool(isRunningParameter, running);
            }
        }

        public void SetMoving(bool moving)
        {
            if (animator != null)
            {
                animator.SetBool(isMovingParameter, moving);
            }
        }

        // 애니메이션 이벤트 콜백들
        public void OnFootstep()
        {
            OnFootstepEvent?.Invoke();
        }

        // 상태 확인 메서드들 (이동 시스템용)
        public bool IsInIdleState()
        {
            if (animator == null)
                return false;

            return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        }

        public bool IsInWalkState()
        {
            if (animator == null)
                return false;

            return animator.GetCurrentAnimatorStateInfo(0).IsName("Walk");
        }

        public bool IsInRunState()
        {
            if (animator == null)
                return false;

            return animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
        }

        public bool IsAnimationPlaying(string stateName)
        {
            if (animator == null)
                return false;

            return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        public float GetAnimationLength(string stateName)
        {
            if (animator == null)
                return 0f;

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(stateName))
            {
                return stateInfo.length;
            }

            return 0f;
        }

        public float GetAnimationProgress()
        {
            if (animator == null)
                return 0f;

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.normalizedTime;
        }

        // 디버그 메서드들
        public void LogCurrentState()
        {
            if (animator == null)
                return;

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log($"현재 애니메이션 상태: {stateInfo.shortNameHash}, 진행도: {stateInfo.normalizedTime:F2}");
        }
    }
}