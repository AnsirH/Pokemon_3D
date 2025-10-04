using System;
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

        // variables

        // properties
        public bool IsTextAreaShowing => textArea.IsShowing;

        public void InitialPlayerHud(PokemonData pokemonData)
        {
            playerHud.Initialize(pokemonData);
            moveButtonPanel.Initialize(pokemonData.Moves);
        }

        public void InitialEnemyHud(PokemonData pokemonData)
        {
            enemyHud.Initialize(pokemonData);
        }

        public void ActiveBattleHud(bool isActive)
        {
            playerHud.gameObject.SetActive(isActive);
            enemyHud.gameObject.SetActive(isActive);
            moveButtonPanel.gameObject.SetActive(false);
        }
        public void ActiveActionButtons(bool isActive)
        {
            actionButtonsObj.SetActive(isActive);
            moveButtonPanel.gameObject.SetActive(false);
        }

        // 배틀 헤더 관련 메서드
        public void UpdatePlayerHpBar(PokemonData pokemonData)
        {
            playerHud.UpdateHpBar(pokemonData.CurrentHp, pokemonData.MaxHP);
        }
        
        public void UpdateEnemyHpBar(PokemonData pokemonData)
        {
            enemyHud.UpdateHpBar(pokemonData.CurrentHp, pokemonData.MaxHP);
        }

        // 배틀 텍스트 관련 메서드
        // 각 텍스트마다 필요한 매개변수가 달라서 각 텍스트를 처리하는 메서드를 만들어서 호출하도록 함
        public void ShowBattleStartText(bool isWildBattle)
        {
            textArea.ShowBattleStartText(isWildBattle);
        }

        public void ShowSpawnPokemonText(string pokemonName)
        {
            textArea.ShowSpawnPokemonText(pokemonName);
        }

        public void ShowPlayerPokemonAttackText(string pokemonName, string moveName)
        {
            textArea.ShowPlayerPokemonAttackText(pokemonName, moveName);
        }

        public void ShowWildEnemyPokemonAttackText(string pokemonName, string moveName, bool isWildBattle)
        {
            textArea.ShowEnemyPokemonAttackText(pokemonName, moveName, isWildBattle);
        }

        public void ShowIneffectiveText()
        {
            textArea.ShowIneffectiveText();
        }

        public void ShowEffectiveText()
        {
            textArea.ShowEffectiveText();
        }
    }
}