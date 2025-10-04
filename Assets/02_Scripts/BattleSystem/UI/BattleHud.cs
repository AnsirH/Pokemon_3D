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
        [SerializeField] GagueBar hpBar;
        [SerializeField] GagueBar expBar;

        [Header("variables")]
        [SerializeField] bool isPlayerHud = true;

        // variables
        Coroutine currentCoroutine;

        // properties
        public bool IsPlayerHud => isPlayerHud;
        public bool IsUpdating => currentCoroutine != null;

        public void Initialize(PokemonData pokemonData)
        {
            nameText.text = pokemonData.Base.Name;
            levelText.text = $"Lv.{pokemonData.Level}";
            hpBar.SetValue(pokemonData.CurrentHp, pokemonData.MaxHP);
            if (isPlayerHud)
                expBar.SetValue(pokemonData.CurrentExp, pokemonData.RequireExpToLevelup);
        }

        public void UpdateHpBar(int currentHp, int maxHp)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(UpdateGagueBarCoroutine(currentHp, maxHp, hpBar));
        }

        public void UpdateExpBar(int currentExp, int requireExpToLevelup)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(UpdateGagueBarCoroutine(currentExp, requireExpToLevelup, expBar));
        }

        private IEnumerator UpdateGagueBarCoroutine(int currentValue, int maxValue, GagueBar gagueBar)
        {
            int currentBarValue = gagueBar.CurrentValue;
            int targetBarValue = currentValue;
            if (currentBarValue == targetBarValue) yield break;
            int changeValue = currentBarValue > targetBarValue ? -1 : 1;

            WaitForSeconds wait = new(Mathf.Abs((targetBarValue - currentBarValue) / maxValue * 2));
            while (currentBarValue != targetBarValue)
            {
                currentBarValue += changeValue;
                gagueBar.SetValue(currentBarValue, maxValue);
                yield return wait;
            }
            currentCoroutine = null;
        }
    }
}