# 데이터 관리 시스템 요구사항 명세서

## 개요
게임의 모든 데이터를 체계적으로 관리하는 핵심 시스템입니다. ScriptableObject 기반의 데이터베이스로 포켓몬, 기술, 아이템, NPC 등의 정보를 효율적으로 저장하고 관리합니다.

### 주요 목표
- ScriptableObject 기반 데이터베이스 구축
- 데이터 무결성 및 일관성 보장
- 효율적인 데이터 검색 및 접근
- 에디터 툴을 통한 데이터 관리 편의성
- 확장 가능한 데이터 구조 설계

### 관련 시스템과의 연관성
- **모든 게임 시스템**: 모든 시스템이 데이터를 참조
- **배틀 시스템**: 포켓몬, 기술 데이터
- **포획 시스템**: 몬스터볼, 포켓몬 데이터
- **NPC 시스템**: NPC, 보상 데이터
- **UI 시스템**: 아이템, 포켓몬 아이콘 데이터

---

## 요구사항 목록

### REQ-020: ScriptableObject 데이터베이스
- **설명**: 모든 게임 데이터를 ScriptableObject로 관리
- **입력**: 
  - 포켓몬 데이터 (이름, 스탯, 타입 등)
  - 기술 데이터 (이름, 위력, 타입 등)
  - 아이템 데이터 (이름, 효과, 가격 등)
  - NPC 데이터 (이름, 파티, 보상 등)
- **출력**: 
  - 구조화된 데이터베이스
  - 에디터에서 접근 가능한 데이터
  - 런타임에서 참조 가능한 데이터
- **데이터 타입**: 
  - **PokemonSO**: 포켓몬 정보
  - **MoveSO**: 기술 정보
  - **ItemSO**: 아이템 정보
  - **NPCSO**: NPC 정보
  - **TypeSO**: 타입 상성 정보
- **제약사항**: 
  - 모든 데이터는 ScriptableObject로 정의
  - 데이터 참조 무결성 보장
  - 중복 데이터 방지
- **수락 기준**: 
  - 모든 데이터가 ScriptableObject로 정의됨
  - 데이터 참조가 정확히 작동함
  - 에디터에서 데이터 수정이 가능함

### REQ-021: 데이터 검색 시스템
- **설명**: 효율적인 데이터 검색 및 접근 시스템
- **입력**: 
  - 검색 조건 (ID, 이름, 타입 등)
  - 필터링 옵션
  - 정렬 기준
- **출력**: 
  - 검색 결과 데이터
  - 필터링된 데이터 목록
  - 정렬된 데이터 목록
- **검색 기능**: 
  - **ID 기반 검색**: 고유 ID로 데이터 검색
  - **이름 기반 검색**: 이름으로 데이터 검색
  - **타입 기반 검색**: 타입으로 데이터 필터링
  - **범위 검색**: 레벨, 가격 등 범위로 검색
- **제약사항**: 
  - 검색 성능 최적화
  - 메모리 사용량 최소화
  - 검색 결과 캐싱
- **수락 기준**: 
  - 검색이 빠르고 정확함
  - 필터링이 정확히 작동함
  - 정렬이 올바르게 수행됨

### REQ-022: 데이터 저장/로드
- **설명**: 게임 진행 데이터의 저장 및 로드
- **입력**: 
  - 플레이어 진행 데이터
  - 포켓몬 파티 데이터
  - 인벤토리 데이터
  - 설정 데이터
- **출력**: 
  - 저장된 데이터 파일
  - 로드된 게임 상태
  - 데이터 무결성 검증
- **저장 데이터**: 
  - **플레이어 정보**: 이름, 레벨, 골드
  - **포켓몬 파티**: 소유 포켓몬, 레벨, 경험치
  - **인벤토리**: 소유 아이템, 수량
  - **진행도**: 스토리 진행, 도감 완성도
  - **설정**: 음향, 그래픽, 조작 설정
- **제약사항**: 
  - 데이터 암호화 (선택사항)
  - 저장 실패 시 안전한 복구
  - 버전 호환성 유지
- **수락 기준**: 
  - 데이터가 정확히 저장됨
  - 로드 시 데이터 무결성이 보장됨
  - 저장/로드 실패 시 적절한 오류 처리

### REQ-023: 에디터 툴
- **설명**: 데이터 관리를 위한 에디터 툴 제공
- **입력**: 
  - 에디터에서의 데이터 입력
  - 데이터 검증 요청
  - 데이터 내보내기/가져오기
- **출력**: 
  - 데이터 검증 결과
  - 에디터 UI 업데이트
  - 데이터 파일 생성
