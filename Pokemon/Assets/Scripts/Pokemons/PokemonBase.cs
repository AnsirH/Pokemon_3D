using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] string _name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    [SerializeField] GameObject model;

    // Base Stats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;
    public string Name
    {
        get { return _name; }
    }

    public string Description
    {
        get { return description; }
    }

    public PokemonType Type1
    {
        get { return type1; }
    }

    public PokemonType Type2
    {
        get { return type2; }
    }

    public GameObject Model
    {
        get { return model; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase MoveBase
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}
public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon
}

public class TypeChart
{
    static float[][] chart =
    {
        //                   NOR   FIR    WAT   ELE    GRA    ICE   FIG   POI
        /*NOR*/new float[] { 1f,   1f,    1f,   1f,    1f,    1f,  0.5f,  1f },
        /*FIR*/new float[] { 1f,  0.5f,  0.5f,  1f,    2f,    2f,   1f,   1f },
        /*WAT*/new float[] { 1f,   1f,    1f,   0.5f, 0.5f,   1f,   1f,   1f },
        /*ELE*/new float[] { 1f,   1f,    2f,   1f,    1f,    1f,   1f,   1f },
        /*GRA*/new float[] { 1f,  0.5f,  0.5f,  1f,   0.5f,   1f,   1f,  0.5f },
        /*ICE*/new float[] { 1f,  0.5f,  0.5f,  1f,    2f,   0.5f,  1f,   1f },
        /*FIG*/new float[] { 2f,   1f,    1f,   1f,    1f,    2f,  0.5f,  1f },
        /*POI*/new float[] { 1f,   1f,    1f,   1f,    2f,    1f,   1f,  0.5f }
    };

    public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
    {
        if (attackType == PokemonType.None || defenseType == PokemonType.None)
            return 1;

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}
