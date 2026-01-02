using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create new Character")]
public class ChatacterBase : ScriptableObject
{

    [TextArea]
    [SerializeField] string description;
    [SerializeField] int LEVEL;
    [SerializeField] string NAME;
    [SerializeField] string CLASSNAME;
    [SerializeField] ElementType ELEMENTAL;
    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite enemySprite;


    [SerializeField] int MaxHP;
    [SerializeField] int MaxMP;
    [SerializeField] int ATK;
    [SerializeField] int DEF;
    [SerializeField] int SPELL;
    [SerializeField] int SPELLGUARD;

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
        get { return description; }

    }
    public string ClassName{
        get { return CLASSNAME; }
    }

    public ElementType Elemental{
        get { return ELEMENTAL; }

    }

    public int MAXHP{
        get { return MaxHP; }

    }
    public int MAXMP{
        get { return MaxMP; }

    }
    public int Attack{
        get { return ATK; }

    }
    public int Defense{
        get { return DEF; }

    }
    public int Spell{
        get { return SPELL; }

    }
    public int SpellGuard{
        get { return SPELLGUARD; }

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
}