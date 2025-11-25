# Pokemon 3D

Unity로 제작한 3D 포켓몬스터 모작 프로젝트입니다. 턴제 배틀 시스템, 야생 포켓몬 조우, 플레이어 이동 등 포켓몬스터의 핵심 게임플레이를 구현했습니다.

## 주요 기능

- **3D 필드 탐험**: 8방향 이동, 걷기/달리기 전환, 애니메이션 블렌딩
- **야생 포켓몬 조우**: 풀숲에서 확률 기반 랜덤 인카운터
- **턴제 배틀 시스템**: 기술 선택, 데미지 계산, 타입 상성, 스탯 변화
- **포켓몬 파티 관리**: 6마리 파티, 레벨업, 경험치, 기술 학습
- **UI 시스템**: HP/EXP 게이지 바, 배틀 메시지, 기술 선택 버튼

## 기술 스택

- **엔진**: Unity 2022+
- **언어**: C#
- **입력 시스템**: Unity New Input System
- **아키텍처**: 싱글톤 패턴, 이벤트 기반 설계, ScriptableObject 데이터 관리

## 프로젝트 구조

```
Assets/02_Scripts/
├── BattleSystem/          # 배틀 시스템
│   ├── BattleSystem.cs         # 배틀 메인 컨트롤러
│   ├── BattleSystem.Move.cs    # 기술 처리 로직
│   ├── BattleTextManager.cs    # 배틀 텍스트 관리
│   ├── UI/                     # 배틀 UI 컴포넌트
│   │   ├── BattleCanvas.cs
│   │   ├── BattleHud.cs
│   │   ├── GaugeBar.cs
│   │   ├── HpBar.cs
│   │   ├── MoveButton.cs
│   │   ├── MoveButtonPanel.cs
│   │   └── TextArea.cs
│   └── Unit/                   # 배틀 유닛
│       ├── PokemonUnit.cs
│       └── PokemonActionController.cs
├── Core/                  # 코어 시스템
│   ├── GameManager.cs          # 게임 매니저
│   ├── GameFlowManager.cs      # 게임 흐름 관리
│   └── EncounterManager.cs     # 인카운터 연출
├── Player/                # 플레이어 시스템
│   ├── PlayerController.cs     # 플레이어 메인 컨트롤러
│   ├── PlayerInputHandler.cs   # 입력 처리
│   ├── PlayerMovement.cs       # 이동 로직
│   ├── PlayerAnimationController.cs  # 애니메이션
│   └── SurfaceChecker.cs       # 지면 감지
├── Pokemon/               # 포켓몬 데이터
│   ├── PokemonData.cs          # 포켓몬 인스턴스 데이터
│   └── PokemonManager.cs       # 파티 관리
├── ScriptableObject/      # 데이터 에셋
│   ├── PokemonBase.cs          # 포켓몬 종족 템플릿
│   ├── MoveBase.cs             # 기술 템플릿
│   ├── PlayerData.cs           # 플레이어 설정
│   ├── WildOccurrenceData.cs   # 야생 포켓몬 출현 데이터
│   └── PokemonMovementBehaviour/  # 기술 동작 패턴
│       ├── AttackBehaviour.cs
│       ├── IdleBehaviour.cs
│       ├── RageBehaviour.cs
│       └── RunBehaviour.cs
├── Surface/               # 지면 시스템
│   ├── SurfaceBase.cs          # 지면 베이스
│   ├── TallGrass.cs            # 풀숲 오브젝트
│   ├── TallGrassSurface.cs     # 풀숲 지면
│   └── WildOccurrenceSurface.cs  # 야생 포켓몬 출현 지면
├── Sound/                 # 사운드
│   └── SoundManager.cs
├── Utility/               # 유틸리티
│   └── TypeChart.cs            # 타입 상성표
├── Enum/                  # 열거형
│   └── CustomEnum.cs
└── Singleton/             # 싱글톤 베이스
    └── Singleton.cs
```

## 핵심 시스템 설명

### 배틀 시스템

턴제 배틀 시스템으로 다음 기능을 포함합니다:

- **데미지 계산**: 공식 포켓몬 데미지 공식 적용
  ```
  기본 데미지 = (((2 × 레벨 / 5 + 2) × 위력 × (공격/방어)) / 50) + 2
  최종 데미지 = 기본 데미지 × 자속보정(1.5x) × 급소(1.5x) × 타입상성 × 랜덤(0.85~1.0)
  ```
