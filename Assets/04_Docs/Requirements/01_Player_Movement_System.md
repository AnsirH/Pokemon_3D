# 플레이어 이동 시스템 요구사항 명세서

## 개요
플레이어 캐릭터의 이동, 애니메이션, 충돌 처리, 카메라 시스템을 담당하는 핵심 시스템입니다. 3D 포켓몬스터 게임의 기본적인 탐험 기능을 제공하며, 다른 모든 시스템의 기반이 됩니다.

### 주요 목표
- 8방향 부드러운 이동 구현
- 직관적인 입력 처리 (WASD, 방향키)
- 정확한 충돌 감지 및 이동 제한
- 탑다운 뷰 카메라 시스템
- 이동 상태에 따른 애니메이션 연동

### 관련 시스템과의 연관성
- **풀숲 시스템**: 이동을 통한 EncounterZone 진입
- **NPC 시스템**: 이동을 통한 NPC 접촉
- **배틀 시스템**: 이동 중 조우 시 배틀 전환
- **UI 시스템**: 이동 상태 표시 (HUD)

---

## 요구사항 목록

### REQ-001: 8방향 이동
- **설명**: 플레이어가 8방향(상, 하, 좌, 우, 대각선 4방향)으로 이동 가능
- **입력**: 
  - WASD 키 (W: 위, A: 왼쪽, S: 아래, D: 오른쪽)
  - 방향키 (↑, ←, ↓, →)
  - 조이스틱 입력 (향후 확장)
- **출력**: 
  - 플레이어 캐릭터의 8방향 이동
  - 이동 방향에 따른 애니메이션 전환
- **제약사항**: 
  - 이동 중 충돌 오브젝트와 겹치지 않음
  - 지형 경계를 벗어나지 않음
  - 최대 이동 속도 제한
- **수락 기준**: 
  - 모든 방향으로 정확히 이동
  - 이동 중 부드러운 애니메이션 전환
  - 충돌 시 즉시 이동 중단

### REQ-002: 속도 전환
- **설명**: Shift 키를 통한 걷기/달리기 속도 전환
- **입력**: 
  - Shift 키 (이동 중 누름/뗌)
  - 설정에서 키 바인딩 변경 가능
- **출력**: 
  - 걷기 속도: 기본 이동 속도 (예: 3.0 units/sec)
  - 달리기 속도: 걷기 속도의 1.5배 (예: 4.5 units/sec)
  - 속도 전환 시 부드러운 가속/감속
- **애니메이션**: 
  - 걷기: Walk 애니메이션
  - 달리기: Run 애니메이션
- **제약사항**: 
  - 이동 중에만 속도 전환 가능
  - 최대 속도 제한
- **수락 기준**: 
  - Shift 키 입력에 즉시 반응
  - 속도 전환 시 애니메이션 동기화
  - 부드러운 가속/감속 처리

### REQ-003: 충돌 처리
- **설명**: 지형, 건물, 나무 등과의 충돌 시 이동 제한
- **입력**: 
  - 충돌 감지 이벤트 (OnTriggerEnter, OnCollisionEnter)
  - 충돌 오브젝트의 Collider 정보
- **출력**: 
  - 이동 방향 차단
  - 충돌 지점에서 이동 중단
  - 충돌 피드백 (선택사항)
- **충돌 오브젝트 타입**: 
  - 지형 (Terrain)
  - 건물 (Building)
  - 나무 (Tree)
  - 기타 장애물 (Obstacle)
- **구현 방식**: 
  - CharacterController 사용 권장
  - Rigidbody + Collider 대안
- **제약사항**: 
  - 충돌 시 정확한 위치에서 멈춤
  - 벽을 통과하지 않음
- **수락 기준**: 
  - 모든 충돌 오브젝트에서 정확히 멈춤
  - 충돌 후 정상적인 이동 재개
  - 벽 통과 버그 없음

### REQ-004: 카메라 시스템
- **설명**: 탑다운 뷰 고정 시점 카메라
- **기능**: 
  - 플레이어를 따라가는 카메라
  - 부드러운 카메라 이동 (Lerp 사용)
  - 카메라 회전 (옵션)
  - 줌 인/아웃 (옵션)
