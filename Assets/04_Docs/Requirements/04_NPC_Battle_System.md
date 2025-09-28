# NPC 배틀 시스템 요구사항 명세서

## 개요
맵 상의 NPC와의 상호작용 및 배틀을 처리하는 시스템입니다. 플레이어에게 도전과 보상을 제공하며, 게임의 진행과 스토리를 이끌어가는 중요한 요소입니다.

### 주요 목표
- NPC와의 자연스러운 상호작용 구현
- 대화 시스템을 통한 스토리 전달
- NPC별 고유한 포켓몬 파티 구성
- 배틀 승리 시 보상 지급 시스템
- 다양한 NPC 타입과 난이도 제공

### 관련 시스템과의 연관성
- **플레이어 이동 시스템**: NPC 접촉 감지
- **배틀 시스템**: NPC와의 배틀 진행
- **데이터 관리 시스템**: NPC 데이터 및 보상 정보
- **UI 시스템**: 대화 UI 및 상호작용 인터페이스
- **인벤토리 시스템**: 보상 아이템 지급

---

## 요구사항 목록

### REQ-012: NPC 상호작용
- **설명**: 플레이어가 NPC의 시야에 들어왔을 때 배틀 발생
- **입력**: 
  - 플레이어 위치 및 상태
  - NPC 시야 범위 및 각도
  - NPC 데이터 (이름, 대사, 파티 등)
- **출력**: 
  - NPC 대화 시작
  - 배틀 트리거
  - 상호작용 UI 표시
- **상호작용 조건**: 
  - 플레이어가 NPC의 시야 범위 내에 진입
  - NPC가 배틀 가능한 상태
  - 플레이어가 배틀 가능한 상태
  - NPC가 플레이어를 바라보고 있음
- **제약사항**: 
  - 중복 상호작용 방지
  - 배틀 중에는 상호작용 불가
  - 일부 NPC는 특정 조건에서만 상호작용 가능
- **수락 기준**: 
  - NPC 시야 진입 시 즉시 상호작용 시작
  - 중복 상호작용이 발생하지 않음
  - 상호작용 조건이 정확히 적용됨

### REQ-013: 대화 시스템
- **설명**: 배틀 시작 전 NPC 대사 출력
- **입력**: 
  - NPC 데이터 (대사 목록)
  - 플레이어 상태 (레벨, 포켓몬 등)
  - 대화 진행 상태
- **출력**: 
  - 대화창에 대사 표시
  - NPC 이름 및 초상화 표시
  - 대화 진행 버튼 표시
- **대화 UI 요소**: 
  - **대화창**: 대사 텍스트 표시
  - **NPC 이름**: 대화하는 NPC 이름
  - **NPC 초상화**: NPC 이미지 표시
  - **진행 버튼**: 다음 대사로 진행
  - **건너뛰기**: 대화 건너뛰기 (옵션)
- **제약사항**: 
  - 대화 중에는 다른 액션 불가
  - 대화 완료 후에만 배틀 시작
  - 대화 건너뛰기 시에도 배틀 시작
- **수락 기준**: 
  - 대사가 정확히 표시됨
  - 대화 진행이 자연스러움
  - 대화 완료 후 배틀이 정상 시작됨

### REQ-014: 보상 시스템
- **설명**: NPC 배틀 종료 후 골드, 아이템 지급
- **입력**: 
  - 배틀 결과 (승리/패배)
  - NPC 데이터 (보상 정보)
  - 플레이어 상태 (레벨, 진행도 등)
- **출력**: 
  - 보상 아이템 지급
  - 골드 지급
  - 경험치 지급 (선택사항)
  - 보상 알림 표시
- **보상 계산**: 
  - **기본 골드**: NPC 레벨 × 10
  - **아이템 확률**: NPC 타입별 확률
  - **레벨 보정**: 플레이어 레벨에 따른 보정
  - **난이도 보정**: NPC 난이도에 따른 보정
- **제약사항**: 
  - 승리 시에만 보상 지급
  - 인벤토리 공간 부족 시 보상 지급 실패
  - 중복 보상 방지
- **수락 기준**: 
  - 승리 시 정확한 보상이 지급됨
  - 패배 시 보상이 지급되지 않음
  - 보상 계산이 정확함

### REQ-015: 고정 파티
- **설명**: NPC별 고정된 포켓몬 파티 구성
- **입력**: 
  - NPC ID
  - NPC 타입 및 난이도
  - 플레이어 레벨 (레벨 스케일링)
- **출력**: 
  - NPC의 포켓몬 파티
  - 포켓몬 레벨 설정
  - 포켓몬 기술 구성
