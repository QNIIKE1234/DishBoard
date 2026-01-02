using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Threading.Tasks;

public enum BattleState {Start, PlayerAction,EnemyAction,PlayerMove,EnemyMove,Busy,DefAction,PlayAction,End}


public class BattleSystem : MonoBehaviour
{
    UserStatus userStatus;
    UseCase useCase;
    [Header("CharacterData")]
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialog dialogBox;
    [SerializeField] SceneChanger sceneChanger;
    [SerializeField] public ArmorController Allarmor;
    [SerializeField] public WeaponController Allweapon;
    public GameObject playerObject;
    public GameObject enemyObject;
    public GameObject playerDialogObject;
    public GameObject enemyDialogObject;
    public GameObject allPositionObject;
    public GameObject playerMove;
    public GameObject frontCanvas;
    public event Action<bool> OnBattleOver;


    [Header("ProcessAction")]
    public bool isUpdateCam = false;
    public float FollowSpeed = 5f;
    public float xOffset = 0f;
    public float yOffset = 0f;
    BattleState state;    
    int currenctAction;
    public int playerPosition = 5;
    public int enemyPosition = 7;


    public bool isPlayerTurn = false;
    public bool isGameEnd = false;
    public int skillCount = 0;
    int skillIndex;
    public List<Transform> positionData = new List<Transform>();
    public List<Image> positionHightlight = new List<Image>();
    public List<MoveQueueData> moveQueueData = new List<MoveQueueData>();


    [Header("MovePosition")]
    [SerializeField] List<Color> moveColor = new List<Color>();
    public List<int> moveLength = new List<int>();
    SkillDatabase.SkillCore CurrentSkill;
    int currentMove;
    public int moveIndex = 0;
    public int currentMoveLength;
    public TextMeshProUGUI movePosText;
    public Image imageMovePos;
    public GameObject posMove;

	[System.Serializable]
    public class MoveQueueData
	{
		public int Side;
        public int SkillIndex;

	}
    void Update(){

        onUpdatePos();
    }
  
    public void StartBattle(){
        StartCoroutine(SetupBatttle());
        posMove = playerObject;
    }
    public void onUpdatePos(){
        Vector3 oppositePos = new Vector3(-posMove.transform.position.x + xOffset, -posMove.transform.position.y + yOffset, posMove.transform.position.z);
        
        frontCanvas.transform.position = Vector3.Slerp(frontCanvas.transform.position, oppositePos, FollowSpeed * Time.deltaTime);
    }
    private IEnumerator SetupBatttle(){

        playerUnit.Setup();
        StartCoroutine(playerHud.SetData(playerUnit.MaxHP,playerUnit.HP,playerUnit.MaxMP,playerUnit.MP,playerUnit.Name,playerUnit.Level,true));
        UpdatePosition(true,5);
        enemyUnit.Setup();
        StartCoroutine(enemyHud.SetData(enemyUnit.MaxHP,enemyUnit.HP,enemyUnit.MaxMP,enemyUnit.MP,enemyUnit.Name,enemyUnit.Level,false));
        UpdatePosition(false,7);
        dialogBox.SetMoveName(playerUnit.skills,playerUnit.Level);

        yield return dialogBox.TypeDialog($"A wild {enemyUnit.Name} appeared");
        yield return new WaitForSeconds(1f);

        if(playerUnit.AGI >= enemyUnit.AGI){
            PlayerAction();
        }else{
           StartCoroutine(OnEnemyRandomMove());
        }

    }

