using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class CharacterDatabase : MonoBehaviour

{
    public string DESCRIPTION;
    public int LEVEL_MAX;
    public int LEVEL_MIN;
    public int LEVEL;
    public string NAME;
    public ClassName CLASSNAME;
    public ElementType ELEMENTAL;

    public Sprite characterSprite;
    public GameObject characterObject;


    public int MAXHP;
    public int MAXMP;
    public int HP;
    public int MP;
    public int PHYSICALATTACK;
    public int DEFENSE;
    public int MAGICALATTACK;
    public int MAGICDEFENSE;
    public int ACCURACY  = 0;
    public int FLEE  = 0;
    public int CRIT = 5;
    public int ATTACKRANGE  = 0;
    public int STR  = 0;
    public int VIT  = 0;
    public int AGI  = 0;
    public int DEX  = 0;
    public int INT  = 0;
    public int LCK  = 0;
    //Reward
    public int EXP;
    public int COIN;
    public List<SkillDatabase> skillData = new List<SkillDatabase>();
    public List<Skill> skill = new List<Skill>();

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
        Bowmaster,
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