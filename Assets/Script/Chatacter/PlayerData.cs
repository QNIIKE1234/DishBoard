using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
		
		DontDestroyOnLoad(this);
    }
    public CharacterClassData classDatabase;
    public ArmorController AllArmor;
    public WeaponController Allweapon;
    public int IndexClass;
    public string PName = "Test";
    public string CName="";
    public string Gender="";
    public int PLevel  = 5;
    public int MAXHP = 0;
    public int HP  = 0;
    public int MAXMP  = 0;
    public int MP  = 0;
    public int PHYSICALATTACK  = 0 ;
    public int DEFENSE  = 0;
    public int MAGICALATTACK = 0;
    public int MAGICDEFENSE  = 0;
    public int ACCURACY  = 0;
    public int FLEE  = 0;
    public int CRIT = 5;
    public int ATTACKRANGE  = 0;
    public int STR  = 0;
    public int VIT  = 0;
    public int AGI  = 0;
    public int DEX  = 0;
    public int INT  = 0 ;
    public int LCK  = 0;
    public int COIN  = 1500;
    public int EXP  = 0;


    //Equipment
    public string WEAPON ;
    public string OFFHAND ;
    public string ARMOR ;  
    public string CAPE ;
    public string HELM ;
    public bool isPlayer;
    public ClassName CLASSNAME;
    public ElementType ELEMENTAL;
    public Sprite characterSprite;
    public GameObject characterObject;

    public string Class;
    public List<Skill> skill = new List<Skill>();
    public List<Item> InventoryItem = new List<Item>();
    public List<LevelStatusPlus> levelStatusPlus = new List<LevelStatusPlus>();
    public List<int> ExpLength = new List<int>();

    public int currentIndex;
    public int maxIndex;

    public string userID;

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

    // public void SendDataToFirestore(string userId)
    // {
    //     // Get a reference to Firestore
    //     FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

    //     // Create a new document in Firestore for the player data
    //     DocumentReference docRef = db.Collection("characterData").Document(userId);
    //     // Create a dictionary to hold the player data
    //     Dictionary<string, object> playerData = new Dictionary<string, object>
    //     {
    //         { "PlayerName", PName },
    //         { "CharacterName", CName },
    //         { "Gender", Gender },
    //         { "Level", PLevel },
    //         { "MaxHP", MAXHP },
    //         { "HP", HP },
    //         { "MaxMP", MAXMP },
    //         { "MP", MP },
    //         { "PhysicalAttack", PHYSICALATTACK },
    //         { "Defense", DEFENSE },
    //         { "MagicalAttack", MAGICALATTACK },
    //         { "MagicDefense", MAGICDEFENSE },
    //         { "Accuracy", ACCURACY },
    //         { "Flee", FLEE },
    //         { "Crit", CRIT },
    //         { "Strength", STR },
    //         { "Vitality", VIT },
    //         { "Agility", AGI },
    //         { "Dexterity", DEX },
    //         { "Intelligence", INT },
    //         { "Luck", LCK },
    //         { "Coin", COIN },
    //         { "Experience", EXP },
    //         { "Class", Class },
    //         {"IndexClass",IndexClass},
    //         { "Weapon", WEAPON},
    //         { "OffHand", OFFHAND },
    //         { "Armor", ARMOR },
    //         { "Cape", CAPE },
    //         { "Helm", HELM },
    //         { "Character", "Yes" },
    //         { "CurrentIndex", currentIndex },
    //         { "MaxIndex", maxIndex },

    //         // Assuming skill is a List<Skill> // Assuming InventoryItem is a List<Item>
    //         // Add more fields as necessary
    //     };
    //     // Set the document data
    //     docRef.SetAsync(playerData).ContinueWithOnMainThread(task => 
    //     {
    //         if (task.IsCompleted)
    //         {
    //             Debug.Log("Player data successfully written to Firestore.");
    //         }
    //         else
    //         {
    //             Debug.LogError("Error writing player data to Firestore: " + task.Exception);
    //         }
    //     });
    // }


    // public void GetDataToFirestore(string userId)
    // {
    //     FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

    //     DocumentReference docRef = db.Collection("characterData").Document(userId);

    //     // ดึงข้อมูลจากเอกสาร
    //     docRef.GetSnapshotAsync().ContinueWithOnMainThread(task => 
    //     {
    //         if (task.IsCompleted && task.Result.Exists)
    //         {
    //             DocumentSnapshot snapshot = task.Result;

    //             // อัปเดตตัวแปรในคลาสด้วยข้อมูลจาก Firestore
    //             PName = snapshot.GetValue<string>("PlayerName");
    //             CName = snapshot.GetValue<string>("CharacterName");
    //             Gender = snapshot.GetValue<string>("Gender");
    //             PLevel = snapshot.GetValue<int>("Level");
    //             MAXHP = snapshot.GetValue<int>("MaxHP");
    //             HP = snapshot.GetValue<int>("HP");
    //             MAXMP = snapshot.GetValue<int>("MaxMP");
    //             MP = snapshot.GetValue<int>("MP");
    //             PHYSICALATTACK = snapshot.GetValue<int>("PhysicalAttack");
    //             DEFENSE = snapshot.GetValue<int>("Defense");
    //             MAGICALATTACK = snapshot.GetValue<int>("MagicalAttack");
    //             MAGICDEFENSE = snapshot.GetValue<int>("MagicDefense");
    //             ACCURACY = snapshot.GetValue<int>("Accuracy");
    //             FLEE = snapshot.GetValue<int>("Flee");
    //             CRIT = snapshot.GetValue<int>("Crit");
    //             STR = snapshot.GetValue<int>("Strength");
    //             VIT = snapshot.GetValue<int>("Vitality");
    //             AGI = snapshot.GetValue<int>("Agility");
    //             DEX = snapshot.GetValue<int>("Dexterity");
    //             INT = snapshot.GetValue<int>("Intelligence");
    //             LCK = snapshot.GetValue<int>("Luck");
    //             COIN = snapshot.GetValue<int>("Coin");
    //             EXP = snapshot.GetValue<int>("Experience");
    //             Class = snapshot.GetValue<string>("Class");
    //             IndexClass = snapshot.GetValue<int>("IndexClass"); 
    //             WEAPON = snapshot.GetValue<string>("Weapon");
    //             OFFHAND = snapshot.GetValue<string>("OffHand"); 
    //             ARMOR = snapshot.GetValue<string>("Armor"); 
    //             CAPE = snapshot.GetValue<string>("Cape"); 
    //             HELM = snapshot.GetValue<string>("Helm");
    //             currentIndex = snapshot.GetValue<int>("CurrentIndex");
    //             maxIndex = snapshot.GetValue<int>("MaxIndex");
    //             // แสดงข้อมูลใน Debug Log
    //             skill =  classDatabase.classData[IndexClass].moves;
    //         }
    //         else
    //         {
    //             Debug.LogError("Error getting player data or document does not exist: " + task.Exception);
    //         }
    //     });
    // }
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