- **타입 상성**: 18가지 타입, 78개 상성 관계 구현
- **스탯 변화**: -6 ~ +6 단계 스탯 변화 시스템
- **배틀 상태**: Start → PlayerTurn → EnemyTurn → Processing → Result → End

### 포켓몬 데이터 시스템

ScriptableObject 기반의 데이터 관리:

- **PokemonBase**: 종족값, 타입, 배울 수 있는 기술, 진화 정보
- **MoveBase**: 기술 위력, 명중률, PP, 효과, 애니메이션
- **PokemonData**: 개체별 레벨, 스탯, 현재 기술 관리

스탯 계산 공식:
```
스탯 = (2 × 종족값 + 개체값 + 노력치/4) × 레벨 / 100 + 5
```

### 플레이어 시스템

컴포넌트 분리 설계:

- **PlayerInputHandler**: New Input System 추상화
- **PlayerMovement**: CharacterController 기반 이동, 가속/감속
- **PlayerAnimationController**: 8방향 블렌드 트리 애니메이션
- **SurfaceChecker**: 물리 기반 지면 감지

### 인카운터 시스템

- **WildOccurrenceSurface**: 확률 기반 야생 포켓몬 조우
- **EncounterManager**: 화면 이펙트 (렌즈 왜곡, 색수차) 연출
- **TallGrass**: 풀숲 애니메이션 및 사운드

## 디자인 패턴

| 패턴 | 적용 위치 |
|------|-----------|
| **Singleton** | GameManager, PokemonManager, BattleSystem, SoundManager 등 |
| **Observer** | PlayerMovement.OnStepEvent, PokemonUnit.OnHit |
| **Strategy** | PokemonBehaviour (AttackBehaviour, IdleBehaviour 등) |
| **Facade** | BattleCanvas, TextArea |
| **State** | BattleState를 통한 배틀 상태 관리 |
| **Template Method** | SurfaceBase.ExecuteSurfaceEvent() |

## 게임 플로우

```
게임 시작
    ↓
필드 탐험 (PlayerController)
    ├─ 입력 처리 → 이동 → 애니메이션
    └─ 지면 감지 (SurfaceChecker)
        ↓
풀숲 진입 (TallGrassSurface)
    ↓
야생 포켓몬 조우 판정
    ↓
인카운터 연출 (EncounterManager)
    ↓
배틀 씬 로드
    ↓
배틀 시작 (BattleSystem)
    ├─ 플레이어 턴: 기술 선택
    ├─ 적 턴: 랜덤 기술 선택
    ├─ 스피드 비교 → 선공 결정
    ├─ 기술 실행 → 데미지 적용
    ├─ 기절 판정
    └─ 경험치 획득 → 레벨업
        ↓
배틀 종료 → 필드 복귀
```

## 구현된 기능 목록

- [x] 3D 필드 이동 (걷기/달리기)
- [x] 8방향 애니메이션 블렌딩
- [x] 야생 포켓몬 인카운터
- [x] 인카운터 화면 연출
- [x] 턴제 배틀 시스템
- [x] 데미지 계산 (타입 상성, 급소, 자속보정)
- [x] 스탯 변화 기술
- [x] HP/EXP 게이지 UI
- [x] 배틀 메시지 시스템
- [x] 경험치 및 레벨업
- [x] 레벨업 시 기술 학습
- [x] 포켓몬 파티 관리 (6마리)
- [x] BGM 및 효과음

## 향후 개발 예정

- [ ] 상태이상 (독, 마비, 화상 등)
- [ ] 트레이너 배틀
- [ ] 포켓몬 교체
- [ ] 아이템 사용
- [ ] 포켓몬 포획
- [ ] 진화 시스템
- [ ] NPC 및 스토리

## 실행 방법

1. Unity 2022 이상 버전으로 프로젝트 열기
2. `Assets/Scenes/Trip` 씬 로드
3. Play 버튼 클릭

## 조작법

| 키 | 동작 |
|----|------|
| WASD / 방향키 | 이동 |
| Shift | 달리기 |

## 참고 자료

- 포켓몬 데미지 계산 공식: [Bulbapedia](https://bulbapedia.bulbagarden.net/wiki/Damage)
- 타입 상성표: [포켓몬 위키](https://pokemon.fandom.com/wiki/Type)

## 라이선스

이 프로젝트는 포트폴리오 목적으로 제작되었습니다.
포켓몬스터는 Nintendo, Game Freak, Creatures Inc.의 등록 상표입니다.

---

*이 프로젝트는 학습 및 포트폴리오 목적으로 제작된 모작 게임입니다.*
