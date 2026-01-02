using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterData : ScriptableObject
{

    [TextArea]
    [SerializeField] string DESCRIPTION;
    [SerializeField] int LEVEL;
    [SerializeField] string NAME;
    [SerializeField] ClassName CLASSNAME;
    [SerializeField] ElementType ELEMENTAL;
    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite enemySprite;


    [SerializeField] int MAXHP;
    [SerializeField] int MAXMP;
    [SerializeField] int PHYSICALATTACK;
    [SerializeField] int DEFENSE;
    [SerializeField] int MAGICALATTACK;
    [SerializeField] int MAGICDEFENSE;

    [SerializeField] List<LearnableMove> learnableMoves;

    //Reward
    [SerializeField] int EXP;
    [SerializeField] int COIN;


    public Sprite PlayerSprite {
        get{return playerSprite;}
    }

    public Sprite EnemySprite{
        get{return enemySprite;}
    }

    public string Name{
        get { return NAME; }
    }

    public string Description{
        get { return DESCRIPTION; }

    }
    public ClassName Classname{
        get { return CLASSNAME; }
    }

    public ElementType Elemental{
        get { return ELEMENTAL; }

    }
    

    public int MaxHP{
        get { return MAXHP; }

    }
    public int MaxMP{
        get { return MAXMP; }

    }
    public int PhysicalAttack{
        get { return PHYSICALATTACK; }

    }
    public int Defanse{
        get { return DEFENSE; }

    }
    public int MagicalAttack{
        get { return MAGICALATTACK; }

    }
    public int MagicDefense{
        get { return MAGICDEFENSE; }

    }
    public int Level{
        get { return LEVEL; }

    }
    public int Exp{
        get { return EXP; }

    }
    public int Coin{
        get { return COIN; }

    }

    public List<LearnableMove> LearnableMoves {
    get{return learnableMoves;}
    }

        [System.Serializable]
        public class LearnableMove{
        [SerializeField] MoveBase moveBase;
        [SerializeField] int Slevel;

        public MoveBase Base {
            get {return moveBase;}
        }

        public int Level {
            get {return Slevel;}
        }
    }

    public enum ElementType
    {
        Neutral,
        Water,
        Fire,
        Earth,
        Wind,
        Poison,
        Holy,
        Shadow,
        Ghost,
        Undead
    }

    public enum ClassName
    {
        None,
        Warrior,
        Sorceress,
        Archer,
        Cleric,
        Academic,
        Theif,

    }
    public enum HighClassName
    {
        Hermit,
        Swordman,
        Mercenary,
        Hunter,
        Sniper,
        HighWizard,
        Sage,
        Paladin,
        Priest,
        Engineer,
        Alchemist,
        Rouge,
        Assasin

    }

}