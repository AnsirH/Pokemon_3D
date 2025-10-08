using Pokemon3D.BattleSystem.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.ScriptableObj.PokemonMovementBehaviour
{
    [CreateAssetMenu(fileName = "NewIdleBehaviour", menuName = "SO/PokemonBehaviour/Create Idle Behaviour")]
    public class IdleBehaviour : PokemonBehaviour
    {
        public float duration;
        public override IEnumerator PlayMovement(PokemonActionController pokemonActionController, Transform target)
        {
            yield return new WaitForSeconds(duration);
        }
    }
}