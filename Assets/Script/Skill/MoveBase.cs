using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Character/Create new move")]

public class MoveBase : ScriptableObject
{
    [SerializeField] Sprite iconSkill;
    [SerializeField] string name;
    [SerializeField] GameObject skillVFX;
    [TextArea]
    [SerializeField] string description;

    [SerializeField] int PATK;
    [SerializeField] int MATK;
    [SerializeField] int ACCURACY;
    [SerializeField] int MP;
    [SerializeField] int VALUE;
    [SerializeField] ElementType ELEMENTAL;
    [SerializeField] SkillTypeList SKILLTYPE;
    [SerializeField] List<BuffType> BUFFTYPE;
    [SerializeField] List<DebuffType> DEBUFFTYPE;
     public string Description {
        get { return description; }
    }

    public string Name {
        get { return name; }
    }
    public GameObject SkillVFX {
        get { return skillVFX; }
    }
    public int PhysicalAttack {
        get { return PATK; }
    }
    public int MagicalAttack {
        get { return MATK; }
    }
    public int Mana {
        get { return MP; }
    }

    public int Accuracy {
        get { return ACCURACY; }
    }
    public int Value {
        get { return VALUE; }
    }

    public ElementType Elemental {
        get { return ELEMENTAL; }
    }
    public SkillTypeList SkillType {
        get { return SKILLTYPE; }
    }
    public List<BuffType> Bufftype {
        get{return BUFFTYPE;}
    }
    public List<DebuffType> Debufftype {
        get{return DEBUFFTYPE;}
    }

    public Sprite IconSKill{
        get {return iconSkill;}
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
    public enum SkillTypeList
    {
        Attack,
        Buff,
        Debuff
    }
    public enum BuffType
    {
        None,
        Heal,
        AttackUp,
        MagicUp,
        DefenseUP,
        SpellGuard,
    }
    public enum DebuffType
    {
        None,
        Burn,
        Stun,
        Disarm,
        Purge,
        DefenseDown,
        SpellGuardDown,
        Blind
    }
}
