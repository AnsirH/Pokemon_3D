using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.Enum;
using Pokemon3D.Singleton;
using Pokemon3D.Pokemon;
using Pokemon3D.Core;
using Pokemon3D.BattleSystem.Unit;
using Pokemon3D.BattleSystem.UI;

namespace Pokemon3D.BattleSystem
{
    [System.Serializable]
    public class StatChange
    {
        public StatType Stat;
        public int Stages;      // 증가/감소 단계
        [Range(0f, 1f)]
        public float Chance;    // 적용 확률
    }

    public class BattleSystem : Singleton<BattleSystem>
    {
        [Header("references")]
        [SerializeField] BattleCanvas canvas;
        [SerializeField] PokemonUnit playerPokemonUnit;
        [SerializeField] PokemonUnit enemyPokemonUnit;

        // variables
        PokemonData playerPokemon;
        PokemonData enemyPokemon;
        BattleState currentState;
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            playerPokemon = PokemonManager.Instance.HeadPokemon;
            enemyPokemon = GameFlowManager.Instance.EnemyPokemon;

            canvas.InitialPlayerHud(playerPokemon);
            canvas.InitialEnemyHud(enemyPokemon);

            playerPokemonUnit.InstantiateModel(playerPokemon.Base.Model);
            enemyPokemonUnit.InstantiateModel(enemyPokemon.Base.Model);
        }

        private void StartBattle()
        {
            currentState = BattleState.Start;
            if (GameFlowManager.Instance.BattleType == BattleType.Wild)
            {
                // 야생 포켓몬 등장 이펙트 실행
                
            }
        }
    }
}