    void PlayerAction(){
        isPlayerTurn = true;
        state = BattleState.PlayerAction;
 
        StartCoroutine(dialogBox.TypeDialog("Choose some action to process skill !!"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove(){
        state = BattleState.PlayerMove;

        dialogBox.EnableMoveSelector(true); 
        
    }



    public void UpdatePosition(bool playerSide , int value){
        if(playerSide){
            playerObject.transform.position = positionData[value].position;
        }else{
            enemyObject.transform.position = positionData[value].position;
        }


    }

    public void UpdatePlayerHP(bool playerSide , int value){
        if(playerSide){
            playerObject.transform.position = positionData[value].position;
        }else{
            enemyObject.transform.position = positionData[value].position;
        }


    }

    public void UpdateHP(bool playerSide){
        StartCoroutine(playerHud.UpDateHP(playerUnit.MaxHP,playerUnit.HP,playerUnit.MaxMP,playerUnit.MP,true));
        if(playerSide){
            
        }else{
            StartCoroutine(enemyHud.UpDateHP(enemyUnit.MaxHP,enemyUnit.HP,enemyUnit.MaxMP,enemyUnit.MP,false));
        }


    }

    public IEnumerator onGuard(bool isPlayer,bool isOpen){
        if(isPlayer){
            playerUnit.playerAnim.SetBool("onGuard",isOpen);
        }else{
            enemyUnit.enemyAnim.SetBool("onGuard",isOpen);
        }
        yield return new WaitForSeconds(0.05f);

    }

    public void onGiveUp(){
            dialogBox.EnableMoveSelector(false);
            playerDialogObject.SetActive(true);
            dialogBox.SKILL_TXT("I'm Give up T_____T");
            StartCoroutine(Giveup());
    }
    //Give UP
    public IEnumerator Giveup(){
        playerDialogObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(onGameEnd(false));
    }

    public void onSelectMove(int _index){
        skillIndex = _index;
        CurrentSkill = SkillDatabase.Instance.skillcore.Find(skills => skills.skillID == playerUnit.skills[_index].skillCode);
        if(CurrentSkill.moveSkill){
            if(CurrentSkill.moveSkill){
                dialogBox.onChoosePosition(true);
            }
        }
        else
        {
            moveQueueData[skillCount].Side = 1;
            moveQueueData[skillCount].SkillIndex = _index;
            skillCount++;
            isPlayerTurn = false;
            onCheckMove();
        }


    }

    public void onCheckMove(){
        dialogBox.onClosePosition();
        if(skillCount == 6){

            moveIndex = 0;

            state = BattleState.Busy;

            StartCoroutine(PlayMove());
        }else{
            if(isPlayerTurn == true){
                dialogBox.EnableActionSelector(true);
                StartCoroutine(dialogBox.TypeDialog("Choose some action to process skill !!"));

            }else if (isPlayerTurn == false){
                playerMove.SetActive(false);
                StartCoroutine(OnEnemyRandomMove());

            }
        }        
    }
    IEnumerator PlayMove(){
        
        skillCount=0;
        for(int i=0; i<moveQueueData.Count; i++){
            if(isGameEnd){
                moveQueueData.Clear();
            }
            else if(playerUnit.HP>0 || enemyUnit.HP >=0){
                if(moveQueueData[i].Side ==1){
                    StartCoroutine(PerformPlayerMove(moveQueueData[i].SkillIndex));
                }else{
                    StartCoroutine(PerformEnemyMove(moveQueueData[i].SkillIndex));
                }
            }
            yield return new WaitForSeconds(3f);
        }


        if(state!=BattleState.End){
            PlayerAction();
            moveLength.Clear();
        }

    }

    IEnumerator PerformPlayerMove(int _index){

        state = BattleState.Busy;
        playerUnit.state = BattleState.PlayerAction;
        enemyUnit.state = BattleState.PlayerAction;
         
        Skill move = playerUnit.skills[_index];
        SkillDatabase.SkillCore skill = SkillDatabase.Instance.skillcore.Find(skills => skills.skillID == move.skillCode);
        List<GameObject> _VFX = new List<GameObject>();
        _VFX = skill._VFX ?? null;
        bool isCanSkill = playerUnit.CalculateMagicalPoint(skill);
        UpdateHP(true);

        if(isCanSkill){
            yield return dialogBox.TypeDialog(" "+(skill.skillName)+" "+"Fainted. Not Enough Mana to use !! ");
            // PlayerAction();
        }
        else if(skill.moveSkill){
            StartCoroutine(playerUnit.onPlayAnim("Move",_VFX));
            yield return dialogBox.TypeDialog($"{playerUnit.Name} used {skill.skillName}");
            dialogBox.SKILL_TXT(skill.skillName+" !!");
   
            StartCoroutine(onMovePosition(1));
        }

        else
        {
    
            yield return dialogBox.TypeDialog($"{playerUnit.Name} is {skill.skillName}");

            playerDialogObject.transform.position = positionData[playerPosition].position; 
            dialogBox.SKILL_TXT(skill.skillName+" !!");
            playerDialogObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            if(playerPosition<enemyPosition){
                //When Plaeyer is Left Side
                if(playerPosition+skill.SkillLength >=enemyPosition){ //in
                    
                    playerUnit.outOfRange = false;
                    StartCoroutine(PerformMove(skill,true,_VFX));
                }
                
                else //far
                {
                    playerUnit.outOfRange = true;
                    StartCoroutine(playerUnit.onPlayAnim("Attack",_VFX));
                    StartCoroutine(enemyUnit.SetTextDamage(new List<string> { "Miss !!" }));
                    

                }
            }

            //When Plaeyer is Right Side
            else
            {
                if(playerPosition-skill.SkillLength <=enemyPosition){
                    playerUnit.outOfRange = false;
                    StartCoroutine(PerformMove(skill,true,_VFX));
                }
                //far
                else
                {
                    playerUnit.outOfRange = true;
                    StartCoroutine(playerUnit.onPlayAnim("Attack",_VFX));
                    StartCoroutine(enemyUnit.SetTextDamage(new List<string> { "Miss !!" }));
                }
            }

            // yield return enemyHud.UpDateHP(enemyUnit.MaxHP,enemyUnit.HP,enemyUnit.MaxMP,enemyUnit.MP);
            yield return new WaitForSeconds(1f);
            
        }

    }

    IEnumerator OnEnemyRandomMove(){
        int EnemyMovement = UnityEngine.Random.Range(0,enemyUnit.skills.Count-1);

        moveQueueData[skillCount].Side = 2;
        moveQueueData[skillCount].SkillIndex = EnemyMovement;
        skillCount++;
        isPlayerTurn = true;
        yield return new WaitForSeconds(1f); 
        onCheckMove();
    }

    IEnumerator PerformEnemyMove(int _index){
        state = BattleState.EnemyMove;
        playerUnit.state = BattleState.EnemyAction;
        enemyUnit.state = BattleState.EnemyAction;

        string skillFromEnemy = enemyUnit.skills[_index].skillCode;
        
        SkillDatabase.SkillCore skill = SkillDatabase.Instance.skillcore.Find(skill => skill.skillID == skillFromEnemy);
        List<GameObject> _VFX = new List<GameObject>();
        _VFX = skill._VFX ?? null;
        bool isCanSkill = enemyUnit.CalculateMagicalPoint(skill);

        if(isCanSkill){
            yield return dialogBox.TypeDialog(" "+(skill.skillName)+" "+"Fainted. Not Enough Mana to use !! ");
        }

        else{
            StartCoroutine(enemyHud.UpDateHP(enemyUnit.MaxHP,enemyUnit.HP,enemyUnit.MaxMP,enemyUnit.MP,false));

            yield return dialogBox.TypeDialog($"{enemyUnit.Name} used {skill.skillName}");
            enemyDialogObject.transform.position = positionData[enemyPosition].position;
            dialogBox.ESKILL_TXT(skill.skillName);
            enemyDialogObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            StartCoroutine(enemyUnit.onPlayAnim("Attack",_VFX));

            if(playerPosition<enemyPosition){  //When Enemy is Right Side
                if(enemyPosition-skill.SkillLength <=playerPosition){
                    enemyUnit.outOfRange = false;
                    StartCoroutine(PerformMove(skill,false,_VFX));
                }
                else{
                    enemyUnit.outOfRange = true;
                    StartCoroutine(playerUnit.SetTextDamage(new List<string> { "Miss !!" }));
                }
            }
            //far
            else{
                if(enemyPosition+skill.SkillLength >=playerPosition){
                    enemyUnit.outOfRange = false;
                    StartCoroutine(PerformMove(skill,false,_VFX));
  
                }else{
                    enemyUnit.outOfRange = true;
                    StartCoroutine(playerUnit.SetTextDamage(new List<string> { "Miss !!" }));
                }
            }

        } 
    }

    IEnumerator onDelaytime(float _delayTime){
        yield return new WaitForSeconds(_delayTime);
        sceneChanger.ChangeScene("Dungeon");
    }

    IEnumerator onGameEnd(bool Won){
        UserStatus.Instance.HP  = (int)playerHud.HP;
        UserStatus.Instance.MP  = (int)playerHud.MP;
        state = BattleState.End;
        if(Won)
        {
                yield return dialogBox.TypeDialog($"{enemyUnit.Name} Fainted. You win!!");
                yield return new WaitForSeconds(0.5f);
                // yield return dialogBox.TypeDialog("Obtain "+enemyUnit.Exp+" EXP");
                UserStatus.Instance.EXP += Mathf.FloorToInt(enemyUnit.Exp);
                yield return new WaitForSeconds(0.5f);
                // yield return dialogBox.TypeDialog("Obtain "+enemyUnit.Coin+" Coin");
                UserStatus.Instance.COIN += Mathf.FloorToInt(enemyUnit.Coin);
                StartCoroutine(playerHud.UpDateEXP());
                yield return new WaitForSeconds(2f);  

                playerHud.gameObject.SetActive(false);
                enemyHud.gameObject.SetActive(false);
        }
        else
        {
            playerHud.gameObject.SetActive(false);
            enemyHud.gameObject.SetActive(false);
            yield return dialogBox.TypeDialog($"{playerUnit.Name} Fainted. GGEZ");
        }
        PopupManager.Instance.RewardResult(() =>BackToDungeon(),Mathf.FloorToInt(enemyUnit.Exp),Mathf.FloorToInt(enemyUnit.Coin));
        // OnSendData();
        
    }

    public async Task OnSendData()
    { // Replace with your actual user ID
        bool result = await UserStatus.Instance.useCase.SendDataToFirestore(UserStatus.Instance.UserId);
        if (result)
        {
            Debug.Log("Data sent successfully.");
            sceneChanger.ChangeScene("Dungeon");
        }
        else
        {
            Debug.Log("Failed to send data.");
        }

        
    }

    public void BackToDungeon(){
        PopupManager.Instance.OpenLoading("isFadeIn");
        StartCoroutine(onDelaytime(2f));
    }


    public IEnumerator onMovePosition(int _side){
        if(_side == 1){
            playerPosition+=moveLength[moveIndex];
            playerObject.transform.position = positionData[playerPosition].position;
            playerDialogObject.transform.position = positionData[playerPosition].position;
        }
        else{
            enemyObject.transform.position = positionData[enemyPosition+moveLength[moveIndex]].position;
        }
        moveIndex++;
        yield return new WaitForSeconds(1.5f);
    }
    public void onMoveLeft(){
        currentMoveLength--;
        if(currentMoveLength<(0-CurrentSkill.SkillLength)){
            currentMoveLength = 0-CurrentSkill.SkillLength; 
        }
        UpdateMoveColor();

    }
    public void onMoveRight(){
        currentMoveLength++;
        if(currentMoveLength>CurrentSkill.SkillLength){
            currentMoveLength = CurrentSkill.SkillLength; 
        }
        UpdateMoveColor();
    }

    public void UpdateMoveColor(){
        if(currentMoveLength>0){
            dialogBox.selectMpvePos.SetActive(true);
            movePosText.color = moveColor[0];
            imageMovePos.color = moveColor[0];
            movePosText.text = ""+currentMoveLength;
        }else if (currentMoveLength==0){
            dialogBox.selectMpvePos.SetActive(false);
            movePosText.color = Color.grey;
            imageMovePos.color = Color.grey;
            movePosText.text = ""+currentMoveLength;
        }

        else if(currentMoveLength<0){
            dialogBox.selectMpvePos.SetActive(true);
            movePosText.color = moveColor[1];
            imageMovePos.color = moveColor[1];
            movePosText.text = ""+Math.Abs(currentMoveLength);
        }
 
    }
    public void onMoveSave(){
        dialogBox.onChoosePosition(false);
        onMoveSave(skillIndex,true,currentMoveLength);

    }
    public void onMoveSave(int _skillIndex, bool _isPlayer,int moveValue = 0){
        if(_isPlayer){
            moveQueueData[skillCount].Side = 1;
            moveQueueData[skillCount].SkillIndex = _skillIndex;
            moveLength.Add(currentMoveLength);
            skillCount++;
            isPlayerTurn = false;
            onCheckMove();
        }else{
            
        }
    }

    public IEnumerator PerformMove(SkillDatabase.SkillCore skill,bool _isPlayer, List<GameObject> _VFX){
        bool isFainted = false;
        if(_isPlayer)
        {
            enemyDialogObject.SetActive(false);

            if(skill.skillType.ToString() == "PHYSICALATTACK" ||skill.skillType.ToString() == "MAGICALATTACK"){
                StartCoroutine(playerUnit.onPlayAnim("Attack",_VFX));
                isFainted = enemyUnit.CalculateDamage(skill,playerUnit.Attack,playerUnit.Spell,playerUnit.Accuracy,playerUnit.Level,playerUnit.Crit);
            }
            else if(skill.skillType.ToString() == "BUFF"){
                StartCoroutine(playerUnit.onPlayAnim("Cast",_VFX));
                StartCoroutine(playerUnit.onSetBuff(skill));
            }

            else if(skill.skillType.ToString() == "DEBUFF"){
                enemyUnit.onSetBuff(skill);
            }


            UpdateHP(false);


            if(isFainted){
                isGameEnd = true;
                enemyUnit.enemyAnim.SetBool("onDie",true);
                StartCoroutine(onGameEnd(isGameEnd));
                moveQueueData.Clear();
                yield break;
            
            }
            playerDialogObject.SetActive(false);
            enemyDialogObject.SetActive(false);     
        }

        else
        {
            enemyDialogObject.SetActive(false);


            if(skill.skillType.ToString() == "PHYSICALATTACK" ||skill.skillType.ToString() == "MAGICALATTACK"){
                // StartCoroutine(playerUnit.onPlayAnim("Attack",_VFX,skill.SkillLength));
                isFainted = playerUnit.CalculateDamage(skill,enemyUnit.Attack,enemyUnit.Spell,enemyUnit.Accuracy,enemyUnit.Level,enemyUnit.Crit);
            }
            else if(skill.skillType.ToString() == "BUFF"){
                StartCoroutine(enemyUnit.onPlayAnim("Cast",_VFX));
                StartCoroutine(enemyUnit.onSetBuff(skill));
            }

            else if(skill.skillType.ToString() == "DEBUFF"){
                playerUnit.onSetBuff(skill);
            }

            UpdateHP(true);

            
            if(isFainted){
                isGameEnd = true;
                StartCoroutine(onGameEnd(isGameEnd));
                moveQueueData.Clear();
            }
            enemyDialogObject.SetActive(false);
            playerDialogObject.SetActive(false);                  
        }
    }





}
