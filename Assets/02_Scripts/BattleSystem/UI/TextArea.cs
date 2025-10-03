using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pokemon3D.Enum;

namespace Pokemon3D.BattleSystem.UI
{
    public class TextArea : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] GameObject textAreaObject;
        [SerializeField] TMP_Text text;

        // variables
        readonly string wildBattleStartText = "야생의 {0}(이)가 나타났다!";
        readonly string npcBattleStartText = "{0}(이)가 승부를 걸어왔다!";
        readonly string spawnPokemonText = "가랏! {0}!";
        readonly string playerPokemonAttackText = "{0}의 {1} 공격!";
        readonly string wildEnemyPokemonAttackText = "야생 {0}의 {1} 공격!";
        readonly string npcEnemyPokemonAttackText = "상대 {0}의 {1} 공격!";
        readonly string ineffectiveText = "효과가 별로인 듯 하다..";
        readonly string effectiveText = "효과가 굉장했다!";

        Coroutine currentCoroutine;

        public void ShowText(BattleTextCase textCase, float duration)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ShowTextCoroutine(textCase, duration));
        }

        private IEnumerator ShowTextCoroutine(BattleTextCase textCase, float duration)
        {
            switch (textCase)
            {
                case BattleTextCase.WildBattleStart:
                    text.text = string.Format(wildBattleStartText, BattleSystem.Instance.EnemyPokemon.Base.Name);
                    break;
                case BattleTextCase.NpcBattleStart:
                    text.text = string.Format(npcBattleStartText, BattleSystem.Instance.EnemyPokemon.Base.Name);
                    break;
                case BattleTextCase.SpawnPokemon:
                    text.text = string.Format(spawnPokemonText, BattleSystem.Instance.PlayerPokemon.Base.Name);
                    break;
                case BattleTextCase.PlayerPokemonAttack:
                    text.text = string.Format(playerPokemonAttackText, BattleSystem.Instance.PlayerPokemon.Base.Name);
                    break;
                case BattleTextCase.WildEnemyPokemonAttack:
                    text.text = string.Format(wildEnemyPokemonAttackText, BattleSystem.Instance.EnemyPokemon.Base.Name);
                    break;
                case BattleTextCase.NpcEnemyPokemonAttack:
                    text.text = string.Format(npcEnemyPokemonAttackText, BattleSystem.Instance.EnemyPokemon.Base.Name);
                    break;
                case BattleTextCase.Ineffective:
                    text.text = ineffectiveText;
                    break;
                case BattleTextCase.Effective:
                    text.text = effectiveText;
                    break;
            }
            textAreaObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            textAreaObject.SetActive(false);
        }
    }
}