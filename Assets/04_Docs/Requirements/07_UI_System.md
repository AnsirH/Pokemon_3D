# UI 시스템 요구사항 명세서

## 개요
게임의 모든 사용자 인터페이스를 관리하는 시스템입니다. 직관적이고 반응성 있는 UI를 통해 플레이어에게 최적의 게임 경험을 제공합니다.

### 주요 목표
- 직관적이고 사용하기 쉬운 UI 설계
- 반응성 있는 사용자 인터페이스 구현
- 다양한 화면 크기와 해상도 지원
- 접근성 고려한 UI 설계
- 일관성 있는 UI 디자인 시스템

### 관련 시스템과의 연관성
- **모든 게임 시스템**: 모든 시스템이 UI를 통해 상호작용
- **플레이어 이동 시스템**: HUD, 미니맵 표시
- **배틀 시스템**: 배틀 UI, 메뉴 시스템
- **포획 시스템**: 포획 UI, 연출 표시
- **NPC 시스템**: 대화 UI, 상호작용 인터페이스
- **데이터 관리 시스템**: 인벤토리, 파티 관리 UI

---

## 요구사항 목록

### REQ-024: 메인 UI (HUD)
- **설명**: 게임 플레이 중 항상 표시되는 기본 UI
- **입력**: 
  - 플레이어 상태 정보
  - 게임 진행 정보
  - 시스템 알림
- **출력**: 
  - HUD 요소 표시
  - 상태 정보 업데이트
  - 알림 메시지 표시
- **UI 요소**: 
  - **플레이어 정보**: 이름, 레벨, 골드
  - **포켓몬 파티**: 현재 파티 포켓몬 6마리
  - **미니맵**: 현재 위치 및 주변 지형
  - **상태 표시**: HP, 상태이상, 버프/디버프
  - **알림**: 시스템 메시지, 이벤트 알림
- **제약사항**: 
  - 화면 가장자리에 고정
  - 다른 UI와 겹치지 않음
  - 성능에 영향을 주지 않음
- **수락 기준**: 
  - 모든 HUD 요소가 정확히 표시됨
  - 실시간으로 정보가 업데이트됨
  - 다른 UI와 충돌하지 않음

### REQ-025: 배틀 UI
- **설명**: 배틀 중 필요한 모든 UI 요소
- **입력**: 
  - 배틀 상태 정보
  - 플레이어 액션 선택
  - 기술 및 아이템 정보
- **출력**: 
  - 배틀 UI 표시
  - 액션 메뉴 표시
  - 배틀 로그 출력
- **UI 요소**: 
  - **포켓몬 정보**: 플레이어/상대 포켓몬 HP, 이름, 레벨
  - **액션 메뉴**: 싸운다, 가방, 포켓몬, 도망간다
  - **기술 메뉴**: 4개 기술 목록, PP 표시
  - **아이템 메뉴**: 사용 가능한 아이템 목록
  - **포켓몬 메뉴**: 파티 포켓몬 목록
  - **대화창**: 배틀 로그, 기술 설명
- **제약사항**: 
  - 배틀 중에만 표시
  - 액션 선택 시 다른 메뉴 숨김
  - 턴 진행 중 입력 차단
- **수락 기준**: 
  - 모든 배틀 UI가 정확히 표시됨
  - 액션 선택이 즉시 반응함
  - 배틀 로그가 실시간 업데이트됨

### REQ-026: 대화 UI
- **설명**: NPC와의 대화 및 스토리 진행 UI
- **입력**: 
  - NPC 대사 데이터
  - 플레이어 선택지
  - 대화 진행 상태
- **출력**: 
  - 대화창 표시
  - NPC 초상화 및 이름
  - 선택지 표시
- **UI 요소**: 
  - **대화창**: 대사 텍스트 표시
  - **NPC 정보**: 이름, 초상화
  - **선택지**: 플레이어가 선택할 수 있는 옵션
  - **진행 버튼**: 다음 대사로 진행
  - **건너뛰기**: 대화 건너뛰기 (옵션)
- **제약사항**: 
  - 대화 중에는 다른 액션 불가
  - 대화 완료 후에만 다른 UI 표시
  - 대화 건너뛰기 시에도 스토리 진행
- **수락 기준**: 
  - 대사가 정확히 표시됨
  - 선택지가 올바르게 작동함
  - 대화 진행이 자연스러움

### REQ-027: 인벤토리 UI
- **설명**: 아이템 관리 및 사용을 위한 UI
- **입력**: 
  - 인벤토리 데이터
  - 아이템 사용 요청
  - 아이템 정렬/필터링
