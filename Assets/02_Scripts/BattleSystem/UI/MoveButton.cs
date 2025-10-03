using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pokemon3D.ScriptableObj;

namespace Pokemon3D.BattleSystem.UI
{
    public class MoveButton : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI ppText;

        // variables
        MoveBase moveBase;

        public void Initialize(MoveBase moveBase)
        {
            nameText.text = moveBase.MoveName;
            ppText.text = moveBase.PP.ToString();
            this.moveBase = moveBase;
        }

        public void OnClick()
        {
            BattleSystem.Instance.SelectMove(moveBase);
        }
    }
}