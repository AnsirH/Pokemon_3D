using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.Enum;
using Pokemon3D.BattleSystem;
using Pokemon3D.ScriptableObj.PokemonMovementBehaviour;
namespace Pokemon3D.ScriptableObj
{
    [CreateAssetMenu(fileName = "NewMoveBase", menuName = "SO/Create Move Base")]
    public class MoveBase : ScriptableObject
    {
        [Header("기본 정보")]
        [Tooltip("기술 이름")] 
        public string Name;
        [TextArea][Tooltip("기술 설명(도감 등에서 보여줄 텍스트)")] 
        public string Description;
        [Tooltip("기술 타입 (불, 물, 풀 등)")] 
        public PokemonType Type;
        [Tooltip("기술 카테고리 (Physical: 물리 / Special: 특수 / Status: 상태)")] 
        public MoveCategory Category;
        [Tooltip("기술 위력 (Physical/Special 기술에만 적용)")] 
        public int Power;
        [Range(0, 100)][Tooltip("명중률 (0~100%, 0이면 절대 안맞음, 100이면 기본적으로 적중)")] 
        public int Accuracy;
        [Tooltip("기술 사용 가능 횟수")] 
        public int PP;
        [Range(-1, 1)][Tooltip("기술 우선도 (턴 순서 결정용, 숫자가 높을수록 먼저 행동)")] 
        public int Priority;

        [Header("부가 효과")]
        [Tooltip("기술이 부여할 상태 이상 (Sleep, Paralysis 등)")] 
        public StatusCondition InflictStatus;
        [Range(0f, 1f)][Tooltip("상태 이상이 발생할 확률 (0~1)")] 
        public float StatusChance;
        [Tooltip("기술로 인한 스탯 변화 리스트 (공격력 감소, 방어력 증가 등)")] 
        public List<StatChange> StatChanges;

        [Header("대상/리소스")]
        [Tooltip("기술 대상 범위 (Single: 단일, AllEnemies: 상대 전체, Self: 자기 자신 등)")] 
        public MoveTarget Target;
        [Tooltip("기술 이펙트")] 
        public GameObject MoveEffectPrefab;
        [Tooltip("타격 이펙트")] 
        public GameObject HitEffectPrefab;
        [Tooltip("기술 사용 시 재생할 사운드")] 
        public AudioClip SoundEffect;

        [Header("기술 행동 정보")]
        public List<PokemonBehaviour> PokemonBehaviours;

        public bool IsDamageable => Category == MoveCategory.Physical || Category == MoveCategory.Special;
    }
}