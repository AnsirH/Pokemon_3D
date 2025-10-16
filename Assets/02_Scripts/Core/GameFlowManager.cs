using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.Pokemon;
using Pokemon3D.Singleton;
using UnityEngine.SceneManagement;
using Pokemon3D.Enum;
using Pokemon3D.Sound;

namespace Pokemon3D.Core
{
    public class GameFlowManager : Singleton<GameFlowManager>
    {
        // variables
        PokemonData enemyPokemonData;
        BattleOpponentType battleType;

        // properties
        public PokemonData EnemyPokemonData => enemyPokemonData;
        public BattleOpponentType BattleType => battleType;

        public void SetBattleData(PokemonData enemyPokemonData, BattleOpponentType battleType)
        {
            this.enemyPokemonData = enemyPokemonData;
            this.battleType = battleType;
        }

        public void MoveToBattleScene()
        {
            SceneManager.LoadScene("Battle Scene");
        }

        public void EndBattle()
        {
            enemyPokemonData = null;
            SceneManager.LoadScene("Trip Scene");
        }
    }
}