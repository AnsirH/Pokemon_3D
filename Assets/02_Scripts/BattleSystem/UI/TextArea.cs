using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pokemon3D.Enum;
using System;

namespace Pokemon3D.BattleSystem.UI
{
    public class TextArea : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] GameObject textAreaObject;
        [SerializeField] TMP_Text text;

        // variables
        float duration = 2.0f;
        readonly string wildBattleStartText = "야생의 {0}(이)가 나타났다!";
        readonly string npcBattleStartText = "{0}(이)가 승부를 걸어왔다!";
        readonly string spawnPokemonText = "가랏! {0}!";
        readonly string playerPokemonAttackText = "{0}의 {1} 공격!";
        readonly string wildEnemyPokemonAttackText = "야생 {0}의 {1} 공격!";
        readonly string npcEnemyPokemonAttackText = "상대 {0}의 {1} 공격!";
        readonly string ineffectiveText = "효과가 별로인 듯 하다..";
        readonly string effectiveText = "효과가 굉장했다!";

        // properties
        public bool IsShowing => textAreaObject.activeSelf;

        Coroutine currentCoroutine;

        void ShowText()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ShowTextCoroutine(duration));
        }

        private IEnumerator ShowTextCoroutine(float duration)
        {
            textAreaObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            textAreaObject.SetActive(false);
            currentCoroutine = null;
        }

        // public methods
        public void SetShowingDuration(float duration)
        {
            this.duration = duration;
        }


        // 각 텍스트마다 필요한 매개변수가 달라서 각 텍스트를 처리하는 메서드를 만들어서 호출하도록 함
        public void ShowBattleStartText(bool isWildBattle)
        {
            if (isWildBattle)
                text.text = string.Format(wildBattleStartText, BattleSystem.Instance.EnemyPokemon.Base.Name);
            else
                text.text = string.Format(npcBattleStartText, BattleSystem.Instance.EnemyPokemon.Base.Name);
            ShowText();
        }

        public void ShowSpawnPokemonText(string pokemonName)
        {
            text.text = string.Format(spawnPokemonText, pokemonName);
            ShowText();
        }

        public void ShowPlayerPokemonAttackText(string pokemonName, string moveName)
        {
            text.text = string.Format(playerPokemonAttackText, pokemonName, moveName);
            ShowText();
        }

        public void ShowEnemyPokemonAttackText(string pokemonName, string moveName, bool isWildBattle)
        {
            if (isWildBattle)
                text.text = string.Format(wildEnemyPokemonAttackText, pokemonName, moveName);
            else
                text.text = string.Format(npcEnemyPokemonAttackText, pokemonName, moveName);
            ShowText();
        }

        public void ShowIneffectiveText()
        {
            text.text = ineffectiveText;
            ShowText();
        }

        public void ShowEffectiveText()
        {
            text.text = effectiveText;
            ShowText();
        }
    }
}