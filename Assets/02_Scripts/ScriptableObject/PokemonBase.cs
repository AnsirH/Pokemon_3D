using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.Enum;

namespace Pokemon3D.ScriptableObj
{
    [CreateAssetMenu(fileName = "NewPokemonBase", menuName = "SO/Create New Pokemon Base")]
    public class PokemonBase : ScriptableObject
    {
        [Header("정보")]
        public string Name;
        public PokemonType Type_1;
        public PokemonType Type_2;

        [Header("능력치")]
        public int MaxHP;
        public int Attack;
        public int Defense;
        public int SpecialAttack;
        public int SpecialDefense;
        public int Speed;

        [Header("기타 전투 데이터")]
        [Range(0, 255)] public int CatchRate;       // 포획률
        public int BaseExpYield;                    // 기본 경험치 수치

        [Header("진화")]
        public PokemonBase EvolvesTo;               // 진화 후 포켓몬

        [Header("기술")]
        public MoveBase[] LearnableMoves;

        [Header("리소스")]
        public Sprite BattleSprite;                 // 전투용 스프라이트
        public GameObject Model;                    // 3D 모델
        public AudioClip CrySound;                  // 울음소리
        public AnimatorOverrideController Animator; // 애니메이션 컨트롤러
    }
}