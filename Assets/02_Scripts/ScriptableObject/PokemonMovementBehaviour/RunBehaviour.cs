using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.BattleSystem.Unit;

namespace Pokemon3D.ScriptableObj.PokemonMovementBehaviour
{
    [CreateAssetMenu(fileName = "NewRunBehaviour", menuName = "SO/PokemonBehaviour/Create Run Behaviour")]
    public class RunBehaviour : PokemonBehaviour
    {
        public float StopDistance = 0.5f;
        public override IEnumerator PlayMovement(PokemonActionController pokemonActionController, Transform target)
        {
            pokemonActionController.PlayMove(true);
            Vector3 direction = target.position - pokemonActionController.transform.position;
            while (direction.magnitude > StopDistance)
            {
                pokemonActionController.Move(direction.normalized);
                yield return null;
                
                direction = target.position - pokemonActionController.transform.position;
            }
        }
    }
}