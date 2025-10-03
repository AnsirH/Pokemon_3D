using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.ScriptableObj
{
    [System.Serializable]
    public struct WildPokemonOccurrenceProbability
    {
        public PokemonBase pokemonBase;
        public float occurrenceProbability;
    }

    [CreateAssetMenu(fileName = "NewWildOccurrenceData", menuName = "SO/Create New Wild Occurrnece Data")]
    public class WildOccurrenceData : ScriptableObject
    {
        public WildPokemonOccurrenceProbability[] WildPokemonOccurrenceProbability;
        public int MaxLevel;
        public int MinLevel;
    }
}
