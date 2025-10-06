using Pokemon3D.BattleSystem.Unit;
using Pokemon3D.Pokemon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Pokemon3D.BattleSystem.UI
{
    public class BattleHud : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] TMP_Text nameText;
        [SerializeField] TMP_Text levelText;
        [SerializeField] GaugeBar hpBar;
        [SerializeField] GaugeBar expBar;

        [Header("variables")]
        [SerializeField] bool isPlayerHud = true;

        // variables
        Coroutine currentCoroutine;
        PokemonUnit pokemonUnit;

        // properties
        public bool IsPlayerHud => isPlayerHud;
        public bool IsUpdating => currentCoroutine != null;

        public void Initialize(PokemonUnit pokemonUnit)
        {
            this.pokemonUnit = pokemonUnit;
            nameText.text = pokemonUnit.PokemonData.Base.Name;
            levelText.text = $"Lv.{pokemonUnit.PokemonData.Level}";
            hpBar.SetValue(pokemonUnit.PokemonData.CurrentHp, pokemonUnit.PokemonData.MaxHP);
            if (isPlayerHud)
                expBar.SetValue(pokemonUnit.PokemonData.CurrentExp, pokemonUnit.PokemonData.RequireExpToLevelup);

            pokemonUnit.OnHit += UpdateHpBar;
        }

        public void UpdateHpBar()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(UpdateGaugeBarCoroutine(pokemonUnit.PokemonData.CurrentHp, pokemonUnit.PokemonData.MaxHP, hpBar));
        }

        public void UpdateExpBar()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(UpdateGaugeBarCoroutine(pokemonUnit.PokemonData.CurrentExp, pokemonUnit.PokemonData.RequireExpToLevelup, expBar));
        }

        private IEnumerator UpdateGaugeBarCoroutine(int currentValue, int maxValue, GaugeBar gaugeBar)
        {
            float speed = 60f; // 초당 변화량
            float startValue = gaugeBar.CurrentValue;
            float targetValue = currentValue;

            float diff = Mathf.Abs(targetValue - startValue);
            if (diff < Mathf.Epsilon) yield break;

            float duration = diff / speed; // 변화량 / 속도 = 걸리는 시간
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                float interpolatedValue = Mathf.Lerp(startValue, targetValue, t);
                gaugeBar.SetValue(Mathf.RoundToInt(interpolatedValue), maxValue);
                yield return null;
            }

            // 마지막 값 보정
            if (currentValue != maxValue)
                gaugeBar.SetValue(currentValue, maxValue);
            else
                gaugeBar.SetValue(0, maxValue);
            currentCoroutine = null;
        }

    }
}