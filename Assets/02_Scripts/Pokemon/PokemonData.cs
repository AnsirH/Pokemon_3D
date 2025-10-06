using Pokemon3D.ScriptableObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pokemon3D.Pokemon
{
    public class PokemonData
    {
        private PokemonBase _base;
        private int level;
        private int currentHp;
        private int currentExp;
        private List<MoveData> moves;

        public PokemonBase Base => _base;
        public int Level => level;
        public int MaxHP => Mathf.FloorToInt(((_base.MaxHP * 2f + 10f) * level / 100) + level + 10);
        public int Attack => Mathf.FloorToInt((_base.Attack * 2f + 10f) * level / 100 + 5);
        public int Defense => Mathf.FloorToInt((_base.Defense * 2f + 10f) * level / 100 + 5);
        public int SpecialAttack => Mathf.FloorToInt((_base.SpecialAttack * 2f + 10f) * level / 100 + 5);
        public int SpecialDefense => Mathf.FloorToInt((_base.SpecialDefense * 2f + 10f) * level / 100 + 5);
        public int Speed => Mathf.FloorToInt((_base.Speed * 2f + 10f) * level / 100 + 5);
        public int RequireExpToLevelup => Mathf.FloorToInt(Mathf.Pow(level, 2f));
        public int RewardExp => Mathf.FloorToInt(_base.BaseExpYield * level / 5f);
        public int CurrentHp
        {
            get { return currentHp; }
            set
            {
                currentHp = value;
                if (currentHp < 0) currentHp = 0;
            }
        }
        public int CurrentExp
        {
            get { return currentExp; }
            set
            {
                currentExp = value;
                if (currentExp < 0) currentExp = 0;
            }
        }
        public List<MoveData> Moves => moves;
        public MoveBase RandomMoveBase { get { return moves[Random.Range(0, moves.Count)].moveBase; } }

        public PokemonData(PokemonBase pokemonBase, int level)
        {
            _base = pokemonBase;
            this.level = level;
            currentHp = MaxHP;
            currentExp = 0;
            moves = new List<MoveData>();
            foreach (var move in pokemonBase.LearnableMoves)
            {
                if (move.RequireLevel <= level)
                {
                    if (moves.Count >= 4)
                    {
                        int damageableMoveCount = 0;
                        List<int> removeableIndexes = new() { 0, 1, 2, 3 };
                        for (int i = 0; i < moves.Count; ++i)
                        {
                            if (moves[i].moveBase.IsDamageable)
                            {
                                damageableMoveCount++;
                                removeableIndexes.Remove(i);
                            }
                        }
                        if (damageableMoveCount < 2)
                        {
                            if (move.MoveBase.IsDamageable)
                                moves[Random.Range(0, moves.Count)] = new MoveData(move.MoveBase);
                            else
                                moves[removeableIndexes[Random.Range(0, removeableIndexes.Count)]] = new MoveData(move.MoveBase);
                        }
                        else
                            moves[Random.Range(0, moves.Count)] = new MoveData(move.MoveBase);
                    }
                    else
                    {
                        moves.Add(new MoveData(move.MoveBase));
                    }
                }
            }
        }

        public void Levelup()
        {
            if (level >= 100) return;
            level++;
            currentExp = 0;
        }
    }

    public class MoveData
    {
        public MoveBase moveBase;
        public int pp;

        public MoveData(MoveBase moveBase)
        {
            this.moveBase = moveBase;
            pp = moveBase.PP;
        }
    }
}