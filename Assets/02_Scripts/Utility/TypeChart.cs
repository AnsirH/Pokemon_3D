using Pokemon3D.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.Utility
{
    public static class TypeChart
    {
        // (공격 타입, 방어 타입) → 배율
        private static readonly Dictionary<(PokemonType, PokemonType), float> chart = new()
        {
            // --- Normal ---
            { (PokemonType.Normal, PokemonType.Rock), 0.5f },
            { (PokemonType.Normal, PokemonType.Steel), 0.5f },
            { (PokemonType.Normal, PokemonType.Ghost), 0f },

            // --- Fire ---
            { (PokemonType.Fire, PokemonType.Grass), 2f },
            { (PokemonType.Fire, PokemonType.Ice), 2f },
            { (PokemonType.Fire, PokemonType.Bug), 2f },
            { (PokemonType.Fire, PokemonType.Steel), 2f },
            { (PokemonType.Fire, PokemonType.Fire), 0.5f },
            { (PokemonType.Fire, PokemonType.Water), 0.5f },
            { (PokemonType.Fire, PokemonType.Rock), 0.5f },
            { (PokemonType.Fire, PokemonType.Dragon), 0.5f },

            // --- Water ---
            { (PokemonType.Water, PokemonType.Fire), 2f },
            { (PokemonType.Water, PokemonType.Ground), 2f },
            { (PokemonType.Water, PokemonType.Rock), 2f },
            { (PokemonType.Water, PokemonType.Water), 0.5f },
            { (PokemonType.Water, PokemonType.Grass), 0.5f },
            { (PokemonType.Water, PokemonType.Dragon), 0.5f },

            // --- Grass ---
            { (PokemonType.Grass, PokemonType.Water), 2f },
            { (PokemonType.Grass, PokemonType.Ground), 2f },
            { (PokemonType.Grass, PokemonType.Rock), 2f },
            { (PokemonType.Grass, PokemonType.Fire), 0.5f },
            { (PokemonType.Grass, PokemonType.Grass), 0.5f },
            { (PokemonType.Grass, PokemonType.Poison), 0.5f },
            { (PokemonType.Grass, PokemonType.Flying), 0.5f },
            { (PokemonType.Grass, PokemonType.Bug), 0.5f },
            { (PokemonType.Grass, PokemonType.Dragon), 0.5f },
            { (PokemonType.Grass, PokemonType.Steel), 0.5f },

            // --- Electric ---
            { (PokemonType.Electric, PokemonType.Water), 2f },
            { (PokemonType.Electric, PokemonType.Flying), 2f },
            { (PokemonType.Electric, PokemonType.Grass), 0.5f },
            { (PokemonType.Electric, PokemonType.Electric), 0.5f },
            { (PokemonType.Electric, PokemonType.Dragon), 0.5f },
            { (PokemonType.Electric, PokemonType.Ground), 0f },

            // --- Ice ---
            { (PokemonType.Ice, PokemonType.Grass), 2f },
            { (PokemonType.Ice, PokemonType.Ground), 2f },
            { (PokemonType.Ice, PokemonType.Flying), 2f },
            { (PokemonType.Ice, PokemonType.Dragon), 2f },
            { (PokemonType.Ice, PokemonType.Fire), 0.5f },
            { (PokemonType.Ice, PokemonType.Water), 0.5f },
            { (PokemonType.Ice, PokemonType.Ice), 0.5f },
            { (PokemonType.Ice, PokemonType.Steel), 0.5f },

            // --- Fighting ---
            { (PokemonType.Fighting, PokemonType.Normal), 2f },
            { (PokemonType.Fighting, PokemonType.Ice), 2f },
            { (PokemonType.Fighting, PokemonType.Rock), 2f },
            { (PokemonType.Fighting, PokemonType.Dark), 2f },
            { (PokemonType.Fighting, PokemonType.Steel), 2f },
            { (PokemonType.Fighting, PokemonType.Poison), 0.5f },
            { (PokemonType.Fighting, PokemonType.Flying), 0.5f },
            { (PokemonType.Fighting, PokemonType.Psychic), 0.5f },
            { (PokemonType.Fighting, PokemonType.Bug), 0.5f },
            { (PokemonType.Fighting, PokemonType.Ghost), 0f },

            // --- Poison ---
            { (PokemonType.Poison, PokemonType.Grass), 2f },
            { (PokemonType.Poison, PokemonType.Poison), 0.5f },
            { (PokemonType.Poison, PokemonType.Ground), 0.5f },
            { (PokemonType.Poison, PokemonType.Rock), 0.5f },
            { (PokemonType.Poison, PokemonType.Ghost), 0.5f },
            { (PokemonType.Poison, PokemonType.Steel), 0f },

            // --- Ground ---
            { (PokemonType.Ground, PokemonType.Fire), 2f },
            { (PokemonType.Ground, PokemonType.Electric), 2f },
            { (PokemonType.Ground, PokemonType.Poison), 2f },
            { (PokemonType.Ground, PokemonType.Rock), 2f },
            { (PokemonType.Ground, PokemonType.Steel), 2f },
            { (PokemonType.Ground, PokemonType.Grass), 0.5f },
            { (PokemonType.Ground, PokemonType.Bug), 0.5f },
            { (PokemonType.Ground, PokemonType.Flying), 0f },

            // --- Flying ---
            { (PokemonType.Flying, PokemonType.Grass), 2f },
            { (PokemonType.Flying, PokemonType.Fighting), 2f },
            { (PokemonType.Flying, PokemonType.Bug), 2f },
            { (PokemonType.Flying, PokemonType.Electric), 0.5f },
            { (PokemonType.Flying, PokemonType.Rock), 0.5f },
            { (PokemonType.Flying, PokemonType.Steel), 0.5f },

            // --- Psychic ---
            { (PokemonType.Psychic, PokemonType.Fighting), 2f },
            { (PokemonType.Psychic, PokemonType.Poison), 2f },
            { (PokemonType.Psychic, PokemonType.Psychic), 0.5f },
            { (PokemonType.Psychic, PokemonType.Steel), 0.5f },
            { (PokemonType.Psychic, PokemonType.Dark), 0f },

            // --- Bug ---
            { (PokemonType.Bug, PokemonType.Grass), 2f },
            { (PokemonType.Bug, PokemonType.Psychic), 2f },
            { (PokemonType.Bug, PokemonType.Dark), 2f },
            { (PokemonType.Bug, PokemonType.Fire), 0.5f },
            { (PokemonType.Bug, PokemonType.Fighting), 0.5f },
            { (PokemonType.Bug, PokemonType.Poison), 0.5f },
            { (PokemonType.Bug, PokemonType.Flying), 0.5f },
            { (PokemonType.Bug, PokemonType.Ghost), 0.5f },
            { (PokemonType.Bug, PokemonType.Steel), 0.5f },

            // --- Rock ---
            { (PokemonType.Rock, PokemonType.Fire), 2f },
            { (PokemonType.Rock, PokemonType.Ice), 2f },
            { (PokemonType.Rock, PokemonType.Flying), 2f },
            { (PokemonType.Rock, PokemonType.Bug), 2f },
            { (PokemonType.Rock, PokemonType.Fighting), 0.5f },
            { (PokemonType.Rock, PokemonType.Ground), 0.5f },
            { (PokemonType.Rock, PokemonType.Steel), 0.5f },

            // --- Ghost ---
            { (PokemonType.Ghost, PokemonType.Psychic), 2f },
            { (PokemonType.Ghost, PokemonType.Ghost), 2f },
            { (PokemonType.Ghost, PokemonType.Dark), 0.5f },
            { (PokemonType.Ghost, PokemonType.Normal), 0f },

            // --- Dragon ---
            { (PokemonType.Dragon, PokemonType.Dragon), 2f },
            { (PokemonType.Dragon, PokemonType.Steel), 0.5f },

            // --- Dark ---
            { (PokemonType.Dark, PokemonType.Psychic), 2f },
            { (PokemonType.Dark, PokemonType.Ghost), 2f },
            { (PokemonType.Dark, PokemonType.Fighting), 0.5f },
            { (PokemonType.Dark, PokemonType.Dark), 0.5f },

            // --- Steel ---
            { (PokemonType.Steel, PokemonType.Ice), 2f },
            { (PokemonType.Steel, PokemonType.Rock), 2f },
            { (PokemonType.Steel, PokemonType.Fire), 0.5f },
            { (PokemonType.Steel, PokemonType.Water), 0.5f },
            { (PokemonType.Steel, PokemonType.Electric), 0.5f },
            { (PokemonType.Steel, PokemonType.Steel), 0.5f }
        };

        public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
        {
            if (attackType == PokemonType.None || defenseType == PokemonType.None)
                return 1f;

            if (chart.TryGetValue((attackType, defenseType), out float value))
                return value;

            return 1f; // 상성 관계 없으면 기본값 1배
        }
    }
}