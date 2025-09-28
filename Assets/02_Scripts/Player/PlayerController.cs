using Pokemon3D.ScriptableObj;
using UnityEngine;
namespace Pokemon3D.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("컴포넌트")]
        [SerializeField] PlayerInputHandler inputHandler;
        [SerializeField] PlayerMovement movement;
        [SerializeField] PlayerAnimatorController animatorController;

        [Header("변수")]
        [SerializeField] PlayerData playerData;

        // 개인 변수들
        private bool isMoving => inputHandler.GetMovementInput().magnitude > 0.1f;
        private bool isInitialized = false;

        private void Awake()
        {
            InitializeComponents();
        }

        private void Start()
        {
            SetupComponentReferences();
            isInitialized = true;
        }

        // 하위 컴포넌트 초기화
        private void InitializeComponents()
        {
            if (inputHandler == null)
                inputHandler = GetComponent<PlayerInputHandler>();
            if (movement == null)
                movement = GetComponent<PlayerMovement>();
            if (animatorController == null)
                animatorController = GetComponent<PlayerAnimatorController>();
        }


        private void SetupComponentReferences()
        {
            // 이동 컨트롤러 설정
            if (movement != null)
            {
                movement.Initialize(playerData);
            }
        }

        private void Update()
        {
            if (!isInitialized)
                return;

            movement.Move(inputHandler.GetMovementInput(), inputHandler.GetRunInput());
            animatorController.MoveAnim(isMoving, inputHandler.GetMovementInput(), movement.GetCurrentSpeed(), inputHandler.GetRunInput());
        }
    }
}