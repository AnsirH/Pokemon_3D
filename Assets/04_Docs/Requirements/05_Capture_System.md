# 포획 시스템 요구사항 명세서

## 개요
야생 포켓몬을 몬스터볼로 포획하는 시스템입니다. 포켓몬스터 게임의 핵심 재미 요소 중 하나로, 전략적 사고와 운의 요소가 결합된 매력적인 시스템입니다.

### 주요 목표
- 직관적이고 만족스러운 포획 시스템 구현
- 다양한 몬스터볼 타입과 포획 확률 시스템
- 몰입감 있는 포획 연출 및 애니메이션
- 포획된 포켓몬의 파티/박스 관리 시스템
- 포획 성공/실패에 따른 적절한 피드백 제공

### 관련 시스템과의 연관성
- **배틀 시스템**: 배틀 중 포획 시도
- **데이터 관리 시스템**: 포켓몬 데이터 및 몬스터볼 데이터
- **UI 시스템**: 포획 UI 및 연출
- **인벤토리 시스템**: 몬스터볼 소모 및 포켓몬 획득
- **파티 관리 시스템**: 포획된 포켓몬 파티 추가

---

## 요구사항 목록

### REQ-016: 몬스터볼 사용
- **설명**: 배틀 중 야생 포켓몬 포획 가능
- **입력**: 
  - 몬스터볼 선택 (가방에서)
  - 포켓몬 타겟 선택
  - 포획 확인
- **출력**: 
  - 몬스터볼 던지기 애니메이션
  - 포획 시도 결과
  - 몬스터볼 소모
- **사용 조건**: 
  - 야생 포켓몬과의 배틀 중
  - 몬스터볼 보유
  - 포켓몬이 기절하지 않음
- **제약사항**: 
  - 포켓몬이 기절 상태에서는 포획 불가
  - 몬스터볼이 부족하면 사용 불가
  - 이미 포획된 포켓몬은 포획 불가
- **수락 기준**: 
  - 포획 조건이 정확히 적용됨
  - 몬스터볼이 정확히 소모됨
  - 포획 시도가 올바르게 처리됨

### REQ-017: 포획 확률 계산
- **설명**: 포켓몬 체력, 상태이상, 몬스터볼 종류에 따른 성공 확률
- **입력**: 
  - 포켓몬 현재 HP
  - 포켓몬 최대 HP
  - 포켓몬 상태이상
  - 몬스터볼 타입
  - 포켓몬 레벨
- **출력**: 
  - 포획 성공 확률 (0-100%)
  - 포획 시도 결과
  - 확률 계산 로그
- **확률 공식**: 
  ```
  기본 확률 = 몬스터볼 기본 확률
  HP 보정 = (3 × 최대HP - 2 × 현재HP) / (3 × 최대HP)
  상태이상 보정 = 상태이상별 계수
  최종 확률 = 기본 확률 × HP 보정 × 상태이상 보정
  ```
- **상태이상 보정**: 
  - **수면/얼음**: 2.5배
  - **마비/독/화상**: 1.5배
  - **혼란**: 1.0배
  - **정상**: 1.0배
- **제약사항**: 
  - 최소 확률: 1%
  - 최대 확률: 100%
  - 확률 계산은 클라이언트에서 수행
- **수락 기준**: 
  - 확률 계산이 정확함
  - 설정된 확률대로 포획 성공/실패
  - 확률 로그가 정확히 기록됨

### REQ-018: 포획 연출
- **설명**: 몬스터볼 흔들림 → 성공/탈출 애니메이션
- **입력**: 
  - 포획 시도 결과
  - 몬스터볼 타입
  - 포켓몬 정보
- **출력**: 
  - 몬스터볼 던지기 애니메이션
  - 몬스터볼 흔들림 애니메이션
  - 성공/탈출 애니메이션
  - 사운드 효과
- **연출 단계**: 
  1. **던지기**: 몬스터볼을 포켓몬에게 던지기
  2. **흔들림**: 몬스터볼이 1-3번 흔들림
  3. **결과**: 성공 시 포켓몬 수집, 실패 시 포켓몬 탈출
  4. **피드백**: 성공/실패에 따른 시각적/청각적 피드백
- **제약사항**: 
  - 연출 중에는 다른 액션 불가
  - 연출 시간은 3-5초 이내
  - 연출 실패 시 안전한 복귀
- **수락 기준**: 
  - 연출이 자연스럽게 재생됨
  - 연출 중 버그가 없음
  - 연출 완료 후 정상적인 배틀 복귀

