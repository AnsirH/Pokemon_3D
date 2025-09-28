# 풀숲 조우 시스템 요구사항 명세서

## 개요
플레이어가 풀숲 영역에 진입할 때 야생 포켓몬과의 조우를 처리하는 시스템입니다. 전통적인 포켓몬스터 게임의 핵심 메커니즘으로, 탐험의 재미와 긴장감을 제공합니다.

### 주요 목표
- 풀숲 진입 감지 및 시각적 피드백
- 확률 기반 야생 포켓몬 조우 시스템
- 조우 시 배틀 씬으로의 자연스러운 전환
- 다양한 풀숲 타입별 조우 확률 차별화
- 몰입감 있는 풀 흔들림 애니메이션

### 관련 시스템과의 연관성
- **플레이어 이동 시스템**: 풀숲 진입 감지
- **배틀 시스템**: 조우 시 배틀 전환
- **데이터 관리 시스템**: 포켓몬 데이터 참조
- **UI 시스템**: 조우 알림 및 전환 효과

---

## 요구사항 목록

### REQ-005: 풀숲 진입 감지
- **설명**: 플레이어가 풀숲 영역에 진입할 때 감지
- **입력**: 
  - 플레이어 위치 (Transform)
  - 풀숲 영역 Collider (Trigger)
  - 플레이어 이동 상태
- **출력**: 
  - 진입 이벤트 발생
  - 풀숲 영역 정보 전달
  - 시각적 효과 트리거
- **제약사항**: 
  - 플레이어가 이동 중일 때만 감지
  - 중복 진입 방지
  - 진입 후 일정 시간 대기 (쿨다운)
- **수락 기준**: 
  - 풀숲 진입 시 정확히 감지
  - 중복 이벤트 발생하지 않음
  - 진입 시 즉시 시각적 효과 재생

### REQ-006: 조우 확률 계산
- **설명**: 풀숲 진입 시 야생 포켓몬과의 조우 확률 계산
- **입력**: 
  - 풀숲 영역 타입 (일반, 희귀, 특별)
  - 플레이어 레벨
  - 시간대 (낮/밤)
  - 날씨 상태 (선택사항)
- **출력**: 
  - 조우 성공/실패 여부
  - 조우된 포켓몬 정보
  - 조우 확률 수치
- **확률 공식**: 
  ```
  기본 확률 = 풀숲 타입별 기본값
  레벨 보정 = 플레이어 레벨 × 0.01
  시간 보정 = 시간대별 계수
  최종 확률 = (기본 확률 + 레벨 보정) × 시간 보정
  ```
- **제약사항**: 
  - 최소 확률: 5%
  - 최대 확률: 95%
- **수락 기준**: 
  - 설정된 확률대로 조우 발생
  - 확률 계산이 정확함
  - 조우 실패 시 자연스러운 필드 복귀

### REQ-007: 배틀 씬 전환
- **설명**: 조우 성공 시 배틀 씬으로 전환
- **입력**: 
  - 조우 성공 이벤트
  - 조우된 포켓몬 데이터
  - 플레이어 파티 정보
- **출력**: 
  - 배틀 씬 로드
  - 배틀 데이터 전달
  - 전환 애니메이션
- **전환 방식**: 
  - **씬 전환**: 완전한 씬 교체 (권장)
  - **오버레이**: 현재 씬 위에 배틀 UI 오버레이
- **제약사항**: 
  - 전환 중 플레이어 입력 차단
  - 전환 실패 시 필드로 복귀
  - 전환 시간 최소화 (3초 이내)
- **수락 기준**: 
  - 조우 시 즉시 배틀 전환
  - 전환 중 버그 없음
  - 전환 실패 시 안전한 복귀

---

## 상세 설계

### 클래스 구조

#### TallGrassController (풀숲 관리)
```csharp
public class TallGrassController : MonoBehaviour
{
    [Header("Grass Settings")]
    public GrassType grassType;
    public float encounterRate = 0.3f;
    public float cooldownTime = 2.0f;
    public List<PokemonEncounterData> possiblePokemon;
    
    [Header("Visual Effects")]
    public ParticleSystem grassShakeEffect;
    public AudioClip grassSound;
    
    private bool isOnCooldown;
    private float lastEncounterTime;
    
    public void OnPlayerEnter(PlayerController player)
    {
        // 플레이어 진입 처리
    }
    
    public bool TryEncounter()
    {
        // 조우 시도
    }
    
    private void PlayGrassShakeAnimation()
    {
        // 풀 흔들림 애니메이션
    }
}
```