- **파티 구성 규칙**: 
  - **포켓몬 수**: 1-6마리 (NPC 타입별)
  - **레벨 범위**: NPC 레벨 ± 2
  - **타입 다양성**: 다양한 타입 조합
  - **기술 구성**: 포켓몬별 최적 기술
- **제약사항**: 
  - NPC별 고유 파티 유지
  - 포켓몬 레벨이 합리적 범위 내
  - 기술 PP가 충분함
- **수락 기준**: 
  - NPC별로 고유한 파티가 구성됨
  - 포켓몬 레벨이 적절함
  - 기술 구성이 전략적으로 의미있음

---

## 상세 설계

### 클래스 구조

#### NPCController (NPC 관리)
```csharp
public class NPCController : MonoBehaviour
{
    [Header("NPC Settings")]
    public NPCSO npcData;
    public bool hasBeenDefeated;
    public bool canBattle = true;
    
    [Header("Visual")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public GameObject exclamationMark;
    
    [Header("Sight Detection")]
    public float sightRange = 5.0f;
    public float sightAngle = 90.0f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    
    [Header("Detection Settings")]
    public float detectionCooldown = 2.0f;
    public bool isDetecting = false;
    
    private bool isInSight;
    private bool isInteracting;
    private float lastDetectionTime;
    private Transform playerTransform;
    
    private void Update()
    {
        if (canBattle && !isInteracting)
        {
            CheckPlayerInSight();
        }
    }
    
    private void CheckPlayerInSight()
    {
        // 플레이어가 시야 범위 내에 있는지 확인
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, sightRange, playerLayer);
        
        foreach (var player in playersInRange)
        {
            if (player.CompareTag("Player"))
            {
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
                
                // 시야 각도 내에 있고, 장애물이 없는지 확인
                if (angleToPlayer <= sightAngle / 2)
                {
                    if (!Physics.Linecast(transform.position, player.transform.position, obstacleLayer))
                    {
                        if (!isInSight && Time.time - lastDetectionTime > detectionCooldown)
                        {
                            StartInteraction();
                        }
                        isInSight = true;
                        return;
                    }
                }
            }
        }
        
        isInSight = false;
    }
    
    public void StartInteraction()
    {
        // 상호작용 시작
        if (!isInteracting)
        {
            isInteracting = true;
            lastDetectionTime = Time.time;
            DialogManager.Instance.StartDialog(npcData);
        }
    }
    
    public void StartBattle()
    {
        // 배틀 시작
        BattleManager.Instance.StartNPCBattle(npcData);
    }
    
    public void OnBattleEnd(bool playerWon)
    {
        // 배틀 종료 처리
        if (playerWon)
        {
            hasBeenDefeated = true;
            canBattle = false;
            GiveRewards();
        }
    }
}
```

#### DialogManager (대화 관리)
```csharp
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }
    
    [Header("UI References")]
    public GameObject dialogPanel;
    public Text dialogText;
    public Text npcNameText;
    public Image npcPortrait;
    public Button nextButton;
    public Button skipButton;
    
    [Header("Dialog Settings")]
    public float textSpeed = 0.05f;
    public bool canSkip = true;
    
    private NPCSO currentNPC;
    private List<string> currentDialogs;
    private int currentDialogIndex;
    private bool isTyping;
    
    public void StartDialog(NPCSO npcData)
    {
        // 대화 시작
        currentNPC = npcData;
        currentDialogs = new List<string>(npcData.DialogLines);
        currentDialogIndex = 0;
        
        ShowDialogUI();
        ShowNextDialog();
    }
    
    public void ShowNextDialog()
    {
        // 다음 대사 표시
        if (currentDialogIndex < currentDialogs.Count)
        {
            StartCoroutine(TypeText(currentDialogs[currentDialogIndex]));
            currentDialogIndex++;
        }
        else
        {
            EndDialog();
        }
    }
    
    public void SkipDialog()
    {
        // 대화 건너뛰기
        if (canSkip)
        {
            EndDialog();
        }
    }
    
    private void EndDialog()
    {
        // 대화 종료
        HideDialogUI();
        if (currentNPC != null)
        {
            NPCController npc = FindNPCController(currentNPC);
            if (npc != null)
            {
                npc.StartBattle();
            }
        }
    }
}
```

