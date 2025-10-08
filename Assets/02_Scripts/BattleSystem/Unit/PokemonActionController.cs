using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.ScriptableObj.PokemonMovementBehaviour;
using System;

namespace Pokemon3D.BattleSystem.Unit
{
    public class PokemonActionController : MonoBehaviour
    {
        [Header("references")] 
        [SerializeField] Animator modelAnim;

        // variables
        // 애니메이션 파라미터 상수
        private readonly string attackLeftTrigger = "AttackLeft";
        private readonly string attackRightTrigger = "AttackRight";
        private readonly string gunLeftTrigger = "GunLeft";
        private readonly string gunRightTrigger = "GunRight";
        private readonly string rageTrigger = "Rage";
        private readonly string hitTrigger = "Hit";
        private readonly string dieTrigger = "Die";
        private readonly string isMovingParameter = "IsMoving";
        private bool isActionAnimationComplete = true;

        // properties
        public bool IsActionAnimationComplete => isActionAnimationComplete;

        public void PlayAttack(bool isLeft)
        {
            if (isLeft)
                modelAnim.SetTrigger(attackLeftTrigger);
            else
                modelAnim.SetTrigger(attackRightTrigger);
            isActionAnimationComplete = false;
        }

        public void PlayGun(bool isLeft)
        {
            if (isLeft)
                modelAnim.SetTrigger(gunLeftTrigger);
            else
                modelAnim.SetTrigger(gunRightTrigger);
            isActionAnimationComplete = false;
        }

        public void PlayRage()
        {
            modelAnim.SetTrigger(rageTrigger);
            isActionAnimationComplete = false;
        }

        public void PlayHit()
        {
            modelAnim.SetTrigger(hitTrigger);
            isActionAnimationComplete = false;
        }

        public void PlayDie()
        {
            modelAnim.SetTrigger(dieTrigger);
            isActionAnimationComplete = false;
        }

        public void PlayMove(bool isMoving)
        {
            modelAnim.SetBool(isMovingParameter, isMoving);
        }

        public void Move(Vector3 direction)
        {
            transform.position += direction * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        public void SetActionAnimationComplete()
        {
            isActionAnimationComplete = true;
        }
    }
}