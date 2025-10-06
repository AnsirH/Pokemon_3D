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
        [Range(0f, 1f)]
        public float Chance;    // 적용 확률
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