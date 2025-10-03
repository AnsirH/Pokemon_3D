using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pokemon3D.ScriptableObj;

namespace Pokemon3D.BattleSystem.UI
{
    public class MoveButtonPanel : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] MoveButton[] moveButtons;

        public void Initialize(List<MoveBase> moves)
        {
            for (int i = 0; i < moveButtons.Length; ++i)
            {
                if (i < moves.Count)
                {
                    moveButtons[i].Initialize(moves[i]);
                    moveButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    moveButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }
}