- **출력**: 
  - 인벤토리 목록 표시
  - 아이템 상세 정보
  - 아이템 사용 결과
- **UI 요소**: 
  - **아이템 목록**: 그리드 형태의 아이템 표시
  - **아이템 정보**: 이름, 설명, 수량, 가격
  - **카테고리 필터**: 타입별 아이템 필터링
  - **정렬 옵션**: 이름, 가격, 획득일 순 정렬
  - **사용 버튼**: 아이템 사용/장착
  - **버리기 버튼**: 아이템 삭제
- **제약사항**: 
  - 인벤토리 공간 제한
  - 아이템 사용 조건 확인
  - 중요한 아이템 삭제 방지
- **수락 기준**: 
  - 아이템이 정확히 표시됨
  - 필터링과 정렬이 정상 작동함
  - 아이템 사용이 올바르게 처리됨

### REQ-028: 파티 관리 UI
- **설명**: 포켓몬 파티 관리 및 교체 UI
- **입력**: 
  - 파티 데이터
  - 포켓몬 교체 요청
  - 포켓몬 정보 조회
- **출력**: 
  - 파티 목록 표시
  - 포켓몬 상세 정보
  - 교체 결과
- **UI 요소**: 
  - **파티 슬롯**: 6개 포켓몬 슬롯
  - **포켓몬 정보**: 이름, 레벨, HP, 타입
  - **상세 정보**: 스탯, 기술, 상태이상
  - **교체 버튼**: 포켓몬 교체
  - **박스 연결**: 박스에서 포켓몬 가져오기
- **제약사항**: 
  - 파티 최대 6마리
  - 기절한 포켓몬은 교체 불가
  - 교체 시 배틀 중단
- **수락 기준**: 
  - 파티가 정확히 표시됨
  - 포켓몬 교체가 정상 작동함
  - 상세 정보가 정확히 표시됨

### REQ-029: 설정 UI
- **설명**: 게임 설정 및 옵션 관리 UI
- **입력**: 
  - 설정 변경 요청
  - 키 바인딩 변경
  - 그래픽/오디오 설정
- **출력**: 
  - 설정 메뉴 표시
  - 설정 변경 결과
  - 설정 저장 확인
- **UI 요소**: 
  - **오디오 설정**: 마스터, 음악, 효과음 볼륨
  - **그래픽 설정**: 해상도, 품질, 전체화면
  - **조작 설정**: 키 바인딩, 마우스 감도
  - **게임플레이 설정**: 자동저장, 애니메이션 스킵
  - **저장/로드**: 게임 저장 및 로드
- **제약사항**: 
  - 설정 변경 시 즉시 적용
  - 설정 저장 실패 시 복구
  - 키 바인딩 중복 방지
- **수락 기준**: 
  - 모든 설정이 정확히 표시됨
  - 설정 변경이 즉시 적용됨
  - 설정이 정확히 저장됨

---

## 상세 설계

### 클래스 구조

#### UIManager (UI 관리)
```csharp
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [Header("UI Panels")]
    public GameObject hudPanel;
    public GameObject battlePanel;
    public GameObject dialogPanel;
    public GameObject inventoryPanel;
    public GameObject partyPanel;
    public GameObject settingsPanel;
    
    [Header("UI Controllers")]
    public HUDController hudController;
    public BattleUIController battleUIController;
    public DialogController dialogController;
    public InventoryController inventoryController;
    public PartyController partyController;
    public SettingsController settingsController;
    
    private Dictionary<UIType, GameObject> uiPanels;
    private Stack<UIType> uiStack;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void InitializeUI()
    {
        uiPanels = new Dictionary<UIType, GameObject>();
        uiStack = new Stack<UIType>();
        
        // UI 패널 등록
        RegisterUIPanel(UIType.HUD, hudPanel);
        RegisterUIPanel(UIType.Battle, battlePanel);
        RegisterUIPanel(UIType.Dialog, dialogPanel);
        RegisterUIPanel(UIType.Inventory, inventoryPanel);
        RegisterUIPanel(UIType.Party, partyPanel);
        RegisterUIPanel(UIType.Settings, settingsPanel);
    }
    
    public void ShowUI(UIType uiType, bool hideOthers = false)
    {
        if (hideOthers)
        {
            HideAllUI();
        }
        
        if (uiPanels.ContainsKey(uiType))
        {
            uiPanels[uiType].SetActive(true);
            uiStack.Push(uiType);
        }
    }
    
    public void HideUI(UIType uiType)
    {
        if (uiPanels.ContainsKey(uiType))
        {
            uiPanels[uiType].SetActive(false);
        }
    }
    
    public void HideAllUI()
    {
        foreach (var panel in uiPanels.Values)
        {
            panel.SetActive(false);
        }
        uiStack.Clear();
    }
}
```

