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
        bool battleIsOver = false;

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

            canvas.InitialPlayerHud(playerPokemonUnit);
            canvas.InitialEnemyHud(enemyPokemonUnit);
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
                yield return StartCoroutine(PlayerTurn());

                SelectEnemeyAction();
                yield return new WaitUntil(() => currentState != BattleState.Processing);

                canvas.ActiveActionButtons(false);
                if (CheckPlayerFirst())
                {
                    yield return StartCoroutine(TurnProcess(playerPokemonUnit, enemyPokemonUnit, playerActionData)); // 플레이어 턴 실행

                    yield return StartCoroutine(TurnProcess(enemyPokemonUnit, playerPokemonUnit, enemyActionData)); // 적 턴 실행
                }
                else
                {
                    yield return StartCoroutine(TurnProcess(enemyPokemonUnit, playerPokemonUnit, enemyActionData)); // 적 턴 실행

                    yield return StartCoroutine(TurnProcess(playerPokemonUnit, enemyPokemonUnit, playerActionData)); // 플레이어 턴 실행
                }
            }
        }

        IEnumerator SetupBattle()
        {
            currentState = BattleState.Start;
            if (isWildBattle)
            {
                // 야생 포켓몬 등장 이펙트 실행
                canvas.ShowBattleText(BattleTextType.WildStart);
            }
            else
            {
                canvas.ShowBattleText(BattleTextType.NpcStart);
                enemyPokemonUnit.Spawn();
            }

            yield return new WaitUntil(() => !canvas.IsTextAreaShowing);

            canvas.ShowBattleText(BattleTextType.Spawn, playerPokemonUnit.PokemonData.Base.Name);
            playerPokemonUnit.Spawn();

            yield return new WaitUntil(() => !canvas.IsTextAreaShowing);
            canvas.ActiveBattleHud(true);
        }

        IEnumerator PlayerTurn()
        {
            canvas.ActiveBattleHud(true);
            canvas.ActiveActionButtons(true);
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

        IEnumerator TurnProcess(PokemonUnit attacker, PokemonUnit defender, ActionData actionData)
        {
            canvas.ActiveBattleHud(false);
            // 기술 텍스트 출력
            if (attacker.IsPlayerUnit) 
                canvas.ShowBattleText(BattleTextType.PlayerAttack, playerPokemonUnit.PokemonData.Base.Name, playerActionData.moveBase.Name);
            else
                canvas.ShowBattleText(isWildBattle ? BattleTextType.WildEnemyAttack : BattleTextType.NpcEnemyAttack, enemyPokemonUnit.PokemonData.Base.Name, enemyActionData.moveBase.Name);
            yield return new WaitUntil(() => !canvas.IsTextAreaShowing);

            canvas.ActiveBattleHud(true, defender);

            // 기술 액션 시작
            yield return StartCoroutine(attacker.MoveAction(actionData.moveBase, defender.transform));

            // 상대 포켓몬 대미지 입음
            int damage = CalculateDamage(attacker.PokemonData, defender.PokemonData, actionData.moveBase, out AttackData attackData);
            defender.Hit(damage);
            yield return new WaitUntil(() => !canvas.CheckBattleHudUpdating(defender));

            if (attackData.isEffectiveness)
                canvas.ShowBattleText(BattleTextType.Effective);
            else if (attackData.isIneffectiveness)
                canvas.ShowBattleText(BattleTextType.Ineffective);
            yield return new WaitUntil(() => !canvas.IsTextAreaShowing);
            if (attackData.isCritical)
                canvas.ShowBattleText(BattleTextType.Critical);
            yield return new WaitUntil(() => !canvas.IsTextAreaShowing);

            if (defender.IsDead)
            {
                if (defender.IsPlayerUnit)
                    canvas.ShowBattleText(BattleTextType.PlayerFaint, playerPokemonUnit.PokemonData.Base.Name);
                else
                    canvas.ShowBattleText(isWildBattle ? BattleTextType.WildEnemyFaint : BattleTextType.NpcEnemyFaint, enemyPokemonUnit.PokemonData.Base.Name);
                yield return new WaitUntil(() => !canvas.IsTextAreaShowing);

                defender.Die();
                canvas.ActiveBattleHud(false);

                yield return new WaitUntil(() => defender.IsActionAnimationComplete);
                StopAllCoroutines();

                StartCoroutine(ResultProcess(defender));
            }
        }

        private IEnumerator ResultProcess(PokemonUnit faintPokemon)
        {
            if (faintPokemon.IsPlayerUnit)
            {
                // 플레이어 죽었을 시 처리
            }
            else
            {
                canvas.ShowBattleText(BattleTextType.RewardExp, playerPokemonUnit.PokemonData.Base.Name, faintPokemon.PokemonData.RewardExp.ToString());
                yield return new WaitUntil(() => !canvas.IsTextAreaShowing);

                canvas.ActiveBattleHud(true, playerPokemonUnit);
                playerPokemonUnit.PokemonData.CurrentExp += faintPokemon.PokemonData.RewardExp;

                if (playerPokemonUnit.PokemonData.CurrentExp >= playerPokemonUnit.PokemonData.RequireExpToLevelup)
                {
                    int remainExp = 0;
                    while (playerPokemonUnit.PokemonData.CurrentExp >= playerPokemonUnit.PokemonData.RequireExpToLevelup)
                    {
                        remainExp = playerPokemonUnit.PokemonData.CurrentExp - playerPokemonUnit.PokemonData.RequireExpToLevelup;
                        playerPokemonUnit.PokemonData.CurrentExp = playerPokemonUnit.PokemonData.RequireExpToLevelup;
                        canvas.UpdatePlayerExpBar();
                        yield return new WaitUntil(() => !canvas.IsPlayerExpBarUpdating);

                        playerPokemonUnit.PokemonData.Levelup();
                        canvas.InitialPlayerHud(playerPokemonUnit);

                        yield return new WaitForSeconds(1.0f);
                        canvas.ShowBattleText(BattleTextType.Levelup, playerPokemonUnit.PokemonData.Base.Name, playerPokemonUnit.PokemonData.Level.ToString());
                        yield return new WaitUntil(() => !canvas.IsTextAreaShowing);
                        playerPokemonUnit.PokemonData.CurrentExp += remainExp;
                    }
                }

                canvas.UpdatePlayerExpBar();
                yield return new WaitUntil(() => !canvas.IsPlayerExpBarUpdating);
                yield return new WaitForSeconds(1.0f);

                GameFlowManager.Instance.EndBattle();
            }
        }

        private int CalculateDamage(PokemonData attackerData, PokemonData defenderData, MoveBase move, out AttackData attackData)
        {
            attackData = new();
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

            if (critical == 1.5f)
                attackData.isCritical = true;
            if (typeEffectiveness > 1.0f)
            {
                attackData.isEffectiveness = true;
                attackData.isIneffectiveness = false;
            }
            else if (typeEffectiveness < 0.5f)
            {
                attackData.isEffectiveness = false;
                attackData.isIneffectiveness = true;
            }
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