### REQ-019: 파티 관리
- **설명**: 포획 성공 시 플레이어 파티 또는 박스로 이동
- **입력**: 
  - 포획 성공 이벤트
  - 포켓몬 데이터
  - 현재 파티 상태
- **출력**: 
  - 포켓몬을 파티/박스에 추가
  - 파티/박스 UI 업데이트
  - 포획 성공 알림
- **파티 관리 규칙**: 
  - **파티 최대**: 6마리
  - **박스 최대**: 30마리 (박스당)
  - **자동 배치**: 파티가 가득 차면 박스로 자동 이동
  - **수동 배치**: 플레이어가 직접 선택 가능
- **제약사항**: 
  - 파티가 가득 찬 경우 박스로 이동
  - 박스도 가득 찬 경우 포획 실패
  - 중복 포켓몬 포획 가능
- **수락 기준**: 
  - 포획된 포켓몬이 정확히 추가됨
  - 파티/박스 UI가 실시간 업데이트됨
  - 파티 관리 규칙이 정확히 적용됨

---

## 상세 설계

### 클래스 구조

#### CaptureManager (포획 관리)
```csharp
public class CaptureManager : MonoBehaviour
{
    public static CaptureManager Instance { get; private set; }
    
    [Header("Capture Settings")]
    public float captureAnimationDuration = 3.0f;
    public int maxShakeAttempts = 3;
    
    [Header("UI References")]
    public CaptureUI captureUI;
    public ParticleSystem captureEffect;
    public AudioSource audioSource;
    
    private bool isCapturing;
    private PokemonSO targetPokemon;
    private ItemSO usedBall;
    
    public void AttemptCapture(PokemonSO pokemon, ItemSO ball)
    {
        if (!CanCapture(pokemon, ball)) return;
        
        targetPokemon = pokemon;
        usedBall = ball;
        isCapturing = true;
        
        StartCoroutine(CaptureSequence());
    }
    
    private IEnumerator CaptureSequence()
    {
        // 1. 몬스터볼 던지기
        yield return StartCoroutine(ThrowBall());
        
        // 2. 포획 확률 계산
        bool success = CalculateCaptureSuccess();
        
        // 3. 흔들림 애니메이션
        yield return StartCoroutine(ShakeBall(success));
        
        // 4. 결과 처리
        if (success)
        {
            yield return StartCoroutine(CaptureSuccess());
        }
        else
        {
            yield return StartCoroutine(CaptureFailure());
        }
        
        isCapturing = false;
    }
    
    private bool CalculateCaptureSuccess()
    {
        float captureRate = CaptureCalculator.CalculateCaptureRate(
            targetPokemon, usedBall);
        
        return Random.Range(0f, 1f) < captureRate;
    }
}
```

#### CaptureCalculator (포획 확률 계산)
```csharp
public static class CaptureCalculator
{
    public static float CalculateCaptureRate(PokemonSO pokemon, ItemSO ball)
    {
        // 기본 확률
        float baseRate = ball.CaptureRate;
        
        // HP 보정
        float hpModifier = CalculateHPModifier(pokemon);
        
        // 상태이상 보정
        float statusModifier = CalculateStatusModifier(pokemon);
        
        // 레벨 보정
        float levelModifier = CalculateLevelModifier(pokemon);
        
        // 최종 확률 계산
        float finalRate = baseRate * hpModifier * statusModifier * levelModifier;
        
        // 범위 제한
        return Mathf.Clamp(finalRate, 0.01f, 1.0f);
    }
    
    private static float CalculateHPModifier(PokemonSO pokemon)
    {
        float maxHP = pokemon.MaxHP;
        float currentHP = pokemon.CurrentHP;
        
        return (3f * maxHP - 2f * currentHP) / (3f * maxHP);
    }
    
    private static float CalculateStatusModifier(PokemonSO pokemon)
    {
        switch (pokemon.StatusEffect)
        {
            case StatusEffect.Sleep:
            case StatusEffect.Frozen:
                return 2.5f;
            case StatusEffect.Paralyzed:
            case StatusEffect.Poisoned:
            case StatusEffect.Burned:
                return 1.5f;
            case StatusEffect.Confusion:
                return 1.0f;
            default:
                return 1.0f;
        }
    }
    
    private static float CalculateLevelModifier(PokemonSO pokemon)
    {
        // 레벨이 높을수록 포획 어려움
        float levelFactor = 1.0f - (pokemon.Level * 0.01f);
        return Mathf.Clamp(levelFactor, 0.5f, 1.0f);
    }
}
```

