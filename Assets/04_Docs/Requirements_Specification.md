# 3D 포켓몬스터 리메이크 게임 - 요구사항 명세서

## 문서 정보
- **문서명**: 3D 포켓몬스터 리메이크 게임 요구사항 명세서
- **버전**: 1.0
- **작성일**: 2024-12-19
- **작성자**: 개발팀
- **프로젝트**: Pokemon_3D

## 목차
1. [프로젝트 개요](#1-프로젝트-개요)
2. [현재 구현 상태](#2-현재-구현-상태)
3. [기능 요구사항](#3-기능-요구사항)
4. [데이터 구조 명세](#4-데이터-구조-명세)
5. [기술적 아키텍처](#5-기술적-아키텍처)
6. [개발 우선순위](#6-개발-우선순위)
7. [테스트 및 검증](#7-테스트-및-검증)

---

## 1. 프로젝트 개요

### 1.1 프로젝트 목표
기존 2D 포켓몬스터 게임의 핵심 재미 요소(탐험, 포획, 배틀)를 유지하면서, 3D 환경으로 리메이크하여 몰입감과 현대적 게임 경험을 제공하는 Unity 기반 RPG 게임 개발

### 1.2 기술 스택
- **엔진**: Unity 2022.3 LTS 이상
- **카메라**: Unity Cinemachine
- **물리**: Rigidbody + Collider 기반 충돌 처리
- **데이터 관리**: ScriptableObject 기반 데이터베이스
- **UI**: Unity UGUI
- **입력**: Unity Input System
- **그래픽**: 셀 셰이딩 기반 카툰풍 3D

### 1.3 플랫폼
- **주 플랫폼**: PC (Windows)
- **향후 확장**: 콘솔, 모바일 플랫폼 고려

---

## 2. 현재 구현 상태

### 2.1 구현 완료 기능
- ✅ **플레이어 8방향 이동**: WASD 키 입력으로 8방향 이동 구현
- ✅ **이동 애니메이션 시스템**: 이동 상태에 따른 애니메이션 전환
- ✅ **카메라 회전 및 이동**: 탑다운 뷰 카메라 시스템

### 2.2 미구현 기능
- ❌ **충돌 처리**: 지형, 건물, 나무와의 충돌 시 이동 제한
- ❌ **풀숲 조우 시스템**: 야생 포켓몬과의 조우 메커니즘
- ❌ **배틀 시스템**: 턴제 전투 로직 및 UI
- ❌ **NPC 배틀 시스템**: NPC와의 상호작용 및 배틀
- ❌ **포획 시스템**: 몬스터볼을 통한 포켓몬 포획
- ❌ **데이터 관리**: ScriptableObject 기반 데이터베이스
- ❌ **상호작용 키**: Space 키를 통한 상호작용

---

## 3. 기능 요구사항

각 기능별 상세 요구사항은 별도 문서로 분리되어 있습니다:

### 3.1 플레이어 이동 시스템
- **[01_Player_Movement_System.md](Requirements/01_Player_Movement_System.md)**
- REQ-001: 8방향 이동
- REQ-002: 속도 전환 (걷기/달리기)
- REQ-003: 충돌 처리
- REQ-004: 카메라 시스템

### 3.2 풀숲 조우 시스템
- **[02_Tall_Grass_Encounter_System.md](Requirements/02_Tall_Grass_Encounter_System.md)**
- REQ-005: 풀숲 진입 감지
- REQ-006: 조우 확률 계산
- REQ-007: 배틀 씬 전환

### 3.3 배틀 시스템
- **[03_Battle_System.md](Requirements/03_Battle_System.md)**
- REQ-008: 턴제 전투
- REQ-009: 기술 시스템
- REQ-010: 상태 관리
- REQ-011: 배틀 UI

### 3.4 NPC 배틀 시스템
- **[04_NPC_Battle_System.md](Requirements/04_NPC_Battle_System.md)**
- REQ-012: NPC 상호작용
- REQ-013: 대화 시스템
- REQ-014: 보상 시스템
- REQ-015: 고정 파티

### 3.5 포획 시스템
- **[05_Capture_System.md](Requirements/05_Capture_System.md)**
- REQ-016: 몬스터볼 사용
- REQ-017: 포획 확률 계산
- REQ-018: 포획 연출
- REQ-019: 파티 관리

### 3.6 데이터 관리 시스템
- **[06_Data_Management_System.md](Requirements/06_Data_Management_System.md)**
- REQ-020: ScriptableObject 데이터베이스
- REQ-021: 데이터 검색 시스템
- REQ-022: 데이터 저장/로드
- REQ-023: 에디터 툴

### 3.7 UI 시스템
- **[07_UI_System.md](Requirements/07_UI_System.md)**
- REQ-024: 메인 UI (HUD)
- REQ-025: 배틀 UI
- REQ-026: 대화 UI
- REQ-027: 인벤토리 UI
- REQ-028: 파티 관리 UI
- REQ-029: 설정 UI

---

## 4. 데이터 구조 명세

상세한 데이터 구조는 **[06_Data_Management_System.md](Requirements/06_Data_Management_System.md)** 문서를 참조하세요.

### 4.1 주요 데이터 타입
- **PokemonSO**: 포켓몬 정보 (ID, 이름, 스탯, 기술, 진화 등)
- **MoveSO**: 기술 정보 (이름, 위력, 명중률, PP, 타입 등)
- **ItemSO**: 아이템 정보 (이름, 효과, 가격, 아이콘 등)
- **NPCSO**: NPC 정보 (이름, 대사, 파티, 보상 등)
- **TypeSO**: 타입 상성 정보

### 4.2 저장 데이터 구조
- **PlayerSaveData**: 플레이어 진행 데이터
- **PokemonSaveData**: 포켓몬 저장 데이터
- **ItemSaveData**: 아이템 저장 데이터
- **GameProgressData**: 게임 진행 데이터
- **SettingsData**: 설정 데이터

---

## 5. 기술적 아키텍처

상세한 아키텍처 설계는 각 기능별 문서를 참조하세요:

### 5.1 권장 디렉토리 구조
```
Assets/
├── 01_Scenes/          # 게임 씬 파일
├── 02_Scripts/         # C# 스크립트
│   ├── Player/         # 플레이어 관련 스크립트
│   ├── Movement/       # 이동 시스템
│   ├── Camera/         # 카메라 시스템
│   ├── Environment/    # 환경 시스템 (풀숲, 조우)
│   ├── Battle/         # 배틀 시스템
│   ├── NPC/           # NPC 시스템
│   ├── Data/          # 데이터 관리
│   ├── UI/            # UI 시스템
│   └── Systems/       # 공통 시스템
├── 03_Animations/      # 애니메이션 파일
├── 04_Docs/           # 문서
│   └── Requirements/  # 기능별 요구사항
├── Imported_Assets/    # 외부 에셋
└── Terrain/           # 지형 데이터
```

### 5.2 핵심 시스템 아키텍처
- **이벤트 기반 시스템**: 시스템 간 느슨한 결합
- **MVC 패턴**: UI와 로직 분리
- **ScriptableObject**: 데이터 중심 설계
- **싱글톤 패턴**: 매니저 클래스 관리

### 5.3 시스템 간 연동
각 시스템은 이벤트를 통해 통신하며, 데이터는 ScriptableObject를 통해 공유됩니다.

---

## 6. 개발 우선순위

### Phase 1: 기본 시스템 (1-2주)
1. **충돌 처리 구현** - [01_Player_Movement_System.md](Requirements/01_Player_Movement_System.md)
2. **상호작용 키 연결** - [01_Player_Movement_System.md](Requirements/01_Player_Movement_System.md)

### Phase 2: 풀숲 시스템 (2-3주)
1. **EncounterZone 구현** - [02_Tall_Grass_Encounter_System.md](Requirements/02_Tall_Grass_Encounter_System.md)
2. **조우 확률 시스템** - [02_Tall_Grass_Encounter_System.md](Requirements/02_Tall_Grass_Encounter_System.md)

### Phase 3: 배틀 시스템 (3-4주)
1. **배틀 파이프라인 프로토타입** - [03_Battle_System.md](Requirements/03_Battle_System.md)
2. **배틀 UI 구현** - [07_UI_System.md](Requirements/07_UI_System.md)

### Phase 4: 포획 시스템 (2-3주)
1. **포획 확률 계산** - [05_Capture_System.md](Requirements/05_Capture_System.md)
2. **포획 연출** - [05_Capture_System.md](Requirements/05_Capture_System.md)

### Phase 5: NPC 시스템 (2-3주)
1. **NPC 상호작용** - [04_NPC_Battle_System.md](Requirements/04_NPC_Battle_System.md)
2. **보상 시스템** - [04_NPC_Battle_System.md](Requirements/04_NPC_Battle_System.md)

### Phase 6: 데이터 정리 (2-3주)
1. **ScriptableObject 파이프라인** - [06_Data_Management_System.md](Requirements/06_Data_Management_System.md)
2. **테스트 및 튜닝** - 모든 시스템 통합 테스트

---

## 7. 테스트 및 검증

### 7.1 기능별 테스트 문서
각 기능별 상세 테스트 케이스는 해당 문서를 참조하세요:

- **[01_Player_Movement_System.md](Requirements/01_Player_Movement_System.md)** - 이동 시스템 테스트
- **[02_Tall_Grass_Encounter_System.md](Requirements/02_Tall_Grass_Encounter_System.md)** - 풀숲 조우 테스트
- **[03_Battle_System.md](Requirements/03_Battle_System.md)** - 배틀 시스템 테스트
- **[04_NPC_Battle_System.md](Requirements/04_NPC_Battle_System.md)** - NPC 배틀 테스트
- **[05_Capture_System.md](Requirements/05_Capture_System.md)** - 포획 시스템 테스트
- **[06_Data_Management_System.md](Requirements/06_Data_Management_System.md)** - 데이터 관리 테스트
- **[07_UI_System.md](Requirements/07_UI_System.md)** - UI 시스템 테스트

### 7.2 통합 테스트
- [ ] 모든 시스템 간 연동이 정상 작동하는가
- [ ] 데이터 일관성이 유지되는가
- [ ] 성능 목표가 달성되는가 (60fps, 메모리 사용량)

### 7.3 수락 기준
각 기능별 수락 기준은 해당 요구사항 문서의 "수락 기준" 섹션을 참조하세요.

---

## 8. 네이밍 컨벤션

### 8.1 파일 및 클래스 네이밍
- **클래스**: PascalCase (예: `PlayerController`, `BattleManager`)
- **인터페이스**: I + PascalCase (예: `IMoveable`, `IBattleAction`)
- **ScriptableObject**: PascalCase + SO (예: `PokemonSO`, `MoveSO`)
- **파일명**: 클래스명과 동일

### 8.2 변수 및 메서드 네이밍
- **public 변수**: PascalCase (예: `CurrentHP`, `IsMoving`)
- **private 변수**: camelCase (예: `currentSpeed`, `isGrounded`)
- **메서드**: PascalCase (예: `MovePlayer()`, `CalculateDamage()`)
- **이벤트**: On + PascalCase (예: `OnPlayerMove`, `OnBattleStart`)

### 8.3 상수 네이밍
- **상수**: UPPER_CASE (예: `MAX_PARTY_SIZE`, `DEFAULT_MOVE_SPEED`)

---

## 9. 예제 코드 스케치

상세한 코드 예제는 각 기능별 문서를 참조하세요:

### 9.1 전투 턴 처리
**[03_Battle_System.md](Requirements/03_Battle_System.md)** - 배틀 시스템 코드 예제

### 9.2 포획 확률 계산
**[05_Capture_System.md](Requirements/05_Capture_System.md)** - 포획 시스템 코드 예제

### 9.3 기타 코드 예제
각 기능별 문서의 "예제 코드 스케치" 섹션을 참조하세요.

---

## 10. 결론

이 요구사항 명세서는 3D 포켓몬스터 리메이크 게임의 핵심 기능들을 상세히 정의하고 있습니다. 각 기능은 명확한 입력/출력과 제약사항을 가지며, 구현 가능한 수준으로 구체화되어 있습니다.

개발 과정에서 이 명세서를 기반으로 일정을 관리하고, 테스트 케이스를 통해 품질을 보장할 수 있습니다. 또한 향후 기능 확장이나 플랫폼 이식 시에도 이 명세서를 참고하여 일관성 있는 개발을 진행할 수 있습니다.

---

**문서 끝**
