using Pokemon3D.Pokemon;
using Pokemon3D.ScriptableObj;
using Pokemon3D.ScriptableObj.PokemonMovementBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.BattleSystem.Unit
{
	public class PokemonUnit : MonoBehaviour
	{
        [Header("references")]
        [SerializeField] PokemonActionController pokemonAction;
        [SerializeField] Transform modelParent;

        [Header("references")]
        [SerializeField] bool isPlayerUnit;

        // variables
        GameObject pokemonModel;
        PokemonData pokemonData;

        // properties
        public PokemonData PokemonData => pokemonData;

        private void Awake()
        {
            if (pokemonAction == null)
                pokemonAction = GetComponent<PokemonActionController>();
        }

        public IEnumerator MoveAction(MoveBase moveBase, Transform target)
        {
            for (int i = 0; i < moveBase.PokemonBehaviours.Count; i++)
            {
                yield return StartCoroutine(moveBase.PokemonBehaviours[i].PlayMovement(pokemonAction, target));
            }
        }

        public void Initialize(PokemonData pokemonData)
        {
            if (isPlayerUnit)
                InstantiateModel(pokemonData.Base.Model, false);
            else
                InstantiateModel(pokemonData.Base.Model, BattleSystem.Instance.IsWildBattle);

            this.pokemonData = pokemonData;
        }

        private void InstantiateModel(GameObject pokemonModel, bool isWild)
        {
            if (isWild)
                modelParent.localScale = Vector3.one;

            if (this.pokemonModel != null){
                Destroy(this.pokemonModel);
                this.pokemonModel = null;
            }
            this.pokemonModel = Instantiate(pokemonModel, modelParent);
            this.pokemonModel.transform.localPosition = Vector3.zero;
            this.pokemonModel.transform.localRotation = Quaternion.identity;
            this.pokemonModel.transform.localScale = Vector3.one;
            pokemonAction.SetAnimatorFromModel(this.pokemonModel);
        }

        public void Spawn()
        {
            pokemonAction.PlaySpawn();
        }

        public void Hit(int damage)
        {
            pokemonData.CurrentHp -= damage;
            if (pokemonData.CurrentHp < 0)
                pokemonData.CurrentHp = 0;
        }
    }
}