using Pokemon3D.ScriptableObj.PokemonMovementBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.BattleSystem.Unit
{
	public class PokemonUnit : MonoBehaviour, IPokemonUnitEventSource
	{
        [Header("references")]
        [SerializeField] PokemonActionController pokemonAnim;
        [SerializeField] Transform modelParent;

        [Header("references")]
        [SerializeField] bool isPlayerUnit;
        // variables
        GameObject pokemonModel;

        // events
        public event Action OnSpawn;
        public event Action OnReturn;
        public event Action OnIdle;
        public event Action<List<PokemonBehaviour>, Transform> OnAttack;
        public event Action OnHit;
        public event Action OnDie;
        public event Action OnItemUse;

        public void ClearEvent()
        {
            OnSpawn = null;
            OnReturn = null;
            OnIdle = null;
            OnAttack = null;
            OnHit = null;
            OnDie = null;
            OnItemUse = null;
        }

        public void DoAttack(List<PokemonBehaviour> pokemonBehaviours, Transform target)
        {
            OnAttack?.Invoke(pokemonBehaviours, target);
        }

        public void DoDie()
        {
            OnDie?.Invoke();
        }

        public void DoHit()
        {
            OnHit?.Invoke();
        }

        public void DoIdle()
        {
            OnIdle?.Invoke();
        }

        public void DoItemUse()
        {
            OnItemUse?.Invoke();
        }

        public void DoReturn()
        {
            OnReturn?.Invoke();
        }

        public void DoSpawn()
        {
            OnSpawn?.Invoke();
        }

        public void Initialize()
        {
            pokemonAnim.Initialize(this);
            if (isPlayerUnit)
                InstantiateModel(BattleSystem.Instance.PlayerPokemon.Base.Model, false);
            else
                InstantiateModel(BattleSystem.Instance.EnemyPokemon.Base.Model, BattleSystem.Instance.IsWildBattle);
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
            pokemonAnim.SetAnimatorFromModel(this.pokemonModel);
        }
    }
}