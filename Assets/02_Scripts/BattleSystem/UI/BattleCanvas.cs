using System.Collections;
using System.Collections.Generic;
using Pokemon3D.Enum;
using Pokemon3D.Pokemon;
using UnityEngine;

namespace Pokemon3D.BattleSystem.UI
{
    public class BattleCanvas : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] GameObject actionButtonsObj;
        [SerializeField] BattleHud playerHud;
        [SerializeField] BattleHud enemyHud;
        [SerializeField] TextArea textArea;
        [SerializeField] MoveButtonPanel moveButtonPanel;

        public void InitialPlayerHud(PokemonData pokemonData)
        {
            playerHud.Initialize(pokemonData);
            moveButtonPanel.Initialize(pokemonData.Moves);
        }

        public void InitialEnemyHud(PokemonData pokemonData)
        {
            enemyHud.Initialize(pokemonData);
        }

        public void ShowText(BattleTextCase textCase, float duration)
        {
            textArea.ShowText(textCase, duration);
        }

        public void ActiveBattleHud(bool isActive)
        {
            playerHud.gameObject.SetActive(isActive);
            enemyHud.gameObject.SetActive(isActive);
            actionButtonsObj.SetActive(isActive);
            moveButtonPanel.gameObject.SetActive(false);
        }
    }
}