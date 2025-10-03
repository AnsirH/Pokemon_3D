using Pokemon3D.ScriptableObj;
using UnityEngine;
namespace Pokemon3D.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("컴포넌트")]
        [SerializeField] PlayerInputHandler inputHandler;
        [SerializeField] PlayerMovement movement;
        [SerializeField] PlayerAnimationController animatorController;
        [SerializeField] SurfaceChecker surfaceChecker;
        [SerializeField] AudioSource audioSource;

        [Header("변수")]
        [SerializeField] PlayerData playerData;

        // 개인 변수들
        private bool isMoving => inputHandler.GetMovementInput().magnitude > 0.1f;
        private bool isInitialized = false;

        // properties
        public Vector3 MoveDirection => movement.GetMoveDirection();

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
                animatorController = GetComponent<PlayerAnimationController>();
            if (surfaceChecker == null)
                surfaceChecker = GetComponent<SurfaceChecker>();
        }


        private void SetupComponentReferences()
        {
            // 이동 컨트롤러 설정
            movement.Initialize(playerData);

            // 이벤트 등록
            animatorController.OnFootstepEvent += PlaySurfaceSoundStep;
            movement.OnStepEvent += ExecuteSurfaceEvent;
        }

        private void Update()
        {
            if (!isInitialized)
                return;

            movement.Move(inputHandler.GetMovementInput(), inputHandler.GetRunInput());
            animatorController.MoveAnim(isMoving, inputHandler.GetMovementInput(), movement.GetCurrentSpeed(), inputHandler.GetRunInput());
        }

        private void PlaySurfaceSoundStep()
        {
            audioSource.PlayOneShot(surfaceChecker.CheckSurface().SurfaceSound);
        }

        private void ExecuteSurfaceEvent()
        {
            surfaceChecker.CheckSurface().ExecuteSurfaceEvent(this);
        }
    }
}