#### HUDController (HUD 제어)
```csharp
public class HUDController : MonoBehaviour
{
    [Header("Player Info")]
    public Text playerNameText;
    public Text playerLevelText;
    public Text goldText;
    
    [Header("Pokemon Party")]
    public Transform partyContainer;
    public GameObject pokemonSlotPrefab;
    
    [Header("Mini Map")]
    public Image miniMapImage;
    public Transform playerMarker;
    
    [Header("Status Effects")]
    public Transform statusEffectContainer;
    public GameObject statusEffectPrefab;
    
    [Header("Notifications")]
    public GameObject notificationPanel;
    public Text notificationText;
    
    private List<GameObject> pokemonSlots;
    private List<GameObject> statusEffects;
    
    public void UpdatePlayerInfo(string name, int level, int gold)
    {
        playerNameText.text = name;
        playerLevelText.text = "Lv." + level;
        goldText.text = gold.ToString();
    }
    
    public void UpdatePokemonParty(List<PokemonSO> party)
    {
        // 기존 슬롯 제거
        foreach (var slot in pokemonSlots)
        {
            Destroy(slot);
        }
        pokemonSlots.Clear();
        
        // 새 슬롯 생성
        for (int i = 0; i < party.Count; i++)
        {
            GameObject slot = Instantiate(pokemonSlotPrefab, partyContainer);
            PokemonSlotUI slotUI = slot.GetComponent<PokemonSlotUI>();
            slotUI.SetPokemon(party[i]);
            pokemonSlots.Add(slot);
        }
    }
    
    public void ShowNotification(string message, float duration = 3f)
    {
        StartCoroutine(ShowNotificationCoroutine(message, duration));
    }
    
    private IEnumerator ShowNotificationCoroutine(string message, float duration)
    {
        notificationPanel.SetActive(true);
        notificationText.text = message;
        
        yield return new WaitForSeconds(duration);
        
        notificationPanel.SetActive(false);
    }
}
```

#### DialogController (대화 제어)
```csharp
public class DialogController : MonoBehaviour
{
    [Header("Dialog UI")]
    public GameObject dialogPanel;
    public Text dialogText;
    public Text npcNameText;
    public Image npcPortrait;
    public Button nextButton;
    public Button skipButton;
    
    [Header("Choice UI")]
    public GameObject choicePanel;
    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;
    
    [Header("Settings")]
    public float textSpeed = 0.05f;
    public bool canSkip = true;
    
    private List<string> currentDialogs;
    private int currentDialogIndex;
    private bool isTyping;
    private Action onDialogComplete;
    
    public void StartDialog(List<string> dialogs, string npcName, Sprite npcPortrait, Action onComplete = null)
    {
        currentDialogs = new List<string>(dialogs);
        currentDialogIndex = 0;
        onDialogComplete = onComplete;
        
        npcNameText.text = npcName;
        this.npcPortrait.sprite = npcPortrait;
        
        dialogPanel.SetActive(true);
        ShowNextDialog();
    }
    
    public void ShowNextDialog()
    {
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
    
    public void ShowChoices(List<string> choices, Action<int> onChoiceSelected)
    {
        choicePanel.SetActive(true);
        
        // 기존 선택지 제거
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
        
        // 새 선택지 생성
        for (int i = 0; i < choices.Count; i++)
        {
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choices[i];
            
            int choiceIndex = i;
            choiceButton.GetComponent<Button>().onClick.AddListener(() => {
                onChoiceSelected(choiceIndex);
                choicePanel.SetActive(false);
            });
        }
    }
    
    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogText.text = "";
        
        foreach (char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        
        isTyping = false;
    }
    
    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        onDialogComplete?.Invoke();
    }
}
```