#### EncounterZone (조우 영역)
```csharp
public class EncounterZone : MonoBehaviour
{
    [Header("Zone Settings")]
    public string zoneName;
    public GrassType grassType;
    public int minLevel = 1;
    public int maxLevel = 10;
    public float baseEncounterRate = 0.3f;
    
    [Header("Pokemon Data")]
    public List<PokemonEncounterData> encounterTable;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어 진입 처리
        }
    }
    
    public PokemonSO GetRandomPokemon()
    {
        // 랜덤 포켓몬 선택
    }
}
```

#### EncounterCalculator (확률 계산)
```csharp
public class EncounterCalculator : MonoBehaviour
{
    public static bool CalculateEncounter(GrassType grassType, int playerLevel, TimeOfDay timeOfDay)
    {
        float baseRate = GetBaseEncounterRate(grassType);
        float levelModifier = playerLevel * 0.01f;
        float timeModifier = GetTimeModifier(timeOfDay);
        
        float finalRate = (baseRate + levelModifier) * timeModifier;
        finalRate = Mathf.Clamp(finalRate, 0.05f, 0.95f);
        
        return UnityEngine.Random.Range(0f, 1f) < finalRate;
    }
    
    private static float GetBaseEncounterRate(GrassType grassType)
    {
        switch (grassType)
        {
            case GrassType.Normal: return 0.3f;
            case GrassType.Rare: return 0.15f;
            case GrassType.Special: return 0.5f;
            default: return 0.3f;
        }
    }
    
    private static float GetTimeModifier(TimeOfDay timeOfDay)
    {
        switch (timeOfDay)
        {
            case TimeOfDay.Morning: return 1.0f;
            case TimeOfDay.Day: return 1.2f;
            case TimeOfDay.Evening: return 0.8f;
            case TimeOfDay.Night: return 0.6f;
            default: return 1.0f;
        }
    }
}
```

#### EncounterManager (조우 관리)
```csharp
public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance { get; private set; }
    
    [Header("Encounter Settings")]
    public float globalCooldown = 1.0f;
    public bool enableEncounter = true;
    
    private float lastEncounterTime;
    private bool isInEncounter;
    
    public void TryEncounter(EncounterZone zone, PlayerController player)
    {
        if (!CanEncounter()) return;
        
        if (EncounterCalculator.CalculateEncounter(zone.grassType, player.Level, GetCurrentTimeOfDay()))
        {
            StartEncounter(zone, player);
        }
    }
    
    private void StartEncounter(EncounterZone zone, PlayerController player)
    {
        // 조우 시작
        isInEncounter = true;
        lastEncounterTime = Time.time;
        
        // 배틀 씬으로 전환
        SceneManager.LoadScene("BattleScene");
    }
}
```

### 데이터 구조

#### PokemonEncounterData (조우 데이터)
```csharp
[System.Serializable]
public class PokemonEncounterData
{
    public PokemonSO pokemon;
    public int minLevel;
    public int maxLevel;
    public float encounterWeight;
    public bool isShiny;
    
    public PokemonSO GetPokemon()
    {
        // 레벨 범위 내에서 랜덤 포켓몬 반환
    }
}
```

#### GrassType (풀숲 타입)
```csharp
public enum GrassType
{
    Normal,     // 일반 풀숲 (30% 확률)
    Rare,       // 희귀 풀숲 (15% 확률)
    Special,    // 특별 풀숲 (50% 확률)
    Cave,       // 동굴 (20% 확률)
    Water       // 물가 (25% 확률)
}
```

#### TimeOfDay (시간대)
```csharp
public enum TimeOfDay
{
    Morning,    // 아침 (06:00-12:00)
    Day,        // 낮 (12:00-18:00)
    Evening,    // 저녁 (18:00-22:00)
    Night       // 밤 (22:00-06:00)
}
```

### 이벤트 흐름

#### 조우 처리 플로우
1. **플레이어 진입**: `EncounterZone`의 `OnTriggerEnter` 호출
2. **진입 검증**: 플레이어 태그 확인, 쿨다운 체크
3. **시각적 효과**: 풀 흔들림 애니메이션 재생
4. **확률 계산**: `EncounterCalculator`가 조우 확률 계산
5. **조우 결정**: 랜덤 값과 확률 비교
6. **배틀 전환**: 조우 성공 시 배틀 씬으로 전환
7. **필드 복귀**: 조우 실패 시 필드로 복귀

