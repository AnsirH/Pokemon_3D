# 배틀 시스템 요구사항 명세서

## 개요
포켓몬스터 게임의 핵심인 턴제 배틀 시스템을 구현합니다. 플레이어와 상대 포켓몬 간의 전략적 전투를 통해 게임의 재미와 깊이를 제공합니다.

### 주요 목표
- 전통적인 포켓몬스터 턴제 배틀 시스템 구현
- 직관적이고 반응성 있는 배틀 UI 제공
- 다양한 기술과 상태이상 시스템 구현
- 몰입감 있는 3D 배틀 환경 구성
- 전략적 깊이와 밸런스 제공

### 관련 시스템과의 연관성
- **풀숲 조우 시스템**: 야생 포켓몬과의 배틀
- **NPC 배틀 시스템**: NPC와의 배틀
- **포획 시스템**: 배틀 중 포켓몬 포획
- **데이터 관리 시스템**: 포켓몬, 기술 데이터 참조
- **UI 시스템**: 배틀 UI 및 메뉴

---

## 요구사항 목록

### REQ-008: 턴제 전투
- **설명**: 전통적인 포켓몬스터 턴제 배틀 시스템
- **입력**: 
  - 플레이어 액션 선택 (싸운다, 가방, 포켓몬, 도망간다)
  - 기술 선택 (싸운다 선택 시)
  - 아이템 사용 (가방 선택 시)
  - 포켓몬 교체 (포켓몬 선택 시)
- **출력**: 
  - 턴 진행 및 결과
  - 데미지 계산 및 적용
  - 상태 변화 및 효과
  - 배틀 로그 출력
- **턴 순서**: 
  - 스피드 스탯 기반 우선순위
  - 우선도 기술 고려
  - 동일 스피드 시 랜덤 결정
- **제약사항**: 
  - 포켓몬이 기절 상태가 아닐 때만 액션 가능
  - PP가 부족한 기술 사용 불가
  - 상태이상에 따른 액션 제한
- **수락 기준**: 
  - 턴 순서가 정확히 처리됨
  - 모든 액션이 올바르게 실행됨
  - 배틀 종료 조건이 정확히 판단됨

### REQ-009: 기술 시스템
- **설명**: 각 포켓몬당 최대 4개 기술 보유 및 사용
- **입력**: 
  - 기술 선택 (기술 메뉴에서)
  - 타겟 선택 (일부 기술)
  - 기술 사용 확인
- **출력**: 
  - 기술 실행 및 효과
  - 데미지 계산 및 적용
  - 상태이상 효과 적용
  - PP 소모
- **기술 속성**: 
  - **위력**: 데미지 계산에 사용
  - **명중률**: 기술 성공 확률
  - **PP**: 사용 가능 횟수
  - **타입**: 포켓몬 타입과의 상성
  - **카테고리**: 물리/특수/변화
  - **우선도**: 턴 순서 결정
- **제약사항**: 
  - PP가 0인 기술 사용 불가
  - 기절 상태에서 기술 사용 불가
  - 일부 기술은 특정 조건에서만 사용 가능
- **수락 기준**: 
  - 기술이 정확한 데미지로 적용됨
  - PP가 정확히 소모됨
  - 상태이상 효과가 올바르게 적용됨

### REQ-010: 상태 관리
- **설명**: HP, 상태이상, 버프/디버프 관리
- **입력**: 
  - 데미지 계산 결과
  - 상태이상 효과
  - 버프/디버프 효과
  - 회복 아이템 사용
- **출력**: 
  - HP 변화 및 표시
  - 상태이상 적용/해제
  - 버프/디버프 효과 적용
  - 상태 변화 알림
- **상태이상 타입**: 
  - **독**: 턴마다 HP 감소
  - **마비**: 25% 확률로 행동 불가
  - **수면**: 1-3턴 동안 행동 불가
  - **화상**: 턴마다 HP 감소, 물리 공격력 50% 감소
  - **얼음**: 행동 불가, 물리 공격에 취약
  - **혼란**: 50% 확률로 자신에게 데미지
- **제약사항**: 
  - 동일한 상태이상 중복 적용 불가
  - 일부 상태이상은 특정 조건에서만 해제
  - 상태이상은 턴마다 효과 적용
- **수락 기준**: 
  - 상태이상이 정확히 적용됨
  - 턴마다 상태이상 효과가 적용됨
  - 상태이상 해제 조건이 정확히 작동함

