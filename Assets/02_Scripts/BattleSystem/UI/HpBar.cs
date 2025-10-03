using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Pokemon3D.BattleSystem.UI
{
    public class HpBar : GagueBar
    {
        [Header("text")]
        [SerializeField] TMP_Text hpText;

        public override void SetValue(int currentValue, int maxValue)
        {
            base.SetValue(currentValue, maxValue);
            hpText.text = $"{currentValue}/{maxValue}";
        }
    }
}