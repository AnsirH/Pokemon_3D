using Pokemon3D.Core;
using Pokemon3D.Enum;
using Pokemon3D.Player;
using Pokemon3D.Pokemon;
using Pokemon3D.ScriptableObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.Surface
{
    public class WildOccurrenceSurface : SurfaceBase
    {
        [Header("Wild Occurrence")]
        [SerializeField] WildOccurrenceData wildOccurrenceData;

        public override void ExecuteSurfaceEvent(PlayerController player)
        {
            base.ExecuteSurfaceEvent(player);
            CheckWildPokemon();
        }

        private void CheckWildPokemon()
        {
            float rand = Random.Range(0, 100.0f);
            for (int i = 0; i < wildOccurrenceData.WildPokemonOccurrenceProbability.Length; ++i)
            {
                if (rand <= wildOccurrenceData.WildPokemonOccurrenceProbability[i].occurrenceProbability)
                {
                    GameFlowManager.Instance.StartBattle(
                        PokemonManager.Instance.CreatePokemonData(wildOccurrenceData.WildPokemonOccurrenceProbability[i].pokemonBase,wildOccurrenceData.MaxLevel, wildOccurrenceData.MinLevel),
                        BattleType.Wild);
                }
                else
                {
                    rand -= wildOccurrenceData.WildPokemonOccurrenceProbability[i].occurrenceProbability;
                }
            }
        }
    }
}