### REQ-011: 배틀 UI
- **설명**: 배틀 중 필요한 UI 요소 표시
- **UI 요소**: 
  - **플레이어 포켓몬 정보**: HP, 이름, 레벨, 상태이상
  - **상대 포켓몬 정보**: HP, 이름, 레벨, 상태이상
  - **액션 메뉴**: 싸운다, 가방, 포켓몬, 도망간다
  - **기술 선택 메뉴**: 4개 기술 목록, PP 표시
  - **대화창**: 배틀 로그, 기술 설명, 상태 변화 알림
  - **타겟 선택**: 다중 타겟 기술 시 타겟 선택
- **입력**: 
  - 마우스 클릭
  - 키보드 입력 (방향키, 엔터, ESC)
  - 터치 입력 (모바일)
- **출력**: 
  - UI 상태 업데이트
  - 사용자 입력 반응
  - 시각적 피드백
- **제약사항**: 
  - 배틀 중에만 UI 표시
  - 포켓몬 교체 시 UI 업데이트
  - 상태이상 시 해당 액션 비활성화
- **수락 기준**: 
  - 모든 UI 요소가 정확히 표시됨
  - 사용자 입력에 즉시 반응함
  - 상태 변화 시 UI가 실시간 업데이트됨

---

## 상세 설계

### 클래스 구조

#### BattleManager (배틀 관리)
```csharp
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    
    [Header("Battle State")]
    public BattleState currentState;
    public PokemonSO playerPokemon;
    public PokemonSO enemyPokemon;
    public bool isPlayerTurn;
    
    [Header("UI References")]
    public BattleUIController battleUI;
    public DialogManager dialogManager;
    
    [Header("Battle Settings")]
    public float turnDelay = 1.0f;
    public float animationDelay = 0.5f;
    
    private Queue<BattleAction> actionQueue;
    private bool isProcessingTurn;
    
    public void StartBattle(PokemonSO enemyPokemon, bool isWild = true)
    {
        // 배틀 시작
    }
    
    public void ProcessPlayerAction(BattleAction action)
    {
        // 플레이어 액션 처리
    }
    
    public void ProcessTurn()
    {
        // 턴 처리
    }
    
    public void EndBattle(BattleResult result)
    {
        // 배틀 종료
    }
}
```

#### BattleStateMachine (배틀 상태 관리)
```csharp
public class BattleStateMachine : MonoBehaviour
{
    public enum BattleState
    {
        Start,          // 배틀 시작
        PlayerTurn,     // 플레이어 턴
        EnemyTurn,      // 상대 턴
        Processing,     // 턴 처리 중
        End             // 배틀 종료
    }
    
    private BattleState currentState;
    private Dictionary<BattleState, IBattleState> states;
    
    public void ChangeState(BattleState newState)
    {
        // 상태 변경
    }
    
    public void Update()
    {
        // 현재 상태 업데이트
    }
}
```

#### MoveLogic (기술 로직)
```csharp
public class MoveLogic : MonoBehaviour
{
    public static int CalculateDamage(PokemonSO attacker, PokemonSO defender, MoveSO move)
    {
        // 데미지 계산
        float level = attacker.Level;
        float attack = move.Category == MoveCategory.Physical ? attacker.Attack : attacker.SpecialAttack;
        float defense = move.Category == MoveCategory.Physical ? defender.Defense : defender.SpecialDefense;
        
        float damage = ((2 * level + 10) * move.Power * attack / defense) / 50 + 2;
        
        // 타입 상성 적용
        float typeEffectiveness = GetTypeEffectiveness(move.Type, defender.Types);
        damage *= typeEffectiveness;
        
        // 랜덤 요소 (85-100%)
        float randomFactor = Random.Range(0.85f, 1.0f);
        damage *= randomFactor;
        
        return Mathf.RoundToInt(damage);
    }
    
    public static bool IsMoveHit(MoveSO move, PokemonSO attacker, PokemonSO defender)
    {
        // 명중률 계산
        float accuracy = move.Accuracy;
        
        // 상태이상 보정
        if (attacker.HasStatusEffect(StatusEffect.Confusion))
            accuracy *= 0.5f;
        
        return Random.Range(0f, 100f) < accuracy;
    }
    
    public static float GetTypeEffectiveness(PokemonType moveType, PokemonType[] targetTypes)
    {
        // 타입 상성 계산
        float effectiveness = 1.0f;
        
        foreach (var targetType in targetTypes)
        {
            effectiveness *= GetTypeEffectiveness(moveType, targetType);
        }
        
        return effectiveness;
    }
}
```