#### InventoryController (인벤토리 제어)
```csharp
public class InventoryController : MonoBehaviour
{
    [Header("Inventory UI")]
    public GameObject inventoryPanel;
    public Transform itemContainer;
    public GameObject itemSlotPrefab;
    
    [Header("Item Info")]
    public GameObject itemInfoPanel;
    public Text itemNameText;
    public Text itemDescriptionText;
    public Text itemQuantityText;
    public Image itemIconImage;
    
    [Header("Filters")]
    public Dropdown categoryFilter;
    public Dropdown sortFilter;
    
    [Header("Actions")]
    public Button useButton;
    public Button dropButton;
    
    private List<ItemSO> currentItems;
    private ItemSO selectedItem;
    private Dictionary<ItemType, List<ItemSO>> filteredItems;
    
    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
        LoadInventory();
    }
    
    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
    }
    
    private void LoadInventory()
    {
        currentItems = InventoryManager.Instance.GetAllItems();
        FilterItems();
        DisplayItems();
    }
    
    private void FilterItems()
    {
        filteredItems = new Dictionary<ItemType, List<ItemSO>>();
        
        foreach (var item in currentItems)
        {
            if (!filteredItems.ContainsKey(item.Type))
            {
                filteredItems[item.Type] = new List<ItemSO>();
            }
            filteredItems[item.Type].Add(item);
        }
    }
    
    private void DisplayItems()
    {
        // 기존 아이템 슬롯 제거
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }
        
        // 새 아이템 슬롯 생성
        var itemsToShow = GetFilteredItems();
        foreach (var item in itemsToShow)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemContainer);
            ItemSlotUI slotUI = slot.GetComponent<ItemSlotUI>();
            slotUI.SetItem(item);
            slotUI.OnItemSelected += OnItemSelected;
        }
    }
    
    private void OnItemSelected(ItemSO item)
    {
        selectedItem = item;
        ShowItemInfo(item);
    }
    
    private void ShowItemInfo(ItemSO item)
    {
        itemInfoPanel.SetActive(true);
        itemNameText.text = item.Name;
        itemDescriptionText.text = item.Description;
        itemQuantityText.text = "수량: " + InventoryManager.Instance.GetItemQuantity(item.ID);
        itemIconImage.sprite = item.Icon;
        
        useButton.interactable = item.IsUsable;
        dropButton.interactable = true;
    }
}
```

#### PartyController (파티 제어)
```csharp
public class PartyController : MonoBehaviour
{
    [Header("Party UI")]
    public GameObject partyPanel;
    public Transform partyContainer;
    public GameObject pokemonSlotPrefab;
    
    [Header("Pokemon Info")]
    public GameObject pokemonInfoPanel;
    public Text pokemonNameText;
    public Text pokemonLevelText;
    public Slider hpSlider;
    public Text hpText;
    public Image pokemonImage;
    
    [Header("Actions")]
    public Button switchButton;
    public Button releaseButton;
    
    private List<PokemonSO> currentParty;
    private PokemonSO selectedPokemon;
    
    public void ShowParty()
    {
        partyPanel.SetActive(true);
        LoadParty();
    }
    
    public void HideParty()
    {
        partyPanel.SetActive(false);
    }
    
    private void LoadParty()
    {
        currentParty = PartyManager.Instance.GetParty();
        DisplayParty();
    }
    
    private void DisplayParty()
    {
        // 기존 포켓몬 슬롯 제거
        foreach (Transform child in partyContainer)
        {
            Destroy(child.gameObject);
        }
        
        // 새 포켓몬 슬롯 생성
        for (int i = 0; i < currentParty.Count; i++)
        {
            GameObject slot = Instantiate(pokemonSlotPrefab, partyContainer);
            PokemonSlotUI slotUI = slot.GetComponent<PokemonSlotUI>();
            slotUI.SetPokemon(currentParty[i]);
            slotUI.OnPokemonSelected += OnPokemonSelected;
        }
    }
    
    private void OnPokemonSelected(PokemonSO pokemon)
    {
        selectedPokemon = pokemon;
        ShowPokemonInfo(pokemon);
    }
    
    private void ShowPokemonInfo(PokemonSO pokemon)
    {
        pokemonInfoPanel.SetActive(true);
        pokemonNameText.text = pokemon.Name;
        pokemonLevelText.text = "Lv." + pokemon.Level;
        hpSlider.value = (float)pokemon.CurrentHP / pokemon.MaxHP;
        hpText.text = pokemon.CurrentHP + "/" + pokemon.MaxHP;
        pokemonImage.sprite = pokemon.Icon;
        
        switchButton.interactable = true;
        releaseButton.interactable = true;
    }
}
```

### 데이터 구조

#### UIType (UI 타입)
```csharp
public enum UIType
{
    HUD,
    Battle,
    Dialog,
    Inventory,
    Party,
    Settings,
    MainMenu,
    PauseMenu
}
```

#### UIState (UI 상태)
```csharp
public enum UIState
{
    Hidden,
    Showing,
    Visible,
    Hiding
}
```

