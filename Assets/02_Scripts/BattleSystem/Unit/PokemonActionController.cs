using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.ScriptableObj.PokemonMovementBehaviour;

namespace Pokemon3D.BattleSystem.Unit
{
    public class PokemonActionController : MonoBehaviour, IPokemonUnitSubComponent
    {
        [Header("references")]
        [SerializeField] Animator anim;

        // 애니메이션 트리거명 상수
        private readonly string attackLeftTrigger = "AttackLeft";
        private readonly string attackRightTrigger = "AttackRight";
        private readonly string gunLeftTrigger = "GunLeft";
        private readonly string gunRightTrigger = "GunRight";
        private readonly string hitTrigger = "Hit";
        private readonly string dieTrigger = "Die";
        // 이동 부울 상수
        private readonly string isMovingParameter = "IsMoving";

        public void Initialize(IPokemonUnitEventSource evenetSource)
        {
            evenetSource.OnAttack += PlayBehaviour;
        }

        public void PlayBehaviour(PokemonBehaviour behaviour, Transform target)
        {
            StartCoroutine(behaviour.PlayMovement(this, target));
        }

        public void PlayAttack(bool isLeft)
        {
            if (isLeft)
                anim.SetTrigger(attackLeftTrigger);
            else
                anim.SetTrigger(attackRightTrigger);
        }
        
        // 공격 애니메이션이 종료되었는지 여부
        public bool IsAnimationFinished(bool isLeft)
        {
            return anim.GetCurrentAnimatorStateInfo(0).IsName(isLeft ? attackLeftTrigger : attackRightTrigger);
        }

        public void PlayGun(bool isLeft)
        {
            if (isLeft)
                anim.SetTrigger(gunLeftTrigger);
            else
                anim.SetTrigger(gunRightTrigger);
        }

        // 총 애니메이션이 종료되었는지 여부
        public bool IsGunAnimationFinished(bool isLeft)
        {
            return anim.GetCurrentAnimatorStateInfo(0).IsName(isLeft ? gunLeftTrigger : gunRightTrigger);
        }

        public void PlayHit()
        {
            anim.SetTrigger(hitTrigger);
        }
        
        public void PlayDie()
        {
            anim.SetTrigger(dieTrigger);
        }

        // 죽음 애니메이션이 종료되었는지 여부
        public bool IsDieAnimationFinished()
        {
            return anim.GetCurrentAnimatorStateInfo(0).IsName(dieTrigger);
        }

        public void PlayMove(bool isMoving)
        {
            anim.SetBool(isMovingParameter, isMoving);
        }

        public void Move(Vector3 direction)
        {
            transform.position += direction * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        public void SetAnimatorFromModel(GameObject pokemonModel)
        {
            anim = pokemonModel.GetComponentInChildren<Animator>();
        }
    }
}