- **에디터 기능**: 
  - **데이터 편집**: 포켓몬, 기술, 아이템 편집
  - **데이터 검증**: 데이터 무결성 검사
  - **일괄 처리**: 여러 데이터 일괄 수정
  - **데이터 내보내기**: JSON, CSV 형태로 내보내기
  - **데이터 가져오기**: 외부 데이터 가져오기
- **제약사항**: 
  - 에디터에서만 실행
  - 데이터 검증 필수
  - 백업 생성 권장
- **수락 기준**: 
  - 에디터 툴이 정상 작동함
  - 데이터 검증이 정확함
  - 일괄 처리가 효율적으로 작동함

---

## 상세 설계

### 클래스 구조

#### DataManager (데이터 관리)
```csharp
public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    
    [Header("Data References")]
    public PokemonDatabase pokemonDatabase;
    public MoveDatabase moveDatabase;
    public ItemDatabase itemDatabase;
    public NPCDatabase npcDatabase;
    public TypeDatabase typeDatabase;
    
    [Header("Save Data")]
    public PlayerSaveData playerSaveData;
    public string saveFileName = "savegame.json";
    
    private Dictionary<int, PokemonSO> pokemonLookup;
    private Dictionary<int, MoveSO> moveLookup;
    private Dictionary<int, ItemSO> itemLookup;
    private Dictionary<int, NPCSO> npcLookup;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDatabases();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void InitializeDatabases()
    {
        // 데이터베이스 초기화
        pokemonLookup = new Dictionary<int, PokemonSO>();
        moveLookup = new Dictionary<int, MoveSO>();
        itemLookup = new Dictionary<int, ItemSO>();
        npcLookup = new Dictionary<int, NPCSO>();
        
        // 데이터 로드
        LoadPokemonData();
        LoadMoveData();
        LoadItemData();
        LoadNPCData();
    }
    
    public PokemonSO GetPokemon(int id)
    {
        return pokemonLookup.ContainsKey(id) ? pokemonLookup[id] : null;
    }
    
    public List<PokemonSO> GetPokemonByType(PokemonType type)
    {
        return pokemonDatabase.pokemonList.Where(p => p.Types.Contains(type)).ToList();
    }
    
    public void SaveGame()
    {
        // 게임 저장
        string json = JsonUtility.ToJson(playerSaveData, true);
        File.WriteAllText(GetSavePath(), json);
    }
    
    public void LoadGame()
    {
        // 게임 로드
        if (File.Exists(GetSavePath()))
        {
            string json = File.ReadAllText(GetSavePath());
            playerSaveData = JsonUtility.FromJson<PlayerSaveData>(json);
        }
    }
}
```

#### PokemonDatabase (포켓몬 데이터베이스)
```csharp
[CreateAssetMenu(fileName = "Pokemon Database", menuName = "Pokemon/Database/Pokemon")]
public class PokemonDatabase : ScriptableObject
{
    [Header("Pokemon List")]
    public List<PokemonSO> pokemonList;
    
    [Header("Search Settings")]
    public bool enableSearchCache = true;
    
    private Dictionary<int, PokemonSO> idLookup;
    private Dictionary<string, PokemonSO> nameLookup;
    
    public void Initialize()
    {
        idLookup = new Dictionary<int, PokemonSO>();
        nameLookup = new Dictionary<string, PokemonSO>();
        
        foreach (var pokemon in pokemonList)
        {
            idLookup[pokemon.ID] = pokemon;
            nameLookup[pokemon.Name] = pokemon;
        }
    }
    
    public PokemonSO GetPokemon(int id)
    {
        return idLookup.ContainsKey(id) ? idLookup[id] : null;
    }
    
    public PokemonSO GetPokemon(string name)
    {
        return nameLookup.ContainsKey(name) ? nameLookup[name] : null;
    }
    
    public List<PokemonSO> SearchPokemon(PokemonSearchCriteria criteria)
    {
        var results = pokemonList.AsEnumerable();
        
        if (criteria.Type != PokemonType.None)
        {
            results = results.Where(p => p.Types.Contains(criteria.Type));
        }
        
        if (criteria.MinLevel > 0)
        {
            results = results.Where(p => p.Level >= criteria.MinLevel);
        }
        
        if (criteria.MaxLevel > 0)
        {
            results = results.Where(p => p.Level <= criteria.MaxLevel);
        }
        
        return results.ToList();
    }
}
```