#### RewardManager (보상 관리)
```csharp
public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance { get; private set; }
    
    [Header("Reward UI")]
    public GameObject rewardPanel;
    public Text rewardText;
    public Image[] rewardIcons;
    public Text[] rewardAmounts;
    
    public void GiveRewards(NPCSO npcData, int playerLevel)
    {
        // 보상 지급
        List<RewardItem> rewards = CalculateRewards(npcData, playerLevel);
        
        foreach (var reward in rewards)
        {
            GiveRewardItem(reward);
        }
        
        ShowRewardUI(rewards);
    }
    
    private List<RewardItem> CalculateRewards(NPCSO npcData, int playerLevel)
    {
        List<RewardItem> rewards = new List<RewardItem>();
        
        // 골드 보상
        int gold = CalculateGoldReward(npcData, playerLevel);
        rewards.Add(new RewardItem { type = RewardType.Gold, amount = gold });
        
        // 아이템 보상
        List<ItemSO> items = CalculateItemRewards(npcData, playerLevel);
        foreach (var item in items)
        {
            rewards.Add(new RewardItem { type = RewardType.Item, item = item, amount = 1 });
        }
        
        return rewards;
    }
    
    private int CalculateGoldReward(NPCSO npcData, int playerLevel)
    {
        int baseGold = npcData.RewardGold;
        int levelBonus = playerLevel * 5;
        int difficultyBonus = (int)(baseGold * npcData.Difficulty * 0.1f);
        
        return baseGold + levelBonus + difficultyBonus;
    }
}
```

#### NPCPartyManager (NPC 파티 관리)
```csharp
public class NPCPartyManager : MonoBehaviour
{
    public static NPCPartyManager Instance { get; private set; }
    
    public List<PokemonSO> GenerateNPCParty(NPCSO npcData, int playerLevel)
    {
        // NPC 파티 생성
        List<PokemonSO> party = new List<PokemonSO>();
        
        foreach (var pokemonData in npcData.Party)
        {
            PokemonSO npcPokemon = CreateNPCPokemon(pokemonData, playerLevel);
            party.Add(npcPokemon);
        }
        
        return party;
    }
    
    private PokemonSO CreateNPCPokemon(PokemonSO basePokemon, int playerLevel)
    {
        // NPC 포켓몬 생성
        PokemonSO npcPokemon = Instantiate(basePokemon);
        
        // 레벨 설정
        int npcLevel = CalculateNPCLevel(basePokemon, playerLevel);
        npcPokemon.SetLevel(npcLevel);
        
        // 기술 설정
        SetNPCPokemonMoves(npcPokemon);
        
        // 스탯 설정
        CalculateNPCStats(npcPokemon);
        
        return npcPokemon;
    }
    
    private int CalculateNPCLevel(PokemonSO basePokemon, int playerLevel)
    {
        // NPC 포켓몬 레벨 계산
        int baseLevel = basePokemon.Level;
        int levelRange = Mathf.RoundToInt(baseLevel * 0.2f);
        int minLevel = Mathf.Max(1, baseLevel - levelRange);
        int maxLevel = baseLevel + levelRange;
        
        return Random.Range(minLevel, maxLevel + 1);
    }
}
```

### 데이터 구조

#### NPCSO (NPC 데이터)
```csharp
[CreateAssetMenu(fileName = "New NPC", menuName = "Pokemon/NPC Data")]
public class NPCSO : ScriptableObject
{
    [Header("Basic Info")]
    public int ID;
    public string Name;
    public NPCType Type;
    public float Difficulty = 1.0f;
    
    [Header("Dialog")]
    public string[] DialogLines;
    public Sprite Portrait;
    
    [Header("Battle")]
    public List<PokemonSO> Party;
    public int[] PartyLevels;
    public bool CanRebattle = false;
    
    [Header("Rewards")]
    public int RewardGold;
    public List<ItemSO> RewardItems;
    public float[] ItemChances;
    
    [Header("Visual")]
    public GameObject Prefab;
    public Sprite Sprite;
    public AnimatorOverrideController AnimatorController;
}
```

#### NPCType (NPC 타입)
```csharp
public enum NPCType
{
    Trainer,        // 일반 트레이너
    GymLeader,      // 체육관 관장
    EliteFour,      // 사천왕
    Champion,       // 챔피언
    Rival,          // 라이벌
    TeamMember,     // 악역 조직원
    ShopKeeper,     // 상점 주인
    Researcher      // 연구원
}
```

#### RewardItem (보상 아이템)
```csharp
[System.Serializable]
public class RewardItem
{
    public RewardType type;
    public ItemSO item;
    public int amount;
    
    public enum RewardType
    {
        Gold,
        Item,
        Experience,
        Pokemon
    }
}
```

### 이벤트 흐름

#### NPC 상호작용 플로우
1. **시야 감지**: 플레이어가 NPC의 시야 범위 내에 진입
2. **시야 각도 확인**: NPC가 플레이어를 바라보고 있는지 확인
3. **장애물 체크**: NPC와 플레이어 사이에 장애물이 없는지 확인
4. **상호작용 시작**: `NPCController`가 상호작용 시작
5. **대화 시작**: `DialogManager`가 대화 UI 표시
6. **대화 진행**: 플레이어가 대화 진행
7. **배틀 시작**: 대화 완료 후 배틀 시작
8. **배틀 진행**: `BattleManager`가 배틀 처리
9. **보상 지급**: 승리 시 `RewardManager`가 보상 지급

