using Pokemon3D.BattleSystem.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.ScriptableObj.PokemonMovementBehaviour
{
    [CreateAssetMenu(fileName = "NewAttackBehaviour", menuName = "SO/PokemonBehaviour/Create Attack Behaviour")]
    public class AttackBehaviour : PokemonBehaviour
    {
        public bool IsLeft = false;
        public override IEnumerator PlayMovement(PokemonActionController pokemonActionController, Transform target)
        {
            pokemonActionController.PlayAttack(IsLeft);
            while (!pokemonActionController.IsAnimationFinished(true))
            {
                yield return null;
            }
            if (target.TryGetComponent(out PokemonActionController actionController))
                actionController.PlayHit();
        }
    }
}