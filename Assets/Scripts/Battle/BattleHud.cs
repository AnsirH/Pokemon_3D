using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] Text currentHp;
    [SerializeField] Text maxHp;
    [SerializeField] HPBar hpBar;
    [SerializeField] bool isPlayerHud;

    Pokemon _pokemon;

    public void SetData(Pokemon pokemon)
    {
        _pokemon = pokemon;
        nameText.text = pokemon.Base.Name;
        levelText.text = pokemon.Level.ToString();
        hpBar.SetHP((float)pokemon.HP / pokemon.MaxHp);
        if (isPlayerHud)
        {
            currentHp.text = pokemon.HP.ToString();
            maxHp.text = pokemon.MaxHp.ToString();
        }
    }

    public IEnumerator UpdateHP()
    {
        yield return hpBar.SetHPSmooth((float)_pokemon.HP / _pokemon.MaxHp);

    }
}
