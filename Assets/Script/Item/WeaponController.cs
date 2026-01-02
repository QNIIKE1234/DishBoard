using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<Weapon> weapon = new List<Weapon>();
    public List<OffHand> offHand = new List<OffHand>();

	[System.Serializable]
    public class Weapon
	{
        public Sprite WeaponImg;
        public Sprite WeaponIcon;
        public string setID;
        public string name;
        public string className;
        public string Type;
        //Status
        public int Attack;
        public int Mattack;
        public int Accuracy;
        public int Crit;

        public int STR;
        public int VIT;
        public int AGI;
        public int INT;
        public int DEX;
        public int LCK;

        public string description;
	}

	[System.Serializable]
    public class OffHand
	{
        public Sprite ImageIcon;
        public string setID;
        public string name;
        public string className;
        public string Type;
        //Status
        public int Attack;
        public int Mattack;
        public int Accuracy;
        public int Crit;

        public int STR;
        public int VIT;
        public int AGI;
        public int INT;
        public int DEX;
        public int LCK;

        public string description;
	}
}