#### SaveLoadManager (저장/로드 관리)
```csharp
public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    
    [Header("Save Settings")]
    public string saveDirectory = "Saves";
    public string saveFileExtension = ".json";
    public bool encryptSaveData = false;
    
    private string GetSaveDirectory()
    {
        string path = Path.Combine(Application.persistentDataPath, saveDirectory);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }
    
    public void SaveGame(string fileName)
    {
        try
        {
            PlayerSaveData saveData = CreateSaveData();
            string json = JsonUtility.ToJson(saveData, true);
            
            if (encryptSaveData)
            {
                json = EncryptData(json);
            }
            
            string filePath = Path.Combine(GetSaveDirectory(), fileName + saveFileExtension);
            File.WriteAllText(filePath, json);
            
            Debug.Log($"Game saved to: {filePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Save failed: {e.Message}");
        }
    }
    
    public PlayerSaveData LoadGame(string fileName)
    {
        try
        {
            string filePath = Path.Combine(GetSaveDirectory(), fileName + saveFileExtension);
            
            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"Save file not found: {filePath}");
                return null;
            }
            
            string json = File.ReadAllText(filePath);
            
            if (encryptSaveData)
            {
                json = DecryptData(json);
            }
            
            PlayerSaveData saveData = JsonUtility.FromJson<PlayerSaveData>(json);
            return saveData;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Load failed: {e.Message}");
            return null;
        }
    }
    
    private PlayerSaveData CreateSaveData()
    {
        // 현재 게임 상태를 저장 데이터로 변환
        return new PlayerSaveData
        {
            playerName = GameManager.Instance.PlayerName,
            playerLevel = GameManager.Instance.PlayerLevel,
            gold = GameManager.Instance.Gold,
            pokemonParty = PartyManager.Instance.GetPartyData(),
            inventory = InventoryManager.Instance.GetInventoryData(),
            gameProgress = GameManager.Instance.GetProgressData(),
            settings = SettingsManager.Instance.GetSettingsData()
        };
    }
}
```

#### DataValidator (데이터 검증)
```csharp
public static class DataValidator
{
    public static ValidationResult ValidatePokemonData(PokemonSO pokemon)
    {
        var result = new ValidationResult();
        
        // ID 검증
        if (pokemon.ID <= 0)
        {
            result.AddError("Pokemon ID must be greater than 0");
        }
        
        // 이름 검증
        if (string.IsNullOrEmpty(pokemon.Name))
        {
            result.AddError("Pokemon name cannot be empty");
        }
        
        // 스탯 검증
        if (pokemon.BaseStats.HP <= 0)
        {
            result.AddError("Pokemon HP must be greater than 0");
        }
        
        // 타입 검증
        if (pokemon.Types == null || pokemon.Types.Length == 0)
        {
            result.AddError("Pokemon must have at least one type");
        }
        
        return result;
    }
    
    public static ValidationResult ValidateMoveData(MoveSO move)
    {
        var result = new ValidationResult();
        
        // ID 검증
        if (move.ID <= 0)
        {
            result.AddError("Move ID must be greater than 0");
        }
        
        // 이름 검증
        if (string.IsNullOrEmpty(move.Name))
        {
            result.AddError("Move name cannot be empty");
        }
        
        // 위력 검증
        if (move.Power < 0)
        {
            result.AddError("Move power cannot be negative");
        }
        
        // 명중률 검증
        if (move.Accuracy < 0 || move.Accuracy > 100)
        {
            result.AddError("Move accuracy must be between 0 and 100");
        }
        
        return result;
    }
}
```

### 데이터 구조

#### PlayerSaveData (플레이어 저장 데이터)
```csharp
[System.Serializable]
public class PlayerSaveData
{
    [Header("Player Info")]
    public string playerName;
    public int playerLevel;
    public int gold;
    public int experience;
    
    [Header("Pokemon Data")]
    public List<PokemonSaveData> pokemonParty;
    public List<PokemonSaveData> pokemonBoxes;
    
    [Header("Inventory Data")]
    public List<ItemSaveData> inventory;
    
    [Header("Game Progress")]
    public GameProgressData gameProgress;
    
    [Header("Settings")]
    public SettingsData settings;
    
    [Header("Save Info")]
    public string saveDate;
    public string gameVersion;
    public int saveSlot;
}
```

#### PokemonSaveData (포켓몬 저장 데이터)
```csharp
[System.Serializable]
public class PokemonSaveData
{
    public int pokemonID;
    public int level;
    public int currentHP;
    public int experience;
    public StatusEffect statusEffect;
    public List<int> moveIDs;
    public int friendship;
    public bool isShiny;
    public string nickname;
}
```

#### ItemSaveData (아이템 저장 데이터)
```csharp
[System.Serializable]
public class ItemSaveData
{
    public int itemID;
    public int quantity;
    public bool isFavorite;
}
```

#### GameProgressData (게임 진행 데이터)
```csharp
[System.Serializable]
public class GameProgressData
{
    public int currentStoryChapter;
    public List<int> completedQuests;
    public List<int> defeatedNPCs;
    public List<int> capturedPokemon;
    public int pokedexCompletion;
    public List<string> unlockedAreas;
    public int playTime;
}
```