- **입력**: 
  - 플레이어 위치
  - 마우스 휠 (줌)
  - 설정에서 카메라 회전 토글
- **출력**: 
  - 카메라 위치 업데이트
  - 부드러운 카메라 이동
- **구현**: 
  - Cinemachine Virtual Camera 사용
  - Follow 타겟으로 플레이어 설정
- **제약사항**: 
  - 카메라가 지형에 가려지지 않음
  - 최소/최대 거리 제한
- **수락 기준**: 
  - 플레이어를 정확히 따라감
  - 부드러운 카메라 이동
  - 지형에 가려지지 않음

---

## 상세 설계

### 클래스 구조

#### PlayerController (메인 컨트롤러)
```csharp
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3.0f;
    public float runSpeed = 4.5f;
    public float acceleration = 10.0f;
    public float deceleration = 10.0f;
    
    [Header("Components")]
    public CharacterController characterController;
    public Animator animator;
    public Transform cameraTarget;
    
    private Vector3 moveDirection;
    private bool isRunning;
    private float currentSpeed;
    
    public void Move(Vector2 input)
    {
        // 8방향 이동 로직
    }
    
    public void SetRunning(bool running)
    {
        // 달리기 상태 변경
    }
}
```

#### MovementController (이동 로직)
```csharp
public class MovementController : MonoBehaviour
{
    public interface IMoveable
    {
        void Move(Vector3 direction);
        void SetSpeed(float speed);
        bool CanMove(Vector3 direction);
    }
    
    public class CharacterMovement : IMoveable
    {
        private CharacterController controller;
        private float speed;
        
        public void Move(Vector3 direction)
        {
            // CharacterController 기반 이동
        }
        
        public bool CanMove(Vector3 direction)
        {
            // 충돌 체크
        }
    }
}
```

#### CollisionChecker (충돌 감지)
```csharp
public class CollisionChecker : MonoBehaviour
{
    [Header("Collision Settings")]
    public LayerMask obstacleLayer;
    public float checkDistance = 0.1f;
    
    public bool CheckCollision(Vector3 direction, float distance)
    {
        // Raycast를 통한 충돌 체크
    }
    
    public Vector3 GetValidMoveDirection(Vector3 desiredDirection)
    {
        // 충돌을 피한 유효한 이동 방향 반환
    }
}
```

#### CameraController (카메라 관리)
```csharp
public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;
    public float followSpeed = 5.0f;
    public Vector3 offset = new Vector3(0, 10, -5);
    public float rotationSpeed = 2.0f;
    
    private CinemachineVirtualCamera virtualCamera;
    
    public void FollowTarget(Transform newTarget)
    {
        // 타겟 변경
    }
    
    public void SetRotation(bool enable)
    {
        // 카메라 회전 활성화/비활성화
    }
}
```

### 메서드 시그니처

#### 입력 처리
```csharp
// PlayerInputHandler.cs
public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 GetMovementInput()
    {
        // WASD/방향키 입력 처리
    }
    
    public bool GetRunInput()
    {
        // Shift 키 입력 처리
    }
    
    public bool GetInteractionInput()
    {
        // Space 키 입력 처리
    }
}
```

#### 애니메이션 제어
```csharp
// PlayerAnimatorController.cs
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    
    public void SetMovementAnimation(Vector2 input, bool isRunning)
    {
        // 이동 애니메이션 설정
    }
    
    public void SetIdleAnimation()
    {
        // 대기 애니메이션 설정
    }
}
```

### 이벤트 흐름

#### 이동 처리 플로우
1. **입력 감지**: `PlayerInputHandler`가 WASD/방향키 입력 감지
2. **입력 전달**: `PlayerController`에 입력 벡터 전달
3. **충돌 체크**: `CollisionChecker`가 이동 방향 충돌 체크
4. **이동 실행**: `CharacterController`로 실제 이동
5. **애니메이션**: `Animator`에 이동 상태 전달
6. **카메라**: `CameraController`가 플레이어 위치 따라감

#### 속도 전환 플로우
1. **Shift 키 감지**: `PlayerInputHandler`가 Shift 키 상태 감지
2. **상태 변경**: `PlayerController`의 `isRunning` 상태 변경
3. **속도 계산**: 걷기/달리기 속도에 따른 `currentSpeed` 계산
4. **애니메이션**: `Animator`에 걷기/달리기 애니메이션 전환
5. **이동 적용**: 새로운 속도로 이동 처리