#### StatusEffectManager (상태이상 관리)
```csharp
public class StatusEffectManager : MonoBehaviour
{
    public static void ApplyStatusEffect(PokemonSO pokemon, StatusEffect effect)
    {
        // 상태이상 적용
        if (pokemon.CanHaveStatusEffect(effect))
        {
            pokemon.SetStatusEffect(effect);
            // 상태이상 효과 적용
        }
    }
    
    public static void ProcessStatusEffects(PokemonSO pokemon)
    {
        // 턴마다 상태이상 효과 처리
        switch (pokemon.StatusEffect)
        {
            case StatusEffect.Poison:
                ApplyPoisonDamage(pokemon);
                break;
            case StatusEffect.Burn:
                ApplyBurnDamage(pokemon);
                break;
            case StatusEffect.Paralysis:
                if (Random.Range(0f, 1f) < 0.25f)
                    pokemon.CannotAct = true;
                break;
            case StatusEffect.Sleep:
                if (pokemon.SleepTurns > 0)
                {
                    pokemon.CannotAct = true;
                    pokemon.SleepTurns--;
                }
                else
                {
                    pokemon.RemoveStatusEffect(StatusEffect.Sleep);
                }
                break;
        }
    }
}
```

#### BattleUIController (배틀 UI)
```csharp
public class BattleUIController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject actionMenu;
    public GameObject moveMenu;
    public GameObject itemMenu;
    public GameObject pokemonMenu;
    
    [Header("Pokemon Info")]
    public PokemonInfoUI playerPokemonInfo;
    public PokemonInfoUI enemyPokemonInfo;
    
    [Header("Dialog")]
    public Text dialogText;
    public GameObject dialogPanel;
    
    public void ShowActionMenu()
    {
        // 액션 메뉴 표시
    }
    
    public void ShowMoveMenu()
    {
        // 기술 메뉴 표시
    }
    
    public void UpdatePokemonInfo(PokemonSO pokemon, bool isPlayer)
    {
        // 포켓몬 정보 업데이트
    }
    
    public void ShowDialog(string message)
    {
        // 대화창 표시
    }
}
```

### 데이터 구조

#### BattleAction (배틀 액션)
```csharp
[System.Serializable]
public class BattleAction
{
    public ActionType type;
    public PokemonSO pokemon;
    public MoveSO move;
    public ItemSO item;
    public PokemonSO targetPokemon;
    public int priority;
    
    public enum ActionType
    {
        Attack,     // 공격
        Item,       // 아이템 사용
        Switch,     // 포켓몬 교체
        Run         // 도망
    }
}
```

#### BattleResult (배틀 결과)
```csharp
[System.Serializable]
public class BattleResult
{
    public bool isVictory;
    public int experienceGained;
    public List<ItemSO> itemsGained;
    public int moneyGained;
    public PokemonSO capturedPokemon;
}
```

#### PokemonInfoUI (포켓몬 정보 UI)
```csharp
public class PokemonInfoUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Text hpText;
    public Image statusEffectIcon;
    
    public void UpdateInfo(PokemonSO pokemon)
    {
        nameText.text = pokemon.Name;
        levelText.text = "Lv." + pokemon.Level;
        hpSlider.value = (float)pokemon.CurrentHP / pokemon.MaxHP;
        hpText.text = pokemon.CurrentHP + "/" + pokemon.MaxHP;
        
        // 상태이상 아이콘 업데이트
        if (pokemon.StatusEffect != StatusEffect.None)
        {
            statusEffectIcon.sprite = GetStatusEffectSprite(pokemon.StatusEffect);
            statusEffectIcon.gameObject.SetActive(true);
        }
        else
        {
            statusEffectIcon.gameObject.SetActive(false);
        }
    }
}
```

### 이벤트 흐름

#### 배틀 시작 플로우
1. **배틀 초기화**: `BattleManager`가 배틀 데이터 설정
2. **UI 표시**: 플레이어/상대 포켓몬 정보 표시
3. **배틀 시작**: 시작 메시지 출력
4. **턴 시작**: 플레이어 턴으로 시작

#### 턴 처리 플로우
1. **액션 선택**: 플레이어가 액션 선택
2. **액션 큐잉**: 선택된 액션을 큐에 추가
3. **턴 처리**: 스피드 순으로 액션 실행
4. **결과 적용**: 데미지, 상태이상 등 적용
5. **UI 업데이트**: 포켓몬 정보 업데이트
6. **배틀 종료 체크**: 승부 조건 확인

