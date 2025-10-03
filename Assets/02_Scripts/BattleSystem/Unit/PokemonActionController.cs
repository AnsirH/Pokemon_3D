using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.ScriptableObj.PokemonMovementBehaviour;

namespace Pokemon3D.BattleSystem.Unit
{
    public class PokemonActionController : MonoBehaviour, IPokemonUnitSubComponent
    {
        [Header("references")] 
        [SerializeField] Animator unitAnim;
        [SerializeField] Animator modelAnim;
        [SerializeField] GameObject PokeBallObj;

        // 애니메이션 트리거명 상수
        private readonly string spawnTrigger = "Spawn";
        private readonly string despawnTrigger = "Despawn";
        private readonly string attackLeftTrigger = "AttackLeft";
        private readonly string attackRightTrigger = "AttackRight";
        private readonly string gunLeftTrigger = "GunLeft";
        private readonly string gunRightTrigger = "GunRight";
        private readonly string hitTrigger = "Hit";
        private readonly string dieTrigger = "Die";
        // 이동 부울 상수
        private readonly string isMovingParameter = "IsMoving";

        private void Awake()
        {
            if (unitAnim == null)
                unitAnim = GetComponent<Animator>();
        }

        public void Initialize(IPokemonUnitEventSource evenetSource)
        {
            evenetSource.OnAttack += PlayBehaviour;
            evenetSource.OnSpawn += Spawn;
        }

        public void PlayBehaviour(List<PokemonBehaviour> behaviours, Transform target)
        {
            for (int i = 0; i < behaviours.Count; ++i)
            {
                StartCoroutine(behaviours[i].PlayMovement(this, target));
            }
        }

        public void PlayAttack(bool isLeft)
        {
            if (isLeft)
                modelAnim.SetTrigger(attackLeftTrigger);
            else
                modelAnim.SetTrigger(attackRightTrigger);
        }
        
        // 공격 애니메이션이 종료되었는지 여부
        public bool IsAnimationFinished()
        {
            return modelAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        }

        public void PlayGun(bool isLeft)
        {
            if (isLeft)
                modelAnim.SetTrigger(gunLeftTrigger);
            else
                modelAnim.SetTrigger(gunRightTrigger);
        }

        // 총 애니메이션이 종료되었는지 여부
        public bool IsGunAnimationFinished(bool isLeft)
        {
            return modelAnim.GetCurrentAnimatorStateInfo(0).IsName(isLeft ? gunLeftTrigger : gunRightTrigger);
        }

        public void PlayHit()
        {
            modelAnim.SetTrigger(hitTrigger);
        }
        
        public void PlayDie()
        {
            modelAnim.SetTrigger(dieTrigger);
        }

        // 죽음 애니메이션이 종료되었는지 여부
        public bool IsDieAnimationFinished()
        {
            return modelAnim.GetCurrentAnimatorStateInfo(0).IsName(dieTrigger);
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

        public void SetAnimatorFromModel(GameObject pokemonModel)
        {
            modelAnim = pokemonModel.GetComponentInChildren<Animator>();
        }

        public void Spawn()
        {
            unitAnim.enabled = true;
            unitAnim.SetTrigger(spawnTrigger);
        }

        public void Despawn()
        {
            unitAnim.SetTrigger(despawnTrigger);
        }
    }
}