#### NotificationData (알림 데이터)
```csharp
[System.Serializable]
public class NotificationData
{
    public string message;
    public float duration;
    public NotificationType type;
    public Sprite icon;
    
    public enum NotificationType
    {
        Info,
        Warning,
        Error,
        Success
    }
}
```

### 이벤트 흐름

#### UI 표시 플로우
1. **UI 요청**: 시스템에서 UI 표시 요청
2. **UI 검증**: 표시 가능 여부 확인
3. **UI 표시**: 해당 UI 패널 활성화
4. **UI 초기화**: UI 데이터 로드 및 표시
5. **사용자 상호작용**: 플레이어 입력 처리
6. **UI 숨김**: UI 사용 완료 후 숨김

#### 대화 UI 플로우
1. **대화 시작**: NPC와의 대화 시작
2. **대사 표시**: 타이핑 효과로 대사 표시
3. **사용자 입력**: 다음 대사 또는 선택지 선택
4. **대화 진행**: 다음 대사로 진행
5. **대화 완료**: 모든 대사 완료 후 종료

---

## 구현 가이드

### 기술적 고려사항

#### UI 시스템 아키텍처
- **MVC 패턴**: Model-View-Controller 분리
- **이벤트 기반**: UI 간 통신을 이벤트로 처리
- **상태 관리**: UI 상태 추적 및 관리

#### 반응형 UI
- **Canvas Scaler**: 다양한 해상도 지원
- **Anchor 설정**: 화면 크기 변경에 대응
- **텍스트 크기**: 접근성을 고려한 텍스트 크기

#### 성능 최적화
- **오브젝트 풀링**: UI 요소 재사용
- **지연 로딩**: 필요할 때만 UI 로드
- **업데이트 최적화**: 불필요한 업데이트 방지

### 성능 요구사항

#### 프레임레이트
- **목표**: 60fps 유지
- **최적화**: 
  - UI 업데이트 최소화
  - 불필요한 리프레시 방지
  - 효율적인 렌더링

#### 메모리 사용량
- **목표**: 100MB 이하
- **최적화**: 
  - UI 오브젝트 풀링
  - 텍스처 압축
  - 불필요한 컴포넌트 제거

### 테스트 케이스

#### 기능 테스트
1. **UI 표시 테스트**
   - 모든 UI가 정확히 표시되는지 확인
   - UI 간 전환이 자연스러운지 확인
   - UI 상태가 정확히 관리되는지 확인

2. **사용자 상호작용 테스트**
   - 버튼 클릭이 정확히 반응하는지 확인
   - 입력이 즉시 처리되는지 확인
   - 오류 입력에 적절히 대응하는지 확인

3. **데이터 표시 테스트**
   - 데이터가 정확히 표시되는지 확인
   - 실시간 업데이트가 정상 작동하는지 확인
   - 데이터 변경 시 UI가 동기화되는지 확인

#### 성능 테스트
1. **프레임레이트 테스트**
   - UI 사용 시 60fps 유지 여부 확인
   - 복잡한 UI에서도 프레임 드롭 없음 확인

2. **메모리 테스트**
   - 메모리 누수 없음 확인
   - UI 오브젝트가 정확히 해제되는지 확인

#### 접근성 테스트
1. **키보드 네비게이션**
   - 키보드만으로 모든 UI 사용 가능한지 확인
   - Tab 순서가 논리적인지 확인

2. **화면 읽기 프로그램**
   - 스크린 리더와 호환되는지 확인
   - 적절한 라벨과 설명이 있는지 확인

---

## 참고사항

### 관련 파일
- `UIManager.cs`: UI 관리
- `HUDController.cs`: HUD 제어
- `DialogController.cs`: 대화 제어
- `InventoryController.cs`: 인벤토리 제어
- `PartyController.cs`: 파티 제어
- `SettingsController.cs`: 설정 제어

### 의존성
- **모든 게임 시스템**: UI 표시 및 상호작용
- **Unity UGUI**: UI 렌더링
- **Unity Input System**: 입력 처리

### 향후 확장 계획
1. **모바일 UI**: 터치 인터페이스 지원
2. **다국어 지원**: 다양한 언어 UI
3. **커스터마이징**: UI 테마 및 레이아웃 변경
4. **접근성 향상**: 더 나은 접근성 기능
5. **VR 지원**: VR 환경에서의 UI

### 버그 대응
- **UI 겹침**: UI 레이어 관리 강화
- **입력 지연**: 입력 처리 최적화
- **메모리 누수**: UI 오브젝트 생명주기 관리
- **성능 이슈**: UI 렌더링 최적화

---

**문서 끝**
