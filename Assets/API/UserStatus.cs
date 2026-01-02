using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using System;

public class UserStatus : MonoBehaviour {


    public static UserStatus Instance { get; set; }
    public string UserId ;
    public string Name ;
    public string Email ;
    public int currentIndex = 0;
    public int maxIndex= 1;
    public int COIN  = 1500;
    public UseCase useCase= new UseCase();
    public Character character ; // Keep track of selected character

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ทำให้ไม่ถูกทำลายเมื่อเปลี่ยนฉาก
        }
        else
        {
            Destroy(gameObject); // ลบอ็อบเจ็กต์ซ้ำ
        }
    }
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
    public List<PLevelStatusPlus> levelStatusPlus = new List<PLevelStatusPlus>();
    public List<Item> InventoryItem = new List<Item>();
    public ElementType ELEMENTAL;
    public List<int> ExpLength = new List<int>();

    [System.Serializable]
    public class PLevelStatusPlus
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

        GetEquipment();
        onUpdateExtraStat();

    }
    public void onUpdateExtraStat(){
        // PHYSICALATTACK +=(STR*5);
        // DEFENSE += (STR*2) + (VIT*3);
        // MAGICALATTACK += (INT*5)+(VIT*3);
        // MAGICDEFENSE += (INT*2);
        CRIT += (LCK/4);
        ACCURACY += (DEX/4);
        FLEE += (AGI/4);
        ATTACKRANGE += onRange();

        
    }
    public void onPLevelUp(int _index){
        PLevelStatusPlus statusPlus = levelStatusPlus.Find(S => S.Class == Class);

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

    public void GetExtraStat(){
        if(Class == "WarriorClass"){
            PHYSICALATTACK +=  (STR*5) + (DEX*2) + (PLevel*2);
            MAGICALATTACK +=  (INT*2)+ (DEX*1)+ (PLevel*2);
            MAGICDEFENSE +=  (INT*1)+(VIT*2)+ (PLevel*2);
            DEFENSE +=  (VIT*3)+ (PLevel*2);
            
        }else if (Class == "SorceressClass"){
            PHYSICALATTACK +=  (STR*2) + (DEX*2)+ (PLevel*2);
            MAGICALATTACK +=  (INT*5)+ (DEX*2)+ (PLevel*2);
            MAGICDEFENSE +=  (INT*3)+(VIT*3)+(PLevel*2);
            DEFENSE +=  (VIT*3)+(STR*1)+ (PLevel*2);       
        }
        else if (Class == "ArcherClass"){
            PHYSICALATTACK +=  (STR*1) + (DEX*3)+ (PLevel*1);
            MAGICALATTACK +=  (INT*1)+ (DEX*1)+ (PLevel*1);
            MAGICDEFENSE +=  (INT*1)+(VIT*1)+ (PLevel*1);
            DEFENSE +=  (VIT*2)+ (PLevel*1);        
        }
        else if (Class == "ClericClass"){
            PHYSICALATTACK +=  (STR*3) + (DEX*1)+ (PLevel*1);
            MAGICALATTACK +=  (INT*2)+ (DEX*1)+ (PLevel*2);
            MAGICDEFENSE +=  (INT*1)+(VIT*1)+ (PLevel*1);
            DEFENSE +=  (VIT*2)+(INT*1)+ (PLevel*1);               
        }
        else if (Class == "AcademicClass"){
            PHYSICALATTACK +=  (STR*2) + (DEX*1)+ (PLevel*1);
            MAGICALATTACK +=  (INT*2)+ (DEX*1)+ (PLevel*2);
            MAGICDEFENSE +=  (INT*1)+(VIT*1)+ (PLevel*1);
            DEFENSE +=  (VIT*2)+ (PLevel*1);               
        }
        else if (Class == "TheifClass"){
            PHYSICALATTACK +=  (STR*3) + (DEX*1) +(LCK*1)+ (PLevel*1);
            MAGICALATTACK +=   (INT*1)+ (DEX*1)+ (PLevel*1);
            MAGICDEFENSE +=  (INT*1)+(VIT*1)+ (PLevel*1);
            DEFENSE +=  (VIT*2)+ (PLevel*1);               
        }
    }

    public void GetEquipment(){
        WeaponController.Weapon PlayerWapon =  Allweapon.weapon.Find(w => w.setID ==  UserStatus.Instance.WEAPON);
        ArmorController.Armor PlayerArmor =  AllArmor.armor.Find(a => a.setID ==  UserStatus.Instance.ARMOR);

        if(PlayerWapon!=null){
            PHYSICALATTACK += PlayerWapon.Attack;
            MAGICALATTACK += PlayerWapon.Mattack;
            ACCURACY += PlayerWapon.Accuracy;
            CRIT += PlayerWapon.Crit;

            STR += ( PlayerWapon.STR );
            VIT += ( PlayerWapon.VIT );
            AGI += ( PlayerWapon.AGI );
            DEX += ( PlayerWapon.DEX );
            INT += ( PlayerWapon.INT );
            LCK += ( PlayerWapon.LCK );
        }

        if(PlayerArmor!=null){
            DEFENSE += PlayerArmor.DEFENSE;
            MAGICDEFENSE += PlayerArmor.MAGICDEFENSE;
            MAXHP += PlayerArmor.HP;
            MAXMP += PlayerArmor.MP;
            FLEE += PlayerArmor.FLEE;

            STR += (PlayerArmor.STR);
            VIT += (PlayerArmor.VIT);
            AGI += (PlayerArmor.AGI);
            DEX += (PlayerArmor.DEX);
            INT += (PlayerArmor.INT);
            LCK += (PlayerArmor.LCK);
        }

        GetExtraStat();
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

        

