using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.BattleSystem.UI
{
    public class GagueBar : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] RectTransform valueBar;

        // variables
        Vector3 value;

        public virtual void Initialize(int currentValue, int maxValue)
        {
            SetValue(currentValue, maxValue);
        }

        public virtual void SetValue(int currentValue, int maxValue)
        {
            if (maxValue < currentValue || maxValue == 0) return;
            if (currentValue < 0) currentValue = 0;
            SetValue((float)currentValue / maxValue);
        }

        private void SetValue(float percent)
        {
            value = valueBar.localScale;
            value.x = percent;
            valueBar.localScale = value;
        }
    }
}