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
        PokemonData pokemonData;

        // properties
        public bool IsPlayerHud => isPlayerHud;

        public void Initialize(PokemonData pokemonData)
        {
            this.pokemonData = pokemonData;
            nameText.text = pokemonData.Base.Name;
            levelText.text = $"Lv.{pokemonData.Level}";
            hpBar.SetValue(pokemonData.CurrentHp, pokemonData.MaxHP);
            if (isPlayerHud)
                expBar.SetValue(pokemonData.CurrentExp, pokemonData.RequireExpToLevelup);
        }
    }
}