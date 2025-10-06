using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.Singleton;
using Pokemon3D.ScriptableObj;

namespace Pokemon3D.Pokemon
{
    public class PokemonManager : Singleton<PokemonManager>
    {
        [Header("테스트")]
        [SerializeField] PokemonBase testPokemonBase;
        [SerializeField] int testPokemonLevel;

        // variables
        PokemonData[] pokemons = new PokemonData[6];

        // properties
        public PokemonData HeadPokemon
        {
            get
            {
                for (int i = 0; i < pokemons.Length; ++i)
                {
                    if (pokemons[i] != null && pokemons[i].CurrentHp > 0)
                        return pokemons[i];
                }
                return null;
            }
        }

        private void Start()
        {
            RegisterPokemon(CreatePokemonData(testPokemonBase, testPokemonLevel));
        }

        public bool RegisterPokemon(PokemonData pokemon)
        {
            for (int i = 0; i < pokemons.Length; ++i)
            {
                if (pokemons[i] == null)
                {
                    pokemons[i] = pokemon;
                    return true;
                }
            }
            return false;
        }

        public PokemonData CreatePokemonData(PokemonBase pokemonBase, int maxLevel, int minLevel)
        {
            return new PokemonData(pokemonBase, Random.Range(minLevel, maxLevel + 1));
        }

        public PokemonData CreatePokemonData(PokemonBase pokemonBase, int level)
        {
            return new PokemonData(pokemonBase, level);
        }
    }
}