#### 기술 사용 플로우
1. **기술 선택**: 기술 메뉴에서 기술 선택
2. **명중률 체크**: 기술이 명중하는지 확인
3. **데미지 계산**: `MoveLogic`으로 데미지 계산
4. **타입 상성**: 타입 상성에 따른 데미지 보정
5. **데미지 적용**: 상대 포켓몬에게 데미지 적용
6. **상태이상**: 기술에 따른 상태이상 적용
7. **PP 소모**: 사용한 기술의 PP 감소

---

## 구현 가이드

### 기술적 고려사항

#### 턴 처리 시스템
- **액션 큐**: 동시 액션을 순서대로 처리
- **우선순위**: 스피드와 기술 우선순위 고려
- **비동기 처리**: 애니메이션과 로직 분리

#### 데미지 계산
- **공식**: 포켓몬스터 공식 데미지 계산식 사용
- **타입 상성**: 18가지 타입 간 상성 테이블
- **랜덤 요소**: 85-100% 랜덤 데미지

#### 상태이상 시스템
- **상태 관리**: 각 포켓몬의 상태이상 추적
- **턴 처리**: 턴마다 상태이상 효과 적용
- **해제 조건**: 특정 조건에서 상태이상 해제

### 성능 요구사항

#### 프레임레이트
- **목표**: 60fps 유지
- **최적화**: 
  - 불필요한 Update 호출 최소화
  - 이벤트 기반 처리
  - UI 업데이트 최적화

#### 메모리 사용량
- **목표**: 100MB 이하
- **최적화**: 
  - 포켓몬 데이터 캐싱
  - UI 오브젝트 풀링
  - 불필요한 컴포넌트 제거

### 테스트 케이스

#### 기능 테스트
1. **턴 처리 테스트**
   - 턴 순서가 정확히 처리되는지 확인
   - 동일 스피드 시 랜덤 결정이 작동하는지 확인
   - 우선순위 기술이 먼저 실행되는지 확인

2. **기술 시스템 테스트**
   - 기술이 정확한 데미지로 적용되는지 확인
   - PP가 정확히 소모되는지 확인
   - 상태이상 효과가 올바르게 적용되는지 확인

3. **상태이상 테스트**
   - 상태이상이 정확히 적용되는지 확인
   - 턴마다 상태이상 효과가 적용되는지 확인
   - 상태이상 해제 조건이 정확히 작동하는지 확인

4. **UI 테스트**
   - 모든 UI 요소가 정확히 표시되는지 확인
   - 사용자 입력에 즉시 반응하는지 확인
   - 상태 변화 시 UI가 실시간 업데이트되는지 확인

#### 성능 테스트
1. **프레임레이트 테스트**
   - 배틀 중 60fps 유지 여부 확인
   - 복잡한 배틀에서도 프레임 드롭 없음 확인

2. **메모리 테스트**
   - 메모리 누수 없음 확인
   - 가비지 컬렉션 최소화 확인

#### 통합 테스트
1. **다른 시스템과의 연동**
   - 풀숲 조우 시스템과의 연동
   - NPC 배틀 시스템과의 연동
   - 포획 시스템과의 연동

---

## 참고사항

### 관련 파일
- `BattleManager.cs`: 배틀 관리
- `BattleStateMachine.cs`: 배틀 상태 관리
- `MoveLogic.cs`: 기술 로직
- `StatusEffectManager.cs`: 상태이상 관리
- `BattleUIController.cs`: 배틀 UI
- `BattleAction.cs`: 배틀 액션
- `BattleResult.cs`: 배틀 결과

### 의존성
- **풀숲 조우 시스템**: 야생 포켓몬 배틀
- **NPC 배틀 시스템**: NPC 배틀
- **포획 시스템**: 배틀 중 포획
- **데이터 관리 시스템**: 포켓몬, 기술 데이터
- **UI 시스템**: 배틀 UI

### 향후 확장 계획
1. **더블 배틀**: 2vs2 배틀 시스템
2. **트리플 배틀**: 3vs3 배틀 시스템
3. **로테이션 배틀**: 포켓몬 교체 제한
4. **멀티플레이어**: 온라인 배틀
5. **배틀 타워**: 연속 배틀 시스템

### 버그 대응
- **턴 순서 오류**: 액션 큐 시스템 강화
- **데미지 계산 오류**: 공식 검증 및 테스트
- **상태이상 버그**: 상태 관리 시스템 강화
- **UI 동기화 오류**: 이벤트 기반 UI 업데이트

---

**문서 끝**
