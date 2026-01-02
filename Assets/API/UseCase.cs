using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using System.Linq;
using System;
using Firebase.Firestore;
using Firebase.Extensions;

public class UseCase
{
    private readonly FirebaseConnection firebaseConnection = new FirebaseConnection();
    public async Task<bool> Login(string _emaill, string _password){
        bool _signinSuccess = await firebaseConnection.LoginAsync(_emaill, _password);
        bool _getAllDataSuccess = false;
        if(_signinSuccess){
            if(_signinSuccess && UserStatus.Instance.UserId != null){
                _getAllDataSuccess = true;
            }
        }
        return (_signinSuccess&&_getAllDataSuccess) ? true:false;
    }

    public async Task<bool> SendDataToFirestore(string userId)
    {
        bool _sendAllDataSuccess = false;

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        // Create a new document in Firestore for the player data
        DocumentReference docRef = db.Collection("characterData").Document(userId);
        // Create a dictionary to hold the player data
        Dictionary<string, object> playerData = new Dictionary<string, object>
        {
            { "PlayerName", UserStatus.Instance.PName },
            { "CharacterName", UserStatus.Instance.CName },
            { "Gender", UserStatus.Instance.Gender },
            { "Level", UserStatus.Instance.PLevel },
            { "MaxHP", UserStatus.Instance.MAXHP },
            { "HP", UserStatus.Instance.HP },
            { "MaxMP", UserStatus.Instance.MAXMP },
            { "MP", UserStatus.Instance.MP },
            { "PhysicalAttack", UserStatus.Instance.PHYSICALATTACK },
            { "Defense", UserStatus.Instance.DEFENSE },
            { "MagicalAttack", UserStatus.Instance.MAGICALATTACK },
            { "MagicDefense", UserStatus.Instance.MAGICDEFENSE },
            { "Accuracy", UserStatus.Instance.ACCURACY },
            { "Flee", UserStatus.Instance.FLEE },
            { "Crit", UserStatus.Instance.CRIT },
            { "Strength", UserStatus.Instance.STR },
            { "Vitality", UserStatus.Instance.VIT },
            { "Agility", UserStatus.Instance.AGI },
            { "Dexterity", UserStatus.Instance.DEX },
            { "Intelligence", UserStatus.Instance.INT },
            { "Luck", UserStatus.Instance.LCK },
            { "Coin", UserStatus.Instance.COIN },
            { "Experience", UserStatus.Instance.EXP },
            { "Class", UserStatus.Instance.Class },
            {"IndexClass",UserStatus.Instance.IndexClass},
            { "Weapon", UserStatus.Instance.WEAPON},
            { "OffHand", UserStatus.Instance.OFFHAND },
            { "Armor", UserStatus.Instance.ARMOR },
            { "Cape", UserStatus.Instance.CAPE },
            { "Helm", UserStatus.Instance.HELM },
            { "Character", "Yes" },
            { "CurrentIndex", UserStatus.Instance.currentIndex },
            { "MaxIndex",UserStatus.Instance.maxIndex },

            // Assuming skill is a List<Skill> // Assuming InventoryItem is a List<Item>
            // Add more fields as necessary
        };
        // Set the document data
        docRef.SetAsync(playerData).ContinueWithOnMainThread(task => 
        {
            if (task.IsCompleted)
            {
                Debug.Log("Player data successfully written to Firestore.");
                _sendAllDataSuccess = true;
            }
            else
            {
                Debug.LogError("Error writing player data to Firestore: " + task.Exception);
                _sendAllDataSuccess = false;
            }
        });

         return _sendAllDataSuccess;
    }


    public async Task<bool> LoadDataToFirestore(string userId)
    {
        bool _getAllDataSuccess = false;

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference docRef = db.Collection("characterData").Document(userId);

        // ดึงข้อมูลจากเอกสาร
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task => 
        {
            if (task.IsCompleted && task.Result.Exists)
            {
                DocumentSnapshot snapshot = task.Result;

                // อัปเดตตัวแปรในคลาสด้วยข้อมูลจาก Firestore
               UserStatus.Instance.PName = snapshot.GetValue<string>("PlayerName");
               UserStatus.Instance.CName = snapshot.GetValue<string>("CharacterName");
               UserStatus.Instance.Gender = snapshot.GetValue<string>("Gender");
               UserStatus.Instance.PLevel = snapshot.GetValue<int>("Level");
               UserStatus.Instance.MAXHP = snapshot.GetValue<int>("MaxHP");
               UserStatus.Instance.HP = snapshot.GetValue<int>("HP");
               UserStatus.Instance.MAXMP = snapshot.GetValue<int>("MaxMP");
               UserStatus.Instance.MP = snapshot.GetValue<int>("MP");
               UserStatus.Instance.PHYSICALATTACK = snapshot.GetValue<int>("PhysicalAttack");
               UserStatus.Instance.DEFENSE = snapshot.GetValue<int>("Defense");
               UserStatus.Instance.MAGICALATTACK = snapshot.GetValue<int>("MagicalAttack");
               UserStatus.Instance.MAGICDEFENSE = snapshot.GetValue<int>("MagicDefense");
               UserStatus.Instance.ACCURACY = snapshot.GetValue<int>("Accuracy");
               UserStatus.Instance.FLEE = snapshot.GetValue<int>("Flee");
               UserStatus.Instance.CRIT = snapshot.GetValue<int>("Crit");
               UserStatus.Instance.STR = snapshot.GetValue<int>("Strength");
               UserStatus.Instance.VIT = snapshot.GetValue<int>("Vitality");
               UserStatus.Instance.AGI = snapshot.GetValue<int>("Agility");
               UserStatus.Instance.DEX = snapshot.GetValue<int>("Dexterity");
               UserStatus.Instance.INT = snapshot.GetValue<int>("Intelligence");
               UserStatus.Instance.LCK = snapshot.GetValue<int>("Luck");
               UserStatus.Instance.COIN = snapshot.GetValue<int>("Coin");
               UserStatus.Instance.EXP = snapshot.GetValue<int>("Experience");
               UserStatus.Instance.Class = snapshot.GetValue<string>("Class");
               UserStatus.Instance.IndexClass = snapshot.GetValue<int>("IndexClass"); 
               UserStatus.Instance.WEAPON = snapshot.GetValue<string>("Weapon");
               UserStatus.Instance.OFFHAND = snapshot.GetValue<string>("OffHand"); 
               UserStatus.Instance.ARMOR = snapshot.GetValue<string>("Armor"); 
               UserStatus.Instance.CAPE = snapshot.GetValue<string>("Cape"); 
               UserStatus.Instance.HELM = snapshot.GetValue<string>("Helm");
               UserStatus.Instance.currentIndex = snapshot.GetValue<int>("CurrentIndex");
               UserStatus.Instance.maxIndex = snapshot.GetValue<int>("MaxIndex");
                // แสดงข้อมูลใน Debug Log
                // skill =  classDatabase.classData[PlayerData.Instance.IndexClass].moves;

                _getAllDataSuccess = true;
            }
            else
            {
                Debug.LogError("Error getting player data or document does not exist: " + task.Exception);
                _getAllDataSuccess = false;
            }
        });

        return _getAllDataSuccess;
    }

    // public async Task LoadInventory(Inventory.Recipe _recipe){

    // }

    // public async Task SendInventory(Inventory.Recipe _recipe){

    // }
}
