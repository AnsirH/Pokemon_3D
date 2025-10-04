using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokemon3D.Enum;
using Pokemon3D.Singleton;
using Pokemon3D.Pokemon;
using Pokemon3D.Core;
using Pokemon3D.BattleSystem.Unit;
using Pokemon3D.BattleSystem.UI;
using Pokemon3D.ScriptableObj;
using Pokemon3D.Utility;

namespace Pokemon3D.BattleSystem
{
    public class BattleSystem : Singleton<BattleSystem>
    {
        [Header("references")]
        [SerializeField] BattleCanvas canvas;
        [SerializeField] PokemonUnit playerPokemonUnit;
        [SerializeField] PokemonUnit enemyPokemonUnit;

        // variables
        BattleState currentState;
        ActionData playerActionData;
        ActionData enemyActionData;
        Coroutine currentCoroutine;
        bool isWildBattle = true;

        // properties
        public PokemonData PlayerPokemon => playerPokemonUnit.PokemonData;
        public PokemonData EnemyPokemon => enemyPokemonUnit.PokemonData;
        public bool IsWildBattle => isWildBattle;

        private void Start()
        {
            isWildBattle = GameFlowManager.Instance.BattleType == BattleOpponentType.Wild;
            Initialize();
            StartBattle();
        }

        private void Initialize()
        {
            playerActionData = new();
            enemyActionData = new();

            playerPokemonUnit.Initialize(PokemonManager.Instance.HeadPokemon);
            enemyPokemonUnit.Initialize(GameFlowManager.Instance.EnemyPokemon);

            canvas.InitialPlayerHud(playerPokemonUnit.PokemonData);
            canvas.InitialEnemyHud(enemyPokemonUnit.PokemonData);
        }

        private void StartBattle()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(StartBattleCoroutine());
        }
        private IEnumerator StartBattleCoroutine()
        {
            yield return StartCoroutine(SetupBattle());
            while (true)
            {
                canvas.ActiveBattleHud(true);
                canvas.ActiveActionButtons(true);
                yield return StartCoroutine(PlayerTurn());

                SelectEnemeyAction();
                yield return new WaitUntil(() => currentState != BattleState.Processing);

                canvas.ActiveActionButtons(false);

                if (CheckPlayerFirst())
                {
                    yield return StartCoroutine(AttackProcess(playerPokemonUnit, enemyPokemonUnit, playerActionData));
                    canvas.UpdateEnemyHpBar(enemyPokemonUnit.PokemonData);

                    yield return StartCoroutine(AttackProcess(enemyPokemonUnit, playerPokemonUnit, enemyActionData));
                    canvas.UpdatePlayerHpBar(playerPokemonUnit.PokemonData);
                }
                else
                {
                    yield return StartCoroutine(AttackProcess(enemyPokemonUnit, playerPokemonUnit, enemyActionData));
                    canvas.UpdatePlayerHpBar(playerPokemonUnit.PokemonData);

                    yield return StartCoroutine(AttackProcess(playerPokemonUnit, enemyPokemonUnit, playerActionData));
                    canvas.UpdateEnemyHpBar(enemyPokemonUnit.PokemonData);
                }
            }
        }

        IEnumerator SetupBattle()
        {
            currentState = BattleState.Start;
            if (isWildBattle)
            {
                // 야생 포켓몬 등장 이펙트 실행
                canvas.ShowBattleStartText(true);
            }
            else
            {
                canvas.ShowBattleStartText(false);
                enemyPokemonUnit.Spawn();
            }

            yield return new WaitForSeconds(3.5f);

            canvas.ShowSpawnPokemonText(playerPokemonUnit.PokemonData.Base.Name);
            playerPokemonUnit.Spawn();

            yield return new WaitForSeconds(3.5f);
            canvas.ActiveBattleHud(true);
        }

        IEnumerator PlayerTurn()
        {
            currentState = BattleState.PlayerTurn;

            // 플레이어 턴 대기
            yield return new WaitUntil(() => currentState != BattleState.EnemyTurn);
        }

        private void ConfirmPlayerTurn()
        {
            if (playerActionData.type == ActionType.None) return;
            currentState = BattleState.EnemyTurn;
        }

        public void SelectPlayerMove(MoveBase moveBase)
        {
            playerActionData.type = ActionType.Attack;
            playerActionData.moveBase = moveBase;
            ConfirmPlayerTurn();
        }

        IEnumerator AttackProcess(PokemonUnit attacker, PokemonUnit defender, ActionData actionData)
        {
            canvas.ShowPlayerPokemonAttackText(attacker.PokemonData.Base.Name, playerActionData.moveBase.name);
            yield return new WaitUntil(() => !canvas.IsTextAreaShowing);
            yield return StartCoroutine(attacker.MoveAction(actionData.moveBase, defender.transform));

            // 상대 포켓몬 대미지 입음
            int damage = CalculateDamage(attacker.PokemonData, defender.PokemonData, actionData.moveBase);
            Debug.Log($"{attacker.PokemonData.Base.name}가 준 데미지 {damage}");
            defender.Hit(damage);

            yield return new WaitForSeconds(2.0f);
        }

        private int CalculateDamage(PokemonData attackerData, PokemonData defenderData, MoveBase move)
        {
            // 1. 기초 값 계산
            float level = attackerData.Level;
            float power = move.Power;
            float attack = move.Category == MoveCategory.Special ? attackerData.SpecialAttack : attackerData.Attack;
            float defense = move.Category == MoveCategory.Special ? defenderData.SpecialDefense : defenderData.Defense;

            // 2. 기본 공식
            float baseDamage = (((2f * level / 5f + 2f) * power * (attack / defense)) / 50f) + 2f;

            // 3. 배율 계산
            float modifier = 1.0f;
            // STAB: Same Type Attack Bonus
            float stab = (attackerData.Base.Type_1 == move.Type || attackerData.Base.Type_2 == move.Type) ? 1.5f : 1f;
            // Critical( 10% 확률 )
            float critical = Random.value < 0.1f ? 1.5f : 1f;
            // TypeEffectiveness
            float typeEffectiveness = 1.0f;
            typeEffectiveness *= TypeChart.GetEffectiveness(move.Type, defenderData.Base.Type_1);
            if (defenderData.Base.Type_2 != PokemonType.None)
                typeEffectiveness *= TypeChart.GetEffectiveness(move.Type, defenderData.Base.Type_2);
            // Random( 0.85 ~ 1.0 )
            float random = Random.Range(0.85f, 1.0f);
            modifier *= stab * critical * typeEffectiveness * random;

            return Mathf.FloorToInt(baseDamage * modifier);
        }

        void SelectEnemeyAction()
        {
            enemyActionData.type = ActionType.Attack;
            enemyActionData.moveBase = enemyPokemonUnit.PokemonData.RandomMoveBase;

            currentState = BattleState.Processing;
        }

        private bool CheckPlayerFirst()
        {
            if (playerActionData.moveBase.Priority == enemyActionData.moveBase.Priority)
            {
                if (playerPokemonUnit.PokemonData.Speed == enemyPokemonUnit.PokemonData.Speed)
                {
                    if (Random.Range(0, 2) == 0) // 랜덤 선택. 0이면 플레이어가 선
                        return true;
                    else
                        return false;
                }
                else if (playerPokemonUnit.PokemonData.Speed > enemyPokemonUnit.PokemonData.Speed)
                    return true;
                else
                    return false;
            }
            else if (playerActionData.moveBase.Priority > enemyActionData.moveBase.Priority)
                return true;
            else
                return false;
        }
    }
}
