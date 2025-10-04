using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pokemon3D.ScriptableObj;
using Pokemon3D.Pokemon;

namespace Pokemon3D.BattleSystem.UI
{
    public class MoveButton : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI ppText;

        // variables
        MoveData moveData;

        public void Initialize(MoveData moveData)
        {
            nameText.text = moveData.moveBase.MoveName;
            ppText.text = moveData.pp.ToString();
            this.moveData = moveData;
        }

        public void OnClick()
        {
            if (moveData.pp > 0)
            {
                BattleSystem.Instance.SelectPlayerMove(moveData.moveBase);
                moveData.pp--;
            }
        }
    }
}