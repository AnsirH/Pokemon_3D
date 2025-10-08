using Pokemon3D.Enum;
using Pokemon3D.ScriptableObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.BattleSystem
{
    [System.Serializable]
    public class StatChange
    {
        public StatType Stat;
        public int Stages;      // 증가/감소 단계

        public string StatToString
        {
            get
            {
                switch (Stat)
                {
                    case StatType.Attack:
                        return "공격";
                    case StatType.Defense:
                        return "방어";
                    case StatType.SpecialAttack:
                        return "특수공격";
                    case StatType.SpecialDefense:
                        return "특수방어";
                    case StatType.Speed:
                        return "스피드";
                    case StatType.Accuracy:
                        return "명중률";
                    case StatType.Evasion:
                        return "회피율";
                }
                return null;
            }
        }
    }

    public struct ActionData
    {
        public ActionType type;
        public MoveBase moveBase;
    }

    public struct AttackData
    {
        public bool isEffectiveness;
        public bool isIneffectiveness;
        public bool isCritical;
    }
}