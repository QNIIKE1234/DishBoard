using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{
    public CharacterClassData classDatabase;
    public ArmorController AllArmor;
    public WeaponController Allweapon;
    public int IndexClass=0;
    public string PName  = "Test";
    public string CName="";
    public string Class="";
    public string Gender="";
    public int PLevel  = 5;
    public int MAXHP  = 0;
    public int HP  = 0;
    public int MAXMP  = 0;
    public int MP  = 0;
    public int PHYSICALATTACK  = 0 ;
    public int DEFENSE  = 0;
    public int MAGICALATTACK  = 0;
    public int MAGICDEFENSE  = 0;
    public int ACCURACY   = 0;
    public int FLEE  = 0;
    public int CRIT  = 5;
    public int ATTACKRANGE  = 0;
    public int STR  = 0;
    public int VIT   = 0;
    public int AGI  = 0;
    public int DEX  = 0;
    public int INT  = 0 ;
    public int LCK  = 0;
    public int EXP  = 0;

    public string WEAPON  ;
    public string OFFHAND  ;
    public string ARMOR   ;
    public string CAPE  ;
    public string HELM  ;
    public List<LevelStatusPlus> levelStatusPlus = new List<LevelStatusPlus>();
    public ElementType ELEMENTAL;

    [System.Serializable]
    public class LevelStatusPlus
	{
        public string Class;
		public int Health;
        public int Mana;
        public int Attack;
        public int MagicAttack;
        public int Defense;
        public int MagicDefense;
        public int Accuracy;
        public int Flee;
        public int STR;
        public int VIT;
        public int AGI;
        public int DEX;
        public int INT;
        public int LCK;

	} 

    public void onRestoreHPandMP(){
        HP = MAXHP;
        MP = MAXMP;
    }
    public void onUpdateData(){
        onUpdateEquipment();
        MAXHP =MAXHP+(VIT*20);
        MAXMP =MAXMP+(INT*20);


    }
    public void onUpdateEquipment(){


        onUpdateAttackAndDefense();


    }
    public void onUpdateAttackAndDefense(){


        onUpdateExtraStat();

    }
    public void onUpdateExtraStat(){
        CRIT += (LCK/10);
        ACCURACY += (DEX/5);
        FLEE += (AGI/5);
        ATTACKRANGE += onRange();

        
    }
    public void onLevelUp(int _index){
        LevelStatusPlus statusPlus = levelStatusPlus.Find(S => S.Class == Class);

        MAXHP += statusPlus.Health*PLevel;
        MAXMP += statusPlus.Mana*PLevel;
        PHYSICALATTACK += statusPlus.Attack*PLevel;
        DEFENSE += statusPlus.Defense*PLevel;
        MAGICALATTACK+= statusPlus.MagicAttack*PLevel;
        MAGICDEFENSE += statusPlus.MagicDefense*PLevel;
        ACCURACY+= statusPlus.Accuracy*PLevel;
        FLEE += statusPlus.Flee*PLevel;

        STR += statusPlus.STR *PLevel;
        VIT  += statusPlus.VIT*PLevel;
        AGI += statusPlus.AGI *PLevel;
        DEX += statusPlus.DEX*PLevel;
        INT += statusPlus.INT*PLevel;
        LCK += statusPlus.LCK*PLevel;

        EXP = 0;
        PLevel ++;
        onRestoreHPandMP();
    }

    public int onRange(){
        int value = DEX/30;
        if(value>1){
            return value;
        }else{
            return 0;
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
