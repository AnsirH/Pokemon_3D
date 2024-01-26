using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    public event Action<bool> OnBattleOver;

    BattleState state;
    int currentAction;
    int currentMove;

    public void StartBattle()
    {
        StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.pokemon);
        enemyHud.SetData(enemyUnit.pokemon);

        dialogBox.SetMoveName(playerUnit.pokemon.Moves);

        yield return dialogBox.TypeDialog($"�߻��� {enemyUnit.pokemon.Base.Name} (��)�� ��Ÿ����!");
        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog($"{playerUnit.pokemon.Base.Name}(��)�� ������ �ұ�?"));
        dialogBox.EnableActionSelector(true);
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
            yield return dialogBox.TypeDialog("�޼ҿ� �¾Ҵ�!");

        if (damageDetails.TypeEffectiveness > 1f)
            yield return dialogBox.TypeDialog("ȿ���� �����ߴ�!");
        else if (damageDetails.TypeEffectiveness < 1f)
            yield return dialogBox.TypeDialog("ȿ���� ������ ���ϴ�");
    }

    public void HandleUpdate()
    {
        if(state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }

        else if(state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    // ����Ű�� �ൿ ���� �Լ�
    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentAction < 3)
                ++currentAction;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentAction > 0)
            {
                --currentAction;
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 2)
            {
                currentAction += 2;
            }
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 1)
                currentAction -= 2;
        }
        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(currentAction == 0)
            {
                PlayerMove();
            }

            else if (currentAction == 1)
            {
                // Run
            }
        }
    }

    public void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableDialogText(false);
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableMoveSelector(true);
    }

    void SelectPlayerMove()
    {
        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableDialogText(true);
        StartCoroutine(PerformPlayerMove());
    }

    public void SelectPlayerMoveForButton(int currentButton)
    {
        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableDialogText(true);
        currentMove = currentButton;
        StartCoroutine(PerformPlayerMove());
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        var move = playerUnit.pokemon.Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnit.pokemon.Base.Name}�� {move.Base.Name}!");

        playerUnit.PlayAttackAnimation();
        enemyUnit.PlayHitAnimation();
        yield return new WaitForSeconds(1f);

        var damageDetails = enemyUnit.pokemon.TakeDamage(move, playerUnit.pokemon);
        yield return enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);


        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"��� {enemyUnit.pokemon.Base.Name}(��)�� ��������!");
            enemyUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
            Destroy(playerUnit.model);
            Destroy(enemyUnit.model);
        }

        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.pokemon.GetRandomMove();

        yield return dialogBox.TypeDialog($"��� {enemyUnit.pokemon.Base.Name}�� {move.Base.Name}!");

        enemyUnit.PlayAttackAnimation();
        playerUnit.PlayHitAnimation();
        yield return new WaitForSeconds(1f);

        var damageDetails = playerUnit.pokemon.TakeDamage(move, playerUnit.pokemon);
        yield return playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.pokemon.Base.Name}(��)�� ��������!");
            playerUnit.PlayFaintAnimation();

            yield return new WaitForSeconds(2f);
            OnBattleOver(false);
            Destroy(playerUnit.model);
            Destroy(enemyUnit.model);
        }

        else
        {
            PlayerAction();
        }
    }

    // ����Ű�� ��� ���� �Լ�
    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.pokemon.Moves.Count - 1)
                ++currentMove;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentMove > 0)
            {
                --currentMove;
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentMove < playerUnit.pokemon.Moves.Count - 2)
            {
                currentMove += 2;
            }
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
                currentMove -= 2;
        }
        dialogBox.UpdateMoveSelection(currentMove, playerUnit.pokemon.Moves[currentMove]);

        if(Input.GetKeyDown(KeyCode.Return))
        {
            SelectPlayerMove();
        }
    }
}
