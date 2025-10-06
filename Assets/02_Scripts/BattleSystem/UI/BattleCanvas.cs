using System;
using System.Collections;
using System.Collections.Generic;
using Pokemon3D.BattleSystem.Unit;
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

        // variables

        // properties
        public bool IsTextAreaShowing => textArea.IsShowing;
        public bool IsPlayerExpBarUpdating => playerHud.IsUpdating;

        public void InitialPlayerHud(PokemonUnit pokemonUnit)
        {
            playerHud.Initialize(pokemonUnit);
            moveButtonPanel.Initialize(pokemonUnit.PokemonData.Moves);
        }

        public void InitialEnemyHud(PokemonUnit pokemonUnit)
        {
            enemyHud.Initialize(pokemonUnit);
        }

        public void ActiveBattleHud(bool isActive)
        {
            playerHud.gameObject.SetActive(isActive);
            enemyHud.gameObject.SetActive(isActive);
            moveButtonPanel.gameObject.SetActive(false);
        }

        public void ActiveBattleHud(bool isActive, PokemonUnit pokemonUnit)
        {
            if (pokemonUnit.IsPlayerUnit)
                playerHud.gameObject.SetActive(isActive);
            else
                enemyHud.gameObject.SetActive(isActive);
        }

        public void ActiveActionButtons(bool isActive)
        {
            actionButtonsObj.SetActive(isActive);
            moveButtonPanel.gameObject.SetActive(false);
        }

        public bool CheckBattleHudUpdating(PokemonUnit pokemonUnit)
        {
            if (pokemonUnit.IsPlayerUnit)
                return playerHud.IsUpdating;
            else
                return enemyHud.IsUpdating;
        }

        // 배틀 텍스트 관련 메서드
        public void ShowBattleText(BattleTextType type, params object[] parameters)
        {
            textArea.ShowText(type, parameters);
        }

        public void UpdatePlayerExpBar()
        {
            playerHud.UpdateExpBar();
        }
    }
}