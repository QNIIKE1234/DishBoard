using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDatabase : MonoBehaviour
{
    public static SkillDatabase Instance { get; private set; }

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
    public List<SkillCore> skillcore = new List<SkillCore>();

    [System.Serializable]
    public class SkillCore
	{

        public string skillID;
        public string skillName;
        public string SkillDescription;
        public Sprite skillIconPath;
        public SkillType skillType;
        public bool moveSkill;
        public int SkillCost;
        public int SkillHit;
        public int SkillLength;
        public List<int> Side = new List<int>();
        public string SkillSubWeapon;
        public ElementType elementType;
        
        public List<AttackSkill> attackSkill = new List<AttackSkill>();
        public List<BuffSkill> buffSkill = new List<BuffSkill>();
        public List<DebuffSkill> debuffSkill = new List<DebuffSkill>();
        public List<GameObject> _VFX = new List<GameObject>();

        public int STR;
        public int AGI;
        public int VIT;
        public int DEX;
        public int INT;
        public int LCK;


	}


	[System.Serializable]
    public class AttackSkill
	{

        public int SkillPhysicalAttack;
        public int SkillMagicalAttack;


	}


	[System.Serializable]
    public class BuffSkill
	{
		public string skillbuff;
        public int skillDuration;
        public int skillValue;


	}
	[System.Serializable]
    public class DebuffSkill
	{
		public string skillbuff;
        public int skillDuration;
        public int skillValue;


	}
    public enum SkillType{
        PHYSICALATTACK,
        MAGICALATTACK,
        BUFF,
        DEBUFF,
        MOVE
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
