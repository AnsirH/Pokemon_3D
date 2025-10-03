using System.Collections;
using System.Collections.Generic;
using Pokemon3D.Pokemon;
using UnityEngine;

namespace Pokemon3D.BattleSystem.UI
{
    public class BattleCanvas : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] BattleHud playerHud;
        [SerializeField] BattleHud enemyHud;

        public void InitialPlayerHud(PokemonData pokemonData)
        {
            playerHud.Initialize(pokemonData);
        }

        public void InitialEnemyHud(PokemonData pokemonData)
        {
            enemyHud.Initialize(pokemonData);
        }
    }
}