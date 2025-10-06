using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.Pokemon;
using Pokemon3D.Singleton;
using UnityEngine.SceneManagement;
using Pokemon3D.Enum;

namespace Pokemon3D.Core
{
    public class GameFlowManager : Singleton<GameFlowManager>
    {
        // variables
        PokemonData enemyPokemon;
        BattleOpponentType battleType;

        // properties
        public PokemonData EnemyPokemon => enemyPokemon;
        public BattleOpponentType BattleType => battleType;

        public void StartBattle(PokemonData enemyPokemonData, BattleOpponentType battleType)
        {
            enemyPokemon = enemyPokemonData;
            this.battleType = battleType;
            SceneManager.LoadScene("Battle Scene");
        }

        public void EndBattle()
        {
            enemyPokemon = null;
            SceneManager.LoadScene("Trip Scene");
        }
    }
}