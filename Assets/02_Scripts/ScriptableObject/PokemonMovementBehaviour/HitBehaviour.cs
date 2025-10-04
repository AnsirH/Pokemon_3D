using Pokemon3D.BattleSystem.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.ScriptableObj.PokemonMovementBehaviour
{
    [CreateAssetMenu(fileName = "NewHitBehaviour", menuName = "SO/PokemonBehaviour/Create Hit Behaviour")]
    public class HitBehaviour : PokemonBehaviour
    {
        public override IEnumerator PlayMovement(PokemonActionController pokemonActionController, Transform target)
        {
            if (target.TryGetComponent(out PokemonActionController actionController))
                actionController.PlayHit();

            yield return null;
        }
    }
}