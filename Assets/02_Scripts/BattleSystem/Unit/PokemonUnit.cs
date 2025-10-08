using Pokemon3D.Enum;
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
        [SerializeField] PokemonActionController actionController;
        [SerializeField] Transform modelParent;
        [SerializeField] Animator unitAnim;
        [SerializeField] GameObject PokeBallObj;
        [SerializeField] Transform hitEffectPoint;
        [SerializeField] ParticleSystem buffEffect;
        [SerializeField] ParticleSystem debuffEffect;

        [Header("variables")]
        [SerializeField] bool isPlayerUnit;

        // evnets
        public event Action OnHit;

        // variables
        GameObject pokemonModel;
        PokemonData pokemonData;
        Dictionary<StatType, int> currentStat = new();
        // 애니메이션 파라미터 상수
        private readonly string spawnTrigger = "Spawn";
        private readonly string despawnTrigger = "Despawn";

        // properties
        public PokemonData PokemonData => pokemonData;
        public bool IsPlayerUnit => isPlayerUnit;
        public bool IsDead => pokemonData.CurrentHp == 0;
        public bool IsActionAnimationComplete => actionController.IsActionAnimationComplete;
        public bool IsBuffEffectComplete => (!buffEffect.isPlaying && !debuffEffect.isPlaying);
        public Transform HitEffectPoint => hitEffectPoint;

        private void Awake()
        {
            if (unitAnim == null)
                unitAnim = GetComponent<Animator>();
        }

        public void Initialize(PokemonData pokemonData)
        {
            if (isPlayerUnit)
                InstantiateModel(pokemonData.Base.Model, false);
            else
                InstantiateModel(pokemonData.Base.Model, BattleSystem.Instance.IsWildBattle);

            this.pokemonData = pokemonData;
            foreach (StatType stat in System.Enum.GetValues(typeof(StatType)))
                currentStat[stat] = 0;
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
            actionController = this.pokemonModel.GetComponentInChildren<PokemonActionController>();
            OnHit += actionController.PlayHit;
        }

        public void Spawn()
        {
            unitAnim.enabled = true;
            unitAnim.SetTrigger(spawnTrigger);
        }

        public void Despawn()
        {
            unitAnim.SetTrigger(despawnTrigger);
        }

        public void Hit(int damage)
        {
            pokemonData.CurrentHp -= damage;
            if (pokemonData.CurrentHp < 0)
                pokemonData.CurrentHp = 0;

            OnHit?.Invoke();
        }

        public IEnumerator MoveAction(MoveBase moveBase, Transform target)
        {
            for (int i = 0; i < moveBase.PokemonBehaviours.Count; i++)
            {
                yield return StartCoroutine(moveBase.PokemonBehaviours[i].PlayMovement(actionController, target));
            }
        }

        public void Die()
        {
            actionController.PlayDie();
        }

        public void ChangeStat(StatChange statChange)
        {
            if (currentStat.ContainsKey(statChange.Stat))
            {
                currentStat[statChange.Stat] += statChange.Stages;
                currentStat[statChange.Stat] = Mathf.Clamp(currentStat[statChange.Stat], -6, 6);
            }
        }

        public void PlayBuffEffect(bool isBuff)
        {
            if (isBuff)
                buffEffect.Play();
            else
                debuffEffect.Play();
        }

        public float GetStatMultiplier(StatType statType)
        {
            if (currentStat[statType] == 0)
                return 1.0f;
            else if (currentStat[statType] > 0)
                return (3 + currentStat[statType]) / 3;
            else
                return 3 / (3 + currentStat[statType]);
        }
    }
}