#### CaptureUI (포획 UI)
```csharp
public class CaptureUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject capturePanel;
    public Image ballImage;
    public Text captureText;
    public Slider captureProgress;
    public Button captureButton;
    
    [Header("Animation")]
    public Animator ballAnimator;
    public ParticleSystem successEffect;
    public ParticleSystem failureEffect;
    
    public void ShowCaptureUI()
    {
        capturePanel.SetActive(true);
        captureButton.interactable = true;
    }
    
    public void HideCaptureUI()
    {
        capturePanel.SetActive(false);
    }
    
    public void UpdateCaptureProgress(float progress)
    {
        captureProgress.value = progress;
    }
    
    public void ShowCaptureResult(bool success)
    {
        if (success)
        {
            captureText.text = "포획 성공!";
            successEffect.Play();
        }
        else
        {
            captureText.text = "포켓몬이 탈출했다!";
            failureEffect.Play();
        }
    }
}
```

#### PartyManager (파티 관리)
```csharp
public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }
    
    [Header("Party Settings")]
    public int maxPartySize = 6;
    public int maxBoxSize = 30;
    
    [Header("Party Data")]
    public List<PokemonSO> playerParty;
    public List<List<PokemonSO>> pokemonBoxes;
    
    public bool AddPokemonToParty(PokemonSO pokemon)
    {
        if (playerParty.Count < maxPartySize)
        {
            playerParty.Add(pokemon);
            UpdatePartyUI();
            return true;
        }
        else
        {
            // 파티가 가득 찬 경우 박스로 이동
            return AddPokemonToBox(pokemon);
        }
    }
    
    public bool AddPokemonToBox(PokemonSO pokemon)
    {
        // 빈 박스 찾기
        for (int i = 0; i < pokemonBoxes.Count; i++)
        {
            if (pokemonBoxes[i].Count < maxBoxSize)
            {
                pokemonBoxes[i].Add(pokemon);
                UpdateBoxUI();
                return true;
            }
        }
        
        // 모든 박스가 가득 찬 경우
        return false;
    }
    
    public void RemovePokemonFromParty(PokemonSO pokemon)
    {
        playerParty.Remove(pokemon);
        UpdatePartyUI();
    }
    
    public void SwitchPokemon(int fromIndex, int toIndex, bool isFromParty)
    {
        // 포켓몬 교체 로직
        if (isFromParty)
        {
            // 파티에서 박스로
            PokemonSO pokemon = playerParty[fromIndex];
            playerParty.RemoveAt(fromIndex);
            AddPokemonToBox(pokemon);
        }
        else
        {
            // 박스에서 파티로
            PokemonSO pokemon = pokemonBoxes[fromIndex / maxBoxSize][fromIndex % maxBoxSize];
            pokemonBoxes[fromIndex / maxBoxSize].RemoveAt(fromIndex % maxBoxSize);
            AddPokemonToParty(pokemon);
        }
    }
}
```

### 데이터 구조

#### CaptureData (포획 데이터)
```csharp
[System.Serializable]
public class CaptureData
{
    public PokemonSO pokemon;
    public ItemSO ball;
    public float captureRate;
    public bool success;
    public DateTime captureTime;
    public string location;
}
```

#### BallType (몬스터볼 타입)
```csharp
public enum BallType
{
    PokeBall,       // 몬스터볼 (기본)
    GreatBall,      // 슈퍼볼 (1.5배)
    UltraBall,      // 하이퍼볼 (2배)
    MasterBall,     // 마스터볼 (100%)
    QuickBall,      // 퀵볼 (첫 턴 4배)
    TimerBall,      // 타이머볼 (턴 수에 따라)
    NetBall,        // 넷볼 (벌레/물 타입 3배)
    DuskBall,       // 다크볼 (밤/동굴 3.5배)
    RepeatBall,     // 리피트볼 (이미 잡은 포켓몬 3배)
    LuxuryBall      // 럭셔리볼 (친밀도 증가)
}
```

#### CaptureResult (포획 결과)
```csharp
[System.Serializable]
public class CaptureResult
{
    public bool success;
    public PokemonSO capturedPokemon;
    public ItemSO usedBall;
    public float actualCaptureRate;
    public int shakeCount;
    public string resultMessage;
}
```

### 이벤트 흐름

#### 포획 시도 플로우
1. **포획 시도**: 플레이어가 몬스터볼 사용
2. **조건 검사**: 포획 가능 여부 확인
3. **확률 계산**: 포획 성공 확률 계산
4. **연출 시작**: 몬스터볼 던지기 애니메이션
5. **흔들림**: 몬스터볼 흔들림 애니메이션
6. **결과 판정**: 성공/실패 결정
7. **결과 처리**: 성공 시 파티 추가, 실패 시 탈출

