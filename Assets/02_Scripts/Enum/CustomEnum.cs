namespace Pokemon3D.Enum
{
    /// <summary>
    /// 포켓몬의 타입을 정의하는 열거형
    /// </summary>
    public enum PokemonType
    {
        None,       // 타입 없음
        Normal,     // 노말 타입
        Fire,       // 불꽃 타입
        Water,      // 물 타입
        Grass,      // 풀 타입
        Electric,   // 전기 타입
        Ice,        // 얼음 타입
        Fighting,   // 격투 타입
        Poison,     // 독 타입
        Ground,     // 땅 타입
        Flying,     // 비행 타입
        Psychic,    // 에스퍼 타입
        Bug,        // 벌레 타입
        Rock,       // 바위 타입
        Ghost,      // 고스트 타입
        Dragon,     // 드래곤 타입
        Dark,       // 악 타입
        Steel       // 강철 타입
    }

    /// <summary>
    /// 기술의 카테고리를 정의하는 열거형
    /// </summary>
    public enum MoveCategory
    {
        Physical,   // 물리 기술 (공격력 기반)
        Special,    // 특수 기술 (특수공격력 기반)
        Status      // 변화 기술 (상태 변화, 버프/디버프)
    }

    /// <summary>
    /// 기술의 타겟을 정의하는 열거형
    /// </summary>
    public enum MoveTarget
    {
        Single,     // 단일 대상 (상대 포켓몬 1마리)
        AllEnemies, // 모든 적 (상대 파티 전체)
        Self        // 자신 (자신에게 적용)
    }

    /// <summary>
    /// 포켓몬의 상태 이상을 정의하는 열거형
    /// </summary>
    public enum StatusCondition
    {
        None,       // 상태 이상 없음
        Sleep,      // 수면 (턴을 건너뜀)
        Paralysis,  // 마비 (이동 실패 확률, 속도 감소)
        Burn,       // 화상 (매 턴 체력 감소, 물리 공격력 감소)
        Freeze,     // 얼음 (턴을 건너뜀, 얼음 타입 기술으로 해제 가능)
        Poison,     // 독 (매 턴 체력 감소)
        Confusion   // 혼란 (자신을 공격할 확률)
    }

    public enum StatType
    {
        Attack,           // 물리 공격력
        Defense,          // 물리 방어력
        SpecialAttack,    // 특수 공격력
        SpecialDefense,   // 특수 방어력
        Speed,            // 스피드
        Accuracy,         // 명중률
        Evasion           // 회피율
    }

    //public enum GameFlow
    //{
    //    MoveAround,     // 플레이어가 맵을 돌아다니는 상태
    //    Battle,         // 배틀이 시작된 상태
    //    Portal          // 다른 맵으로 전환되는 상태
    //}

    public enum BattleState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Processing,
        Result,
        End
    }

    public enum BattleOpponentType
    {
        // 야생
        Wild,
        // 트레이너
        Trainer
    }

    public enum ActionType
    {
        None,
        Attack,
        Item,
        Switch,
        Run
    }

    /// <summary>
    /// 배틀 중 표시되는 텍스트 타입을 정의하는 열거형
    /// </summary>
    public enum BattleTextType
    {
        WildStart,          // 야생 포켓몬 등장 텍스트
        NpcStart,           // 트레이너 포켓몬 등장 텍스트
        Spawn,              // 플레이어 포켓몬 스폰 텍스트
        PlayerAttack,       // 플레이어 포켓몬 공격 텍스트
        WildEnemyAttack,    // 적 포켓몬 공격 텍스트
        NpcEnemyAttack,     // 적 포켓몬 공격 텍스트
        Ineffective,        // 효과가 별로인 텍스트
        Effective,          // 효과가 굉장한 텍스트
        Critical,           // 급소 공격 텍스트
        PlayerFaint,        // 플레이어 포켓몬 기절 텍스트
        WildEnemyFaint,     // 야생 포켓몬 기절 텍스트
        NpcEnemyFaint,      // 트레이너 포켓몬 기절 텍스트
        RewardExp,          // 경험치 획득
        Levelup,            // 레벨 업
        Buff,               // 버프
        Debuff,             // 디버프
    }
}