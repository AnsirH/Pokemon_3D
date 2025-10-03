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

namespace Pokemon3D.BattleSystem
{
    public class BattleSystem : Singleton<BattleSystem>
    {
        [Header("references")]
        [SerializeField] BattleCanvas canvas;
        [SerializeField] PokemonUnit playerPokemonUnit;
        [SerializeField] PokemonUnit enemyPokemonUnit;

        // variables
        PokemonData playerPokemon;
        PokemonData enemyPokemon;
        BattleState currentState;
        ActionData playerActionData;
        ActionData enemyActionData;
        Coroutine currentCoroutine;
        bool isWildBattle = true;

        // properties
        public PokemonData PlayerPokemon => playerPokemon;
        public PokemonData EnemyPokemon => enemyPokemon;
        public bool IsWildBattle => isWildBattle;

        private void Start()
        {
            isWildBattle = GameFlowManager.Instance.BattleType == BattleOpponentType.Wild;
            Initialize();
            StartBattle();
        }

        private void Initialize()
        {
            playerPokemon = PokemonManager.Instance.HeadPokemon;
            enemyPokemon = GameFlowManager.Instance.EnemyPokemon;

            playerActionData = new();
            enemyActionData = new();

            canvas.InitialPlayerHud(playerPokemon);
            canvas.InitialEnemyHud(enemyPokemon);

            playerPokemonUnit.Initialize();
            enemyPokemonUnit.Initialize();
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
            currentState = BattleState.Start;
            if (isWildBattle)
            {
                // 야생 포켓몬 등장 이펙트 실행
                canvas.ShowText(BattleTextCase.WildBattleStart, 3.0f);
            }
            else
            {
                canvas.ShowText(BattleTextCase.NpcBattleStart, 3.0f);
                enemyPokemonUnit.DoSpawn();
            }

            yield return new WaitForSeconds(3.5f);

            canvas.ShowText(BattleTextCase.SpawnPokemon, 3.0f);
            playerPokemonUnit.DoSpawn();

            yield return new WaitForSeconds(3.5f);
            canvas.ActiveBattleHud(true);

            currentState = BattleState.PlayerTurn;

            // 플레이어 턴 대기
            while (currentState != BattleState.EnemyTurn)
                yield return null;

            SelectEnemeyAction();

            while (currentState != BattleState.Processing)
                yield return null;

            CalculateTurn(out PokemonUnit firstActionUnit, out PokemonUnit secondActionUnit);

            canvas.ShowText(BattleTextCase.PlayerPokemonAttack, 2.0f);
            firstActionUnit.DoAttack(playerActionData.moveBase.PokemonBehaviours, secondActionUnit.transform);

            yield return new WaitForSeconds(2.0f);
            secondActionUnit.DoAttack(enemyActionData.moveBase.PokemonBehaviours, firstActionUnit.transform);

        }

        private void ConfirmPlayerTurn()
        {
            if (playerActionData.type == ActionType.None) return;
            currentState = BattleState.EnemyTurn;
        }

        public void SelectMove(MoveBase moveBase)
        {
            playerActionData.type = ActionType.Attack;
            playerActionData.moveBase = moveBase;
            ConfirmPlayerTurn();
        }

        public void SelectEnemeyAction()
        {
            enemyActionData.type = ActionType.Attack;
            enemyActionData.moveBase = enemyPokemon.RandomMoveBase;

            currentState = BattleState.Processing;
        }

        private void CalculateTurn(out PokemonUnit firstActionUnit, out PokemonUnit secondActionUnit)
        {
            if (playerActionData.moveBase.Priority == enemyActionData.moveBase.Priority)
            {
                if (playerPokemon.Speed == enemyPokemon.Speed)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        firstActionUnit = playerPokemonUnit;
                        secondActionUnit = enemyPokemonUnit;
                    }
                    else
                    {
                        firstActionUnit = enemyPokemonUnit;
                        secondActionUnit = playerPokemonUnit;
                    }
                }
                else if (playerPokemon.Speed > enemyPokemon.Speed)
                {
                    firstActionUnit = playerPokemonUnit;
                    secondActionUnit = enemyPokemonUnit;
                }
                else
                {
                    firstActionUnit = enemyPokemonUnit;
                    secondActionUnit = playerPokemonUnit;
                }
            }
            else if (playerActionData.moveBase.Priority > enemyActionData.moveBase.Priority)
            {
                firstActionUnit = playerPokemonUnit;
                secondActionUnit = enemyPokemonUnit;
            }
            else
            {
                firstActionUnit = enemyPokemonUnit;
                secondActionUnit = playerPokemonUnit;
            }
        }
    }
}
