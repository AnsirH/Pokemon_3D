using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.BattleSystem.UI
{
    public class GaugeBar : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] RectTransform valueBar;

        // variables
        Vector3 value;
        int currentValue;
        int maxValue;

        // properties
        public int CurrentValue => currentValue;

        public virtual void SetValue(int currentValue, int maxValue)
        {
            if (maxValue < currentValue || maxValue == 0) return;
            if (currentValue < 0) currentValue = 0;
            this.currentValue = currentValue;
            this.maxValue = maxValue;
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