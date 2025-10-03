using Pokemon3D.BattleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.BattleSystem.Unit;

namespace Pokemon3D.ScriptableObj.PokemonMovementBehaviour
{
    public abstract class PokemonBehaviour : ScriptableObject
    {
        [Header("variables")]
        public float movementSpeed;

        public abstract IEnumerator PlayMovement(PokemonActionController pokemonActionController, Transform target);
    }
}
