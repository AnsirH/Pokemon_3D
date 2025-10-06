using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pokemon3D.Enum;
using System;

namespace Pokemon3D.BattleSystem.UI
{
    public class TextArea : MonoBehaviour
    {
        [Header("references")]
        [SerializeField] GameObject textAreaObject;
        [SerializeField] TMP_Text text;

        // variables
        float duration = 2.0f;

        // properties
        public bool IsShowing => textAreaObject.activeSelf;

        Coroutine currentCoroutine;

        void ShowText()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ShowTextCoroutine(duration));
        }

        private IEnumerator ShowTextCoroutine(float duration)
        {
            textAreaObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            textAreaObject.SetActive(false);
            currentCoroutine = null;
        }

        // public methods
        public void SetShowingDuration(float duration)
        {
            this.duration = duration;
        }

        /// <summary>
        /// 통합된 텍스트 표시 메서드 - Facade 패턴 적용
        /// </summary>
        /// <param name="textType">표시할 텍스트 타입</param>
        /// <param name="parameters">텍스트 포맷에 필요한 매개변수들</param>
        public void ShowText(BattleTextType textType, params object[] parameters)
        {
            switch (textType)
            {
                case BattleTextType.WildStart:
                    text.text = string.Format(BattleTextManager.Instance.Get(textType), BattleSystem.Instance.EnemyPokemon.Base.Name);
                    break;

                case BattleTextType.NpcStart:
                    text.text = string.Format(BattleTextManager.Instance.Get(textType), BattleSystem.Instance.EnemyPokemon.Base.Name);
                    break;

                case BattleTextType.Spawn:
                    text.text = string.Format(BattleTextManager.Instance.Get(textType), (string)parameters[0]);
                    break;

                case BattleTextType.PlayerAttack:
                    text.text = string.Format(BattleTextManager.Instance.Get(textType), (string)parameters[0], (string)parameters[1]);
                    break;

                case BattleTextType.WildEnemyAttack:
                    text.text = string.Format(BattleTextManager.Instance.Get(textType), (string)parameters[0], (string)parameters[1]);
                    break;

                case BattleTextType.NpcEnemyAttack:
                    text.text = string.Format(BattleTextManager.Instance.Get(textType), (string)parameters[0], (string)parameters[1]);
                    break;

                case BattleTextType.Ineffective:
                    text.text = BattleTextManager.Instance.Get(textType);
                    break;

                case BattleTextType.Effective:
                    text.text = BattleTextManager.Instance.Get(textType);
                    break;

                case BattleTextType.Critical:
                    text.text = BattleTextManager.Instance.Get(textType);
                    break;

                case BattleTextType.WildEnemyFaint:
                    text.text = string.Format(BattleTextManager.Instance.Get(textType), (string)parameters[0]);
                    break;

                case BattleTextType.NpcEnemyFaint:
                    text.text = string.Format(BattleTextManager.Instance.Get(textType), (string)parameters[0]);
                    break;

                case BattleTextType.RewardExp:
                    text.text = string.Format(BattleTextManager.Instance.Get(textType), (string)parameters[0], (string)parameters[1]);
                    break;

                case BattleTextType.Levelup:
                    text.text = string.Format(BattleTextManager.Instance.Get(textType), (string)parameters[0], (string)parameters[1]);
                    break;

                default:
                    Debug.LogWarning($"Unknown BattleTextType: {textType}");
                    break;
            }
            ShowText();
        }
    }
}