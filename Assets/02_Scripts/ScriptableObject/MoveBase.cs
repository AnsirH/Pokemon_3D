using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.Enum;
using Pokemon3D.BattleSystem;

namespace Pokemon3D.ScriptableObj
{
    [CreateAssetMenu(fileName = "NewMoveBase", menuName = "SO/Create Move Base")]
    public class MoveBase : ScriptableObject
    {
        [Header("기본 정보")]
        public string MoveName;                     // 기술 이름
        [TextArea] public string Description;       // 기술 설명(도감 등에서 보여줄 텍스트)
        public PokemonType Type;                    // 기술 타입 (불, 물, 풀 등)
        public MoveCategory Category;               // 기술 카테고리 (Physical: 물리 / Special: 특수 / Status: 상태)
        public int Power;                           // 기술 위력 (Physical/Special 기술에만 적용)
        [Range(0, 100)] public int Accuracy;        // 명중률 (0~100%, 0이면 절대 안맞음, 100이면 기본적으로 적중)
        public int PP;                              // 기술 사용 가능 횟수
        [Range(-1, 1)] public int Priority;          // 기술 우선도 (턴 순서 결정용, 숫자가 높을수록 먼저 행동)

        [Header("부가 효과")]
        public StatusCondition InflictStatus;       // 기술이 부여할 상태 이상 (Sleep, Paralysis 등)
        [Range(0f, 1f)] public float StatusChance;  // 상태 이상이 발생할 확률 (0~1)
        public List<StatChange> StatChanges;        // 기술로 인한 스탯 변화 리스트 (공격력 감소, 방어력 증가 등)

        [Header("대상/리소스")]
        public MoveTarget Target;                   // 기술 대상 범위 (Single: 단일, AllEnemies: 상대 전체, Self: 자기 자신 등)
        public GameObject EffectAnimation;          // 기술 사용 시 재생할 이펙트(프리팹)
        public AudioClip SoundEffect;               // 기술 사용 시 재생할 사운드

    }
}