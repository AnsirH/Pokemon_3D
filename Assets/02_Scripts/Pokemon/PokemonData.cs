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
        private List<MoveBase> moves;

        public PokemonBase Base => _base;
        public int Level => level;
        public int MaxHP => _base.MaxHP + Level * 2;
        public int Attack => _base.Attack + Level;
        public int Defense => _base.Defense + Level;
        public int SpecialAttack => _base.SpecialAttack + Level;
        public int SpecialDefense => _base.SpecialDefense + Level;
        public int Speed => _base.Speed + Level;
        public int RequireExpToLevelup => _base.BaseExpYield * _base.BaseExpYield * level;
        public int RewardExp => _base.BaseExpYield * level;
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
        public List<MoveBase> Moves => moves;
        public MoveBase RandomMoveBase { get { return moves[Random.Range(0, moves.Count)]; } }

        public PokemonData(PokemonBase pokemonBase, int level)
        {
            _base = pokemonBase;
            this.level = level;
            currentHp = MaxHP;
            currentExp = 0;
            moves = new List<MoveBase>();
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
                            if (moves[i].IsDamageable)
                            {
                                damageableMoveCount++;
                                removeableIndexes.Remove(i);
                            }
                        }
                        if (damageableMoveCount < 2)
                        {
                            if (move.MoveBase.IsDamageable)
                                moves[Random.Range(0, moves.Count)] = move.MoveBase;
                            else
                                moves[removeableIndexes[Random.Range(0, removeableIndexes.Count)]] = move.MoveBase;
                        }
                    }
                    else
                    {
                        moves.Add(move.MoveBase);
                    }
                }
            }
        }
    }
}