#### 보상 지급 플로우
1. **배틀 결과**: 승리/패배 결과 확인
2. **보상 계산**: NPC 데이터와 플레이어 레벨 기반 계산
3. **아이템 지급**: 인벤토리에 아이템 추가
4. **골드 지급**: 플레이어 골드 증가
5. **보상 UI**: 보상 내용 표시
6. **완료**: 보상 지급 완료

---

## 구현 가이드

### 기술적 고려사항

#### 상호작용 시스템
- **시야 감지**: NPC의 시야 범위 및 각도 기반 감지
- **장애물 체크**: Linecast를 통한 장애물 감지
- **상태 관리**: 상호작용 중 상태 추적
- **중복 방지**: 동시 상호작용 방지

#### 대화 시스템
- **타이핑 효과**: 텍스트가 천천히 나타나는 효과
- **건너뛰기**: 대화 건너뛰기 기능
- **다국어 지원**: 다양한 언어 지원 (향후)

#### 보상 시스템
- **확률 계산**: 아이템 지급 확률 계산
- **인벤토리 연동**: 보상 아이템을 인벤토리에 추가
- **중복 방지**: 동일한 보상 중복 지급 방지

### 성능 요구사항

#### 프레임레이트
- **목표**: 60fps 유지
- **최적화**: 
  - 불필요한 Update 호출 최소화
  - 이벤트 기반 처리
  - UI 업데이트 최적화

#### 메모리 사용량
- **목표**: 50MB 이하
- **최적화**: 
  - NPC 데이터 캐싱
  - UI 오브젝트 풀링
  - 불필요한 컴포넌트 제거

### 테스트 케이스

#### 기능 테스트
1. **상호작용 테스트**
   - NPC 시야 진입 시 상호작용이 시작되는지 확인
   - 시야 각도와 장애물 체크가 정확히 작동하는지 확인
   - 중복 상호작용이 방지되는지 확인
   - 상호작용 조건이 정확히 적용되는지 확인

2. **대화 시스템 테스트**
   - 대사가 정확히 표시되는지 확인
   - 대화 진행이 자연스러운지 확인
   - 건너뛰기 기능이 작동하는지 확인

3. **보상 시스템 테스트**
   - 승리 시 정확한 보상이 지급되는지 확인
   - 패배 시 보상이 지급되지 않는지 확인
   - 보상 계산이 정확한지 확인

4. **파티 시스템 테스트**
   - NPC별로 고유한 파티가 구성되는지 확인
   - 포켓몬 레벨이 적절한지 확인
   - 기술 구성이 의미있는지 확인

#### 성능 테스트
1. **프레임레이트 테스트**
   - NPC 상호작용 시 프레임 드롭 없음 확인
   - 60fps 유지 여부 확인

2. **메모리 테스트**
   - 메모리 누수 없음 확인
   - 가비지 컬렉션 최소화 확인

#### 통합 테스트
1. **다른 시스템과의 연동**
   - 플레이어 이동 시스템과의 연동
   - 배틀 시스템과의 연동
   - 인벤토리 시스템과의 연동

---

## 참고사항

### 관련 파일
- `NPCController.cs`: NPC 관리
- `DialogManager.cs`: 대화 관리
- `RewardManager.cs`: 보상 관리
- `NPCPartyManager.cs`: NPC 파티 관리
- `NPCSO.cs`: NPC 데이터
- `RewardItem.cs`: 보상 아이템

### 의존성
- **플레이어 이동 시스템**: NPC 시야 감지
- **배틀 시스템**: NPC 배틀
- **데이터 관리 시스템**: NPC 데이터
- **UI 시스템**: 대화 UI
- **인벤토리 시스템**: 보상 지급

### 향후 확장 계획
1. **스토리 진행**: NPC별 스토리 라인
2. **퀘스트 시스템**: NPC로부터 퀘스트 수주
3. **친밀도 시스템**: NPC와의 관계도
4. **멀티플레이어**: 다른 플레이어와의 NPC 배틀
5. **AI 대화**: AI 기반 동적 대화

### 버그 대응
- **시야 감지 오류**: 시야 각도 및 범위 계산 검증
- **장애물 체크 실패**: Linecast 정확성 검증
- **상호작용 중복**: 상태 관리 시스템 강화
- **대화 버그**: 대화 상태 추적 강화
- **보상 중복**: 보상 지급 로직 검증
- **파티 오류**: NPC 파티 생성 로직 검증

---

**문서 끝**