#### SettingsData (설정 데이터)
```csharp
[System.Serializable]
public class SettingsData
{
    [Header("Audio")]
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    
    [Header("Graphics")]
    public int resolutionWidth;
    public int resolutionHeight;
    public bool fullscreen;
    public int qualityLevel;
    
    [Header("Controls")]
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode runKey;
    public KeyCode interactKey;
    
    [Header("Gameplay")]
    public bool autoSave;
    public int autoSaveInterval;
    public bool showDamageNumbers;
    public bool skipAnimations;
}
```

### 이벤트 흐름

#### 데이터 로드 플로우
1. **게임 시작**: `DataManager` 초기화
2. **데이터베이스 로드**: ScriptableObject 데이터 로드
3. **인덱스 생성**: 검색용 인덱스 생성
4. **저장 데이터 로드**: 플레이어 진행 데이터 로드
5. **게임 상태 복원**: 로드된 데이터로 게임 상태 복원

#### 데이터 저장 플로우
1. **저장 요청**: 플레이어가 저장 요청
2. **데이터 수집**: 현재 게임 상태 수집
3. **데이터 검증**: 저장 데이터 유효성 검사
4. **암호화**: 필요시 데이터 암호화
5. **파일 저장**: JSON 형태로 파일 저장
6. **완료 알림**: 저장 완료 알림

---

## 구현 가이드

### 기술적 고려사항

#### ScriptableObject 사용
- **장점**: 에디터에서 편집 가능, 메모리 효율적
- **단점**: 런타임 수정 불가, 참조 관리 복잡
- **해결책**: 에디터 툴로 데이터 관리, 참조 검증

#### 데이터 검색 최적화
- **인덱스 사용**: 자주 검색하는 필드에 인덱스 생성
- **캐싱**: 검색 결과 캐싱으로 성능 향상
- **지연 로딩**: 필요할 때만 데이터 로드

#### 저장/로드 시스템
- **JSON 사용**: 가독성과 호환성
- **암호화**: 치트 방지를 위한 데이터 암호화
- **버전 관리**: 저장 데이터 버전 호환성

### 성능 요구사항

#### 메모리 사용량
- **목표**: 200MB 이하
- **최적화**: 
  - 불필요한 데이터 로드 방지
  - 메모리 풀링 사용
  - 가비지 컬렉션 최소화

#### 로딩 시간
- **목표**: 3초 이내
- **최적화**: 
  - 비동기 로딩
  - 진행률 표시
  - 우선순위 로딩

### 테스트 케이스

#### 기능 테스트
1. **데이터 검색 테스트**
   - ID 기반 검색이 정확한지 확인
   - 이름 기반 검색이 정확한지 확인
   - 필터링이 정확히 작동하는지 확인

2. **저장/로드 테스트**
   - 데이터가 정확히 저장되는지 확인
   - 로드 시 데이터 무결성이 보장되는지 확인
   - 저장/로드 실패 시 적절한 오류 처리가 되는지 확인

3. **데이터 검증 테스트**
   - 데이터 검증이 정확히 작동하는지 확인
   - 잘못된 데이터가 감지되는지 확인
   - 검증 오류가 적절히 보고되는지 확인

#### 성능 테스트
1. **메모리 테스트**
   - 메모리 사용량이 목표치 이하인지 확인
   - 메모리 누수 없음 확인

2. **로딩 시간 테스트**
   - 로딩 시간이 목표치 이하인지 확인
   - 로딩 중 프레임 드롭 없음 확인

#### 통합 테스트
1. **다른 시스템과의 연동**
   - 모든 시스템이 데이터를 정확히 참조하는지 확인
   - 데이터 변경 시 시스템 간 동기화가 되는지 확인

---

## 참고사항

### 관련 파일
- `DataManager.cs`: 데이터 관리
- `PokemonDatabase.cs`: 포켓몬 데이터베이스
- `SaveLoadManager.cs`: 저장/로드 관리
- `DataValidator.cs`: 데이터 검증
- `PlayerSaveData.cs`: 플레이어 저장 데이터
- `PokemonSaveData.cs`: 포켓몬 저장 데이터

### 의존성
- **모든 게임 시스템**: 데이터 참조
- **Unity JsonUtility**: JSON 직렬화
- **Unity ScriptableObject**: 데이터 저장

### 향후 확장 계획
1. **클라우드 저장**: 온라인 저장소 연동
2. **데이터 동기화**: 멀티플레이어 데이터 동기화
3. **데이터 분석**: 플레이어 행동 데이터 분석
4. **모드 지원**: 커스텀 데이터 모드 지원
5. **데이터 백업**: 자동 백업 시스템

### 버그 대응
- **데이터 손실**: 자동 백업 및 복구 시스템
- **참조 오류**: 참조 검증 및 복구
- **성능 이슈**: 데이터 최적화 및 캐싱
- **호환성 문제**: 버전 관리 및 마이그레이션

---

**문서 끝**
