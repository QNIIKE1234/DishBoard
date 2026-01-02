using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClassData : MonoBehaviour
{
    public List<ClassData> classData = new List<ClassData>();
    
	[System.Serializable]
    public class ClassData
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
        public int Crit;
        public int STR;
        public int VIT;
        public int AGI;
        public int DEX;
        public int INT;
        public int LCK;
        public List<Skill> moves = new List<Skill>();
        public string ClassDesScription;

	}   
}