#### 풀숲 타입별 처리
1. **일반 풀숲**: 기본 확률 30%, 일반 포켓몬
2. **희귀 풀숲**: 낮은 확률 15%, 희귀 포켓몬
3. **특별 풀숲**: 높은 확률 50%, 특별 포켓몬
4. **동굴**: 중간 확률 20%, 동굴 포켓몬
5. **물가**: 중간 확률 25%, 물 포켓몬

---

## 구현 가이드

### 기술적 고려사항

#### 충돌 감지
- **Trigger Collider 사용**: 물리 충돌 없이 진입 감지
- **Layer 설정**: 풀숲 전용 레이어 사용
- **태그 시스템**: 플레이어 태그로 정확한 감지

#### 확률 계산
- **시드 기반**: 재현 가능한 랜덤 값 생성
- **가중치 시스템**: 포켓몬별 등장 확률 차등화
- **서버 검증**: 온라인 시 서버에서 확률 재검증

#### 씬 전환
- **비동기 로딩**: `SceneManager.LoadSceneAsync` 사용
- **로딩 화면**: 전환 중 로딩 UI 표시
- **데이터 전달**: ScriptableObject를 통한 데이터 전달

### 성능 요구사항

#### 프레임레이트
- **목표**: 60fps 유지
- **최적화**: 
  - 불필요한 Update 호출 최소화
  - 이벤트 기반 처리
  - 오브젝트 풀링 활용

#### 메모리 사용량
- **목표**: 50MB 이하
- **최적화**: 
  - 포켓몬 데이터 캐싱
  - 불필요한 컴포넌트 제거
  - 텍스처 압축

### 테스트 케이스

#### 기능 테스트
1. **진입 감지 테스트**
   - 풀숲 진입 시 정확히 감지되는지 확인
   - 중복 진입 방지가 작동하는지 확인
   - 쿨다운 시간이 정확한지 확인

2. **확률 계산 테스트**
   - 설정된 확률대로 조우가 발생하는지 확인
   - 풀숲 타입별 확률 차이가 정확한지 확인
   - 시간대별 보정이 적용되는지 확인

3. **배틀 전환 테스트**
   - 조우 성공 시 배틀 씬으로 전환되는지 확인
   - 전환 중 버그가 없는지 확인
   - 전환 실패 시 안전한 복귀가 되는지 확인

4. **시각적 효과 테스트**
   - 풀 흔들림 애니메이션이 재생되는지 확인
   - 사운드 효과가 정상 작동하는지 확인
   - 파티클 효과가 올바르게 표시되는지 확인

#### 성능 테스트
1. **프레임레이트 테스트**
   - 풀숲 진입 시 프레임 드롭 없음 확인
   - 60fps 유지 여부 확인

2. **메모리 테스트**
   - 메모리 누수 없음 확인
   - 가비지 컬렉션 최소화 확인

#### 통합 테스트
1. **다른 시스템과의 연동**
   - 플레이어 이동 시스템과의 연동
   - 배틀 시스템과의 연동
   - UI 시스템과의 연동

---

## 참고사항

### 관련 파일
- `TallGrassController.cs`: 풀숲 관리
- `EncounterZone.cs`: 조우 영역
- `EncounterCalculator.cs`: 확률 계산
- `EncounterManager.cs`: 조우 관리
- `PokemonEncounterData.cs`: 조우 데이터

### 의존성
- **플레이어 이동 시스템**: 진입 감지
- **배틀 시스템**: 조우 시 전환
- **데이터 관리 시스템**: 포켓몬 데이터
- **UI 시스템**: 전환 효과

### 향후 확장 계획
1. **날씨 시스템**: 날씨에 따른 조우 확률 변화
2. **계절 시스템**: 계절별 포켓몬 등장
3. **특별 이벤트**: 이벤트 기간 중 특별 포켓몬 등장
4. **멀티플레이어**: 다른 플레이어와의 조우 공유
5. **AR 기능**: 실제 환경과 연동된 조우

### 버그 대응
- **중복 조우**: 쿨다운 시스템 강화
- **확률 오류**: 시드 기반 랜덤 사용
- **전환 실패**: 안전한 복귀 메커니즘
- **성능 이슈**: 이벤트 기반 처리로 최적화

---

**문서 끝**
