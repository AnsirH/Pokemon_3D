using Pokemon3D.BattleSystem.Unit;
using Pokemon3D.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.BattleSystem
{
    public partial class BattleSystem
    {
        IEnumerator MoveProcess(PokemonUnit attacker, PokemonUnit defender, ActionData actionData)
        {
            if (actionData.moveBase.IsDamageable)
                canvas.ActiveBattleHud(true, defender);

            yield return StartCoroutine(attacker.MoveAction(actionData.moveBase, defender.transform));

            if (actionData.moveBase.IsDamageable)
            {
                // 상대 포켓몬 대미지 입음
                int damage = CalculateDamage(attacker, defender, actionData.moveBase, out AttackData attackData);
                defender.Hit(damage);
                Instantiate(actionData.moveBase.HitEffectPrefab, defender.HitEffectPoint.position, Quaternion.identity);
                yield return new WaitUntil(() => !canvas.CheckBattleHudUpdating(defender));
                yield return new WaitForSeconds(1.0f);

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
            else
            {
                bool isBuffEffectPlay = false;
                for (int i = 0; i < actionData.moveBase.StatChanges.Count; ++i)
                {
                    if (!isBuffEffectPlay && actionData.moveBase.StatChanges[i].Stages < 0)
                        defender.PlayBuffEffect(false);
                    else if (!isBuffEffectPlay && actionData.moveBase.StatChanges[i].Stages > 0)
                        defender.PlayBuffEffect(true);
                    else if (isBuffEffectPlay && actionData.moveBase.StatChanges[i].Stages < 0 && actionData.moveBase.StatChanges[i - 1].Stages > 0)
                        defender.PlayBuffEffect(false);
                    else if (isBuffEffectPlay && actionData.moveBase.StatChanges[i].Stages > 0 && actionData.moveBase.StatChanges[i-1].Stages < 0)
                        defender.PlayBuffEffect(true);

                    yield return new WaitUntil(() => defender.IsBuffEffectComplete);

                    defender.ChangeStat(actionData.moveBase.StatChanges[i]);
                    canvas.ShowBattleText(BattleTextType.Debuff, defender.PokemonData.Base.Name, actionData.moveBase.StatChanges[i].StatToString);
                    yield return new WaitUntil(() => !canvas.IsTextAreaShowing);
                }
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}