---

## 구현 가이드

### 기술적 고려사항

#### 물리 시스템 선택
- **CharacterController 권장**: 
  - 정밀한 이동 제어
  - 충돌 감지 최적화
  - 물리 시뮬레이션 불필요
- **Rigidbody 대안**: 
  - 물리 기반 이동
  - 더 복잡한 충돌 처리
  - 성능 오버헤드

#### 입력 시스템
- **Unity Input System 사용**: 
  - 모던한 입력 처리
  - 키 바인딩 변경 용이
  - 멀티플랫폼 지원
- **레거시 Input Manager**: 
  - 간단한 구현
  - 제한된 기능

#### 애니메이션 시스템
- **Animator Controller**: 
  - 상태 기반 애니메이션
  - 부드러운 전환
  - 파라미터 기반 제어
- **Animation Events**: 
  - 정확한 타이밍 제어
  - 사운드/이펙트 연동

### 성능 요구사항

#### 프레임레이트
- **목표**: 60fps 유지
- **최소**: 30fps 이하로 떨어지지 않음
- **최적화**: 
  - 불필요한 Update 호출 최소화
  - 캐싱 활용
  - Object Pooling 고려

#### 메모리 사용량
- **목표**: 100MB 이하
- **최적화**: 
  - 텍스처 압축
  - 애니메이션 압축
  - 불필요한 컴포넌트 제거

### 테스트 케이스

#### 기능 테스트
1. **8방향 이동 테스트**
   - 각 방향으로 정확히 이동하는지 확인
   - 대각선 이동이 정확한 각도로 되는지 확인
   - 이동 중 애니메이션이 올바르게 재생되는지 확인

2. **속도 전환 테스트**
   - Shift 키 입력에 즉시 반응하는지 확인
   - 걷기/달리기 속도가 정확한지 확인
   - 애니메이션 전환이 부드러운지 확인

3. **충돌 처리 테스트**
   - 모든 충돌 오브젝트에서 정확히 멈추는지 확인
   - 벽을 통과하지 않는지 확인
   - 충돌 후 정상적인 이동이 가능한지 확인

4. **카메라 테스트**
   - 플레이어를 정확히 따라가는지 확인
   - 부드러운 카메라 이동이 되는지 확인
   - 지형에 가려지지 않는지 확인

#### 성능 테스트
1. **프레임레이트 테스트**
   - 60fps 유지 여부 확인
   - 이동 중 프레임 드롭 없음 확인

2. **메모리 테스트**
   - 메모리 누수 없음 확인
   - 가비지 컬렉션 최소화 확인

#### 통합 테스트
1. **다른 시스템과의 연동**
   - 풀숲 진입 시 정상 작동
   - NPC 접촉 시 정상 작동
   - 배틀 전환 시 정상 작동

---

## 참고사항

### 관련 파일
- `PlayerController.cs`: 메인 플레이어 컨트롤러
- `PlayerAnimatorController.cs`: 애니메이션 제어
- `PlayerInputHandler.cs`: 입력 처리
- `MovementController.cs`: 이동 로직
- `CollisionChecker.cs`: 충돌 감지
- `CameraController.cs`: 카메라 관리

### 의존성
- **Unity Input System**: 입력 처리
- **Cinemachine**: 카메라 시스템
- **CharacterController**: 물리 기반 이동
- **Animator**: 애니메이션 시스템

### 향후 확장 계획
1. **점프 시스템**: 3D 환경에서의 점프 기능
2. **달리기 스태미나**: 달리기 시 스태미나 소모
3. **이동 효과**: 먼지, 발자국 등 시각적 효과
4. **음향 효과**: 발걸음 소리, 달리기 소리
5. **모바일 지원**: 터치 입력, 가상 조이스틱

### 버그 대응
- **벽 통과 버그**: CollisionChecker 강화
- **애니메이션 동기화**: Animator 파라미터 정확성 확인
- **카메라 지터**: Lerp 값 조정
- **입력 지연**: Input System 설정 최적화

---

**문서 끝**