#### 파티 관리 플로우
1. **포획 성공**: 포켓몬 포획 완료
2. **파티 확인**: 현재 파티 상태 확인
3. **자동 배치**: 파티 공간이 있으면 파티에 추가
4. **박스 이동**: 파티가 가득 찬 경우 박스로 이동
5. **UI 업데이트**: 파티/박스 UI 업데이트
6. **알림 표시**: 포획 성공 알림

---

## 구현 가이드

### 기술적 고려사항

#### 포획 확률 계산
- **공식**: 포켓몬스터 공식 포획 확률 사용
- **보정 요소**: HP, 상태이상, 레벨, 몬스터볼 타입
- **랜덤 요소**: 시드 기반 랜덤 값 생성

#### 애니메이션 시스템
- **코루틴 사용**: 순차적 애니메이션 처리
- **이벤트 기반**: 애니메이션 완료 시 이벤트 발생
- **중단 처리**: 애니메이션 중단 시 안전한 복귀

#### 파티 관리
- **데이터 구조**: 리스트 기반 파티/박스 관리
- **UI 동기화**: 데이터 변경 시 UI 실시간 업데이트
- **저장/로드**: 파티 데이터 영구 저장

### 성능 요구사항

#### 프레임레이트
- **목표**: 60fps 유지
- **최적화**: 
  - 불필요한 Update 호출 최소화
  - 이벤트 기반 처리
  - 애니메이션 최적화

#### 메모리 사용량
- **목표**: 50MB 이하
- **최적화**: 
  - 포켓몬 데이터 캐싱
  - UI 오브젝트 풀링
  - 불필요한 컴포넌트 제거

### 테스트 케이스

#### 기능 테스트
1. **포획 시도 테스트**
   - 포획 조건이 정확히 적용되는지 확인
   - 몬스터볼이 정확히 소모되는지 확인
   - 포획 시도가 올바르게 처리되는지 확인

2. **확률 계산 테스트**
   - 확률 계산이 정확한지 확인
   - 설정된 확률대로 포획이 성공/실패하는지 확인
   - 확률 로그가 정확히 기록되는지 확인

3. **연출 테스트**
   - 연출이 자연스럽게 재생되는지 확인
   - 연출 중 버그가 없는지 확인
   - 연출 완료 후 정상적인 배틀 복귀가 되는지 확인

4. **파티 관리 테스트**
   - 포획된 포켓몬이 정확히 추가되는지 확인
   - 파티/박스 UI가 실시간 업데이트되는지 확인
   - 파티 관리 규칙이 정확히 적용되는지 확인

#### 성능 테스트
1. **프레임레이트 테스트**
   - 포획 연출 중 60fps 유지 여부 확인
   - 프레임 드롭 없음 확인

2. **메모리 테스트**
   - 메모리 누수 없음 확인
   - 가비지 컬렉션 최소화 확인

#### 통합 테스트
1. **다른 시스템과의 연동**
   - 배틀 시스템과의 연동
   - 인벤토리 시스템과의 연동
   - UI 시스템과의 연동

---

## 참고사항

### 관련 파일
- `CaptureManager.cs`: 포획 관리
- `CaptureCalculator.cs`: 포획 확률 계산
- `CaptureUI.cs`: 포획 UI
- `PartyManager.cs`: 파티 관리
- `CaptureData.cs`: 포획 데이터
- `CaptureResult.cs`: 포획 결과

### 의존성
- **배틀 시스템**: 배틀 중 포획
- **데이터 관리 시스템**: 포켓몬, 몬스터볼 데이터
- **UI 시스템**: 포획 UI
- **인벤토리 시스템**: 몬스터볼 소모
- **파티 관리 시스템**: 포획된 포켓몬 추가

### 향후 확장 계획
1. **특별 포획**: 레전더리 포켓몬 포획
2. **포획 도전**: 연속 포획 도전
3. **포획 통계**: 포획 성공률 통계
4. **포획 이벤트**: 특별 포획 이벤트
5. **멀티플레이어**: 다른 플레이어와의 포획 경쟁

### 버그 대응
- **포획 중복**: 포획 상태 추적 강화
- **확률 오류**: 확률 계산 공식 검증
- **연출 버그**: 애니메이션 상태 관리 강화
- **파티 오류**: 파티 데이터 동기화 강화

---

**문서 끝**
