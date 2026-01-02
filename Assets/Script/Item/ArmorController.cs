using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : MonoBehaviour
{
    public List<Armor> armor = new List<Armor>();
    public List<Helm> helm = new List<Helm>();
    public List<Cape> cape = new List<Cape>();

	[System.Serializable]
    public class Armor
	{
        public Sprite ArmorIcon;
        public string setID;
        public string name;
        public string className;
        public string Type;
        public Sprite Top;
        public Sprite Shoulder_L;
        public Sprite Shoulder_R;
        public Sprite Arm_L;
        public Sprite Arm_R;
        public Sprite Hand_L;
        public Sprite Hand_R;
        public Sprite Bottom_L;
        public Sprite Bottom_R;

        public Sprite Boot_L;
        public Sprite Boot_R;

        //Status
        public int DEFENSE;
        public int MAGICDEFENSE;
        public int HP;
        public int MP;
        public int FLEE;

        public int STR;
        public int VIT;
        public int AGI;
        public int INT;
        public int DEX;
        public int LCK;
	}

	[System.Serializable]
    public class Helm
	{
        public string setID;
        public string name;
        public string className;
        public string Type;
        public Sprite ImageIcon;
        public Sprite HelmIcon;

        //Status
        public int DEFENSE;
        public int MAGICDEFENSE;
        public int HP;
        public int MP;
        public int FLEE;

        public int STR;
        public int VIT;
        public int AGI;
        public int INT;
        public int DEX;
        public int LCK;
	}

	[System.Serializable]
    public class Cape
	{
        public string setID;
        public string name;
        public string className;
        public string Type;
        public Sprite ImageIcon;

        //Status
        public int DEFENSE;
        public int MAGICDEFENSE;
        public int HP;
        public int MP;
        public int FLEE;

        public int STR;
        public int VIT;
        public int AGI;
        public int INT;
        public int DEX;
        public int LCK;
	} 
}
