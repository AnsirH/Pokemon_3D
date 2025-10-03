using Pokemon3D.ScriptableObj.PokemonMovementBehaviour;
using System;
using UnityEngine;

namespace Pokemon3D.BattleSystem.Unit
{
	public interface IPokemonUnitEventSource
	{
		public event Action OnSpawn;
		public event Action OnReturn;
		public event Action OnIdle;
		public event Action<PokemonBehaviour, Transform> OnAttack;
		public event Action OnHit;
		public event Action OnDie;
		public event Action OnItemUse;

		public void DoSpawn();
		public void DoReturn();
		public void DoIdle();
		public void DoAttack(PokemonBehaviour pokemonBehaviour, Transform target);
		public void DoHit();
		public void DoDie();
		public void DoItemUse();
		public void Initialize(); // 모든 이벤트 등록( 활성화 시 호출 )
		public void ClearEvent(); // 모든 이벤트 구독 취소( 비활성화 시 호출 )
	}

	public interface IPokemonUnitSubComponent
	{
		public void Initialize(IPokemonUnitEventSource evenetSource);
	}
}
