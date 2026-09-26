using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public partial class BattleUnit : MonoBehaviour
{
    // [SerializeField] PlayerData  UserStatus.Instance.character;
    [SerializeField] CharacterDataPrefabs _baseDataEnemy;
    [SerializeField] int clevel;
    [SerializeField] bool isPlayerUnit;
    [SerializeField] bool isEnemyUnit;
    [SerializeField] StageList stageList;
    [SerializeField] BattleSystem battleSystem;
    public GameObject playerCharacter;
    CharacterAnimationController playerEquipment;
    public GameObject enemyCharacter;
    public GameObject text_damage;
    public GameObject buffIcon;
    public Transform buffIconContext;
    public Animator playerAnim;
    public Animator enemyAnim;
    Vector3 orginalPos;
    public GameObject _vfx;
    public float FollowSpeed = 5f; 
    List<CharacterDatabase> monsterData = new List<CharacterDatabase>();
    public List<GameObject> playerPrefabs = new List<GameObject>();
    //Data

    public int Level ;
    public string Class;
    public int HP ;
    public int MP ;
    public string Name ;
    public List<Skill> skills = new List<Skill>();
    public List<GameObject> buffIconGameObject = new List<GameObject>();
    public int Attack;
    public int Defense;
    public int Spell;
    public int SpellGuard;
    public int MaxHP;
    public int MaxMP;
    public int Accuracy;
    public int AttackRage;
    public int Crit;
    public int Flee;
    public int STR;
    public int VIT;
    public int AGI;
    public int DEX;
    public int INT;
    public int LCK;
    public string element;

    //Reward
    public float Exp;
    public float Coin;
    public int physicalAttackCount = 1;
    public int enemy_physicalAttackCount = 1;
    public int magicalAttackCount = 1;

    public bool outOfRange;

    public BattleState state;
    public List<BuffSkill> buffSkillList = new List<BuffSkill>();

	[System.Serializable]
    public class BuffSkill
	{
		public string skillbuff;
        public int skillDuration;
        public int skillValue;
        public bool isBuff;


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
    public void Setup(){

        if(isPlayerUnit){
            //CreatePlayerPrefabs
            GameObject _PLAYER = Instantiate(playerPrefabs[0],playerCharacter.transform);
            playerEquipment =_PLAYER.GetComponent<CharacterAnimationController>();
            playerEquipment.OnUpDateEquipment();
            _PLAYER.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
            battleSystem.playerObject = _PLAYER;
            playerAnim = _PLAYER.GetComponent<Animator>();
            playerAnim.SetBool("onReady",true);
            //SetPlayerData
            //  UserStatus.Instance.character = UserStatus.Instance.character;
            Class = !string.IsNullOrEmpty(UserStatus.Instance.Class) ? UserStatus.Instance.Class : UserStatus.Instance.CName;
            if (string.IsNullOrEmpty(UserStatus.Instance.Class)) UserStatus.Instance.Class = Class;
            if (string.IsNullOrEmpty(UserStatus.Instance.CName)) UserStatus.Instance.CName = Class;

            if (UserStatus.Instance.PHYSICALATTACK < 120 && UserStatus.Instance.MAGICALATTACK < 120)
            {
                UserStatus.Instance.GetExtraStat();
            }

            Level =  UserStatus.Instance.PLevel;
            MaxHP =  UserStatus.Instance.MAXHP;
            HP =  UserStatus.Instance.HP;
            MaxMP =  UserStatus.Instance.MAXMP;
            MP =  UserStatus.Instance.MP;

            if (HP <= 0 || HP > MaxHP)
            {
                HP = MaxHP;
                UserStatus.Instance.HP = MaxHP;
            }
            if (MP <= 0 || MP > MaxMP)
            {
                MP = MaxMP;
                UserStatus.Instance.MP = MaxMP;
            }
            STR =  UserStatus.Instance.STR;
            VIT = UserStatus.Instance.VIT;
            AGI =  UserStatus.Instance.AGI;
            DEX =  UserStatus.Instance.DEX;
            INT =  UserStatus.Instance.INT;
            LCK=  UserStatus.Instance.LCK;

            Attack = Mathf.FloorToInt( UserStatus.Instance.PHYSICALATTACK);
            Spell = Mathf.FloorToInt( UserStatus.Instance.MAGICALATTACK);
            Accuracy =  UserStatus.Instance.ACCURACY;
            AttackRage =  UserStatus.Instance.ATTACKRANGE;
            Flee =  UserStatus.Instance.FLEE;
            Crit =  UserStatus.Instance.CRIT;
            Defense = Mathf.FloorToInt( UserStatus.Instance.DEFENSE);
            SpellGuard = Mathf.FloorToInt( UserStatus.Instance.MAGICDEFENSE);
            Name = UserStatus.Instance.PName;
            skills =  GetSkillData();
            element =  UserStatus.Instance.ELEMENTAL.ToString();
        }
    
        else{
            //Random Enemy
            monsterData = _baseDataEnemy.stageMonsterData[0].monsterData;
            int randomMonster = Random.Range(0,monsterData.Count);
            int randomLevel = Random.Range(monsterData[randomMonster].LEVEL_MIN,monsterData[randomMonster].LEVEL_MAX);
            //CreateEnemyPrefabs
            GameObject _ENEMY = Instantiate(monsterData[randomMonster].characterObject,enemyCharacter.transform);
            _ENEMY.transform.localScale = new Vector3(-0.8f, 0.8f, 1f);
            battleSystem.enemyObject = _ENEMY;
            enemyAnim = _ENEMY.GetComponent<Animator>();
            enemyAnim.SetBool("onReady",true);
            //SetEnemyData
            monsterData[randomMonster].LEVEL = randomLevel;
            Level = randomLevel;
            MaxHP = monsterData[randomMonster].MAXHP+ (Level*3);
            HP = monsterData[randomMonster].HP+ (Level*3);
            MaxMP = monsterData[randomMonster].MAXMP+ (Level*3);
            MP = monsterData[randomMonster].MP+ (Level*3);

            STR = monsterData[randomMonster].STR;
            VIT = monsterData[randomMonster].VIT;
            AGI = monsterData[randomMonster].AGI;
            DEX = monsterData[randomMonster].DEX;
            INT = monsterData[randomMonster].INT;
            LCK= monsterData[randomMonster].LCK;

            Attack = Mathf.FloorToInt((monsterData[randomMonster].PHYSICALATTACK)+ (Level*3));
            Spell = Mathf.FloorToInt((monsterData[randomMonster].MAGICALATTACK)+ (Level*3));
            Defense = Mathf.FloorToInt((monsterData[randomMonster].DEFENSE)+ (Level*3));
            SpellGuard = Mathf.FloorToInt((monsterData[randomMonster].MAGICDEFENSE)+ (Level*3));
            Accuracy =  monsterData[randomMonster].ACCURACY;
            AttackRage =  monsterData[randomMonster].ATTACKRANGE;
            Flee =  monsterData[randomMonster].FLEE;
            Crit = monsterData[randomMonster].CRIT;

            Name = monsterData[randomMonster].NAME;
            Exp = monsterData[randomMonster].EXP+randomLevel*5;
            Coin = monsterData[randomMonster].COIN+randomLevel;
            skills = monsterData[randomMonster].skill;
            element = monsterData[randomMonster].ELEMENTAL.ToString();



            
        }

    }

    public List<Skill> GetSkillData(){
        List<Skill> skills = new List<Skill>();

        // 1. Always guarantee basic skills (Normal Attack & Move)
        string[] basicSkills = new string[] { "SKI_000_NormalAttack", "SKI_000_Move" };
        foreach (string basicCode in basicSkills)
        {
            Skill basicSkill = MasterISkillData.masterSkillList.Find(s => s != null && s.skillCode == basicCode);
            if (basicSkill != null && !skills.Exists(s => s.skillCode == basicCode))
            {
                skills.Add(basicSkill);
            }
        }

        // 2. Add class skills from classDatabase based on player's level
        if (UserStatus.Instance != null && UserStatus.Instance.classDatabase != null && UserStatus.Instance.classDatabase.classData != null)
        {
            var classData = UserStatus.Instance.classDatabase.classData.Find(c => 
                c != null && (c.Class == UserStatus.Instance.CName || c.Class == UserStatus.Instance.Class));
            
            if (classData == null && UserStatus.Instance.IndexClass >= 0 && UserStatus.Instance.IndexClass < UserStatus.Instance.classDatabase.classData.Count)
            {
                classData = UserStatus.Instance.classDatabase.classData[UserStatus.Instance.IndexClass];
            }

            if (classData != null && classData.moves != null)
            {
                foreach (var classMove in classData.moves)
                {
                    if (classMove != null && classMove.skillLevel <= UserStatus.Instance.PLevel)
                    {
                        Skill foundSkill = MasterISkillData.masterSkillList.Find(s => s != null && s.skillCode == classMove.skillCode);
                        if (foundSkill != null && !skills.Exists(s => s.skillCode == foundSkill.skillCode))
                        {
                            foundSkill.skillLevel = classMove.skillLevel;
                            skills.Add(foundSkill);
                        }
                    }
                }
            }
        }

        // 3. Also check stat-based unlocked skills (with clamped non-negative stats so negative AGI penalties don't block skills)
        if (SkillDatabase.Instance != null && SkillDatabase.Instance.skillcore != null && UserStatus.Instance != null)
        {
            int pStr = Mathf.Max(0, UserStatus.Instance.STR);
            int pInt = Mathf.Max(0, UserStatus.Instance.INT);
            int pVit = Mathf.Max(0, UserStatus.Instance.VIT);
            int pDex = Mathf.Max(0, UserStatus.Instance.DEX);
            int pAgi = Mathf.Max(0, UserStatus.Instance.AGI);
            int pLck = Mathf.Max(0, UserStatus.Instance.LCK);

            foreach (SkillDatabase.SkillCore addSkill in SkillDatabase.Instance.skillcore)
            {
                if (addSkill == null) continue;

                int reqInt = (addSkill.skillID == "SKI_003_BraveSpirit") ? 0 : addSkill.INT;

                if (pStr >= addSkill.STR && pInt >= reqInt &&
                    pVit >= addSkill.VIT && pDex >= addSkill.DEX &&
                    pAgi >= addSkill.AGI && pLck >= addSkill.LCK)
                {
                    Skill newSkill = MasterISkillData.masterSkillList.Find(s => s != null && s.skillCode == addSkill.skillID);
                    if (newSkill != null && !skills.Exists(s => s.skillCode == newSkill.skillCode))
                    {
                        skills.Add(newSkill);
                    }
                }
            }
        }

        return skills;
    }


    public bool CalculateMagicalPoint(SkillDatabase.SkillCore _move)
    {
        if (_move == null) return false;

        if (MP < _move.SkillCost)
        {
            // Not enough mana
            return true;
        }

        MP -= _move.SkillCost;
        if (isPlayerUnit)
        {
            StartCoroutine(SetNewMP(_move.SkillCost));
        }

        return false;
    }

   public bool CalculateDamage(SkillDatabase.SkillCore _move ,int _PATK ,int _MATK,float _ACCURACY,int _Level,int _Crit)
   {
        List<string> damage = new List<string>();
        int FinalSum = 0;
        float modifier = Random.Range(0.85f, 1f);

        float physRatio = (_move.attackSkill != null && _move.attackSkill.Count > 0) ? (_move.attackSkill[0].SkillPhysicalAttack / 100f) : 1f;
        float magRatio = (_move.attackSkill != null && _move.attackSkill.Count > 0) ? (_move.attackSkill[0].SkillMagicalAttack / 100f) : 0f;

        float physDamage = 0f;
        float magDamage = 0f;

        if (physRatio > 0f)
        {
            float rawPhys = _PATK * physRatio;
            physDamage = Mathf.Max(rawPhys * 0.25f, rawPhys - ((float)Defense * 0.5f));
        }

        if (magRatio > 0f)
        {
            float rawMag = _MATK * magRatio;
            magDamage = Mathf.Max(rawMag * 0.25f, rawMag - ((float)SpellGuard * 0.5f));
        }

        if (physRatio <= 0f && magRatio <= 0f)
        {
            float rawPhys = _PATK;
            physDamage = Mathf.Max(rawPhys * 0.25f, rawPhys - ((float)Defense * 0.5f));
        }

        float FinalDamage = (physDamage + magDamage) + (_Level + 10);
        float sumElemnt = ElementDamage(_move.elementType.ToString());
        int sumDamage = Mathf.FloorToInt(FinalDamage);

        //-------------------------------------------------------------------------------------------------------

        for(int i=0; i<_move.SkillHit; i++){

                bool onMissDamage = onMiss((int)_ACCURACY);
                float SumDamage = onCalculateDamage(modifier,sumDamage,sumElemnt,FinalDamage);

                if(onCritical(_Crit)){
                    int critDmg = (int)(SumDamage * 3f);
                    damage.Add(""+critDmg);
                    FinalSum += critDmg;
                }

                else if(!onMissDamage){
                    damage.Add(""+(int)SumDamage);
                    FinalSum += (int)SumDamage;   
                }

                else{
                     damage.Add("Miss !!");
                }           
        }

        HP -= FinalSum;


        //-------------------------------------------------------------------------------------------------------

        // int sumDamageAndHit = Mathf.FloorToInt((FinalDamage*modifier)*sumElemnt)*_move.SkillHit;

        if(isPlayerUnit){
            StartCoroutine(onSetDeBuff(_move,true));
        }else{
            StartCoroutine(onSetDeBuff(_move,false));
        }

        StartCoroutine(SetTextDamage(damage));

        if(HP<=0){
            return true;
        }
        else{
           return false;
        }

   }
    public int onCalculateDamage(float _modifier ,int _sumDamage,float _sumElement,float _FinalDamage){
        int damage = Mathf.FloorToInt((_FinalDamage*_modifier)*_sumElement);
        if(damage<=0){
            damage =1;
        }
        return damage;

    }
    public bool onMiss(int accuracy){
        int acc = Random.Range(0,accuracy);
        if(acc>Flee){
            return false;
        }else{
            return true;
        }

    }

    public bool onCritical(int _Crit){
        int critChance = Random.Range(0,101);
        if(_Crit>critChance){
            return true;
        }else{
            return false;
        }
    }

    public IEnumerator SetNewHP(int value){
        if (UserStatus.Instance != null) {
            UserStatus.Instance.HP = Mathf.Max(0, UserStatus.Instance.HP - value);
        }
        yield return new WaitForSeconds(1f);
    }
    public IEnumerator SetNewMP(int value){
        if (UserStatus.Instance != null) {
            UserStatus.Instance.MP = Mathf.Max(0, UserStatus.Instance.MP - value);
        }
        yield return new WaitForSeconds(1f);

    }
    public IEnumerator SetTextDamage(List<string> damage = null){
        GameObject _damage;
        if(physicalAttackCount>2){
            physicalAttackCount=1;
        }
        if(enemy_physicalAttackCount>2){
            enemy_physicalAttackCount=1;
        }

        yield return new WaitForSeconds(0.5f);
        for(int i=0; i<damage.Count; i++){
                if(isPlayerUnit){
                    _damage = Instantiate(text_damage,battleSystem.positionData[battleSystem.playerPosition]);
                }else{
                    _damage = Instantiate(text_damage,battleSystem.positionData[battleSystem.enemyPosition]);
                    
                }

                TextDamage _notice = _damage.GetComponent<TextDamage>();

                _notice.SetDamageData(damage[i]); 

                
                Destroy(_damage,2.5f);
            
            yield return new WaitForSeconds(0.15f);
        }
    }

    public IEnumerator onPlayAnim(string _type,List<GameObject> _move){
        if (battleSystem == null) yield break;
        if(isPlayerUnit){
            if(_type == "Attack"){

                battleSystem.posMove = battleSystem.enemyObject;
                playerAnim.SetBool("onAttack"+2,true);
                StartCoroutine(battleSystem.onGuard(false,true));

                yield return new WaitForSeconds(0.2f);

                GameObject VFX1 = Instantiate(_move[0],battleSystem.playerObject.transform);
                Destroy(VFX1,1f);


                if(!outOfRange){
                    GameObject VFX2 = Instantiate(_move[1],battleSystem.positionData[battleSystem.enemyPosition].transform);
                    Destroy(VFX2,1f);
                }

                yield return new WaitForSeconds(0.7f);

                battleSystem.posMove = battleSystem.playerObject;
                StartCoroutine(battleSystem.onGuard(false,false));
                playerAnim.SetBool("onAttack"+2,false);
                // physicalAttackCount =physicalAttackCount+1;     
                // physicalAttackCount =physicalAttackCount+1;
            }
            else if(_type == "Move"){
                playerAnim.SetBool("onDash",true);
                yield return new WaitForSeconds(0.35f);
                playerAnim.SetBool("onDash",false); 
            }
            else{
                playerAnim.SetBool("onCastMagic",true);
                GameObject VFX = Instantiate(_move[0],battleSystem.playerObject.transform);
                yield return new WaitForSeconds(0.7f);
                Destroy(VFX,0.4f);
                yield return new WaitForSeconds(0.3f);
                playerAnim.SetBool("onCastMagic",false);
            }

        }

        
        else{
            if(_type == "Attack"){
                enemyAnim.SetBool("onAttack"+enemy_physicalAttackCount,true);
                StartCoroutine(battleSystem.onGuard(true,true));
                yield return new WaitForSeconds(0.2f);
                GameObject VFX1 = Instantiate(_move[0],battleSystem.enemyObject.transform);
                Destroy(VFX1,1f);

                if(!outOfRange){
                    GameObject VFX2 = Instantiate(_move[1],battleSystem.positionData[battleSystem.playerPosition].transform);
                    Destroy(VFX2,1f);
                }
               
                yield return new WaitForSeconds(0.7f);
                StartCoroutine(battleSystem.onGuard(true,false));
                enemyAnim.SetBool("onAttack"+enemy_physicalAttackCount,false);
                // enemy_physicalAttackCount =enemy_physicalAttackCount+1;      
            }else{
                enemyAnim.SetBool("onCastMagic",true);
                GameObject VFX = Instantiate(_move[0],enemyCharacter.transform);
                yield return new WaitForSeconds(0.7f);
                Destroy(VFX,0.4f);
                yield return new WaitForSeconds(0.3f);
                enemyAnim.SetBool("onCastMagic",false);
            }

        }

    }

}
