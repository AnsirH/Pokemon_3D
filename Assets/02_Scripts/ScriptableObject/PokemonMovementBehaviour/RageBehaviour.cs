using Pokemon3D.BattleSystem.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.ScriptableObj.PokemonMovementBehaviour
{
    [CreateAssetMenu(fileName = "NewRageBehaviour", menuName = "SO/PokemonBehaviour/Create Rage Behaviour")]
    public class RageBehaviour : PokemonBehaviour
    {
        public override IEnumerator PlayMovement(PokemonActionController pokemonActionController, Transform target)
        {
            pokemonActionController.PlayRage();
            yield return new WaitUntil(() => pokemonActionController.IsActionAnimationComplete);
        }
    }
}

