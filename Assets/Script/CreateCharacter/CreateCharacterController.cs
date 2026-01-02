using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;
public class CreateCharacterController : MonoBehaviour
{
    [SerializeField] ArmorController _armor;
    [SerializeField] WeaponController _Weapon;
    [SerializeField] BattleDialog dialogBox;
    public SceneChanger sceneChanger;
    private FirebaseFirestore firestore;
    public Animator sceneAnimator;
    public CharacterAnimationController player;
    public GameObject panelCharacter;
    public GameObject panelStatus;
    public CharacterClassData classDatabase;
    public List<GameObject> skillButton = new List<GameObject>();
    public List<GameObject> moveSpriteObject = new List<GameObject>();
    public List<GameObject> CharacterGender = new List<GameObject>();
    public List<GameObject> CharacterGenerate = new List<GameObject>();
    public List<Sprite> CharacterWeapon = new List<Sprite>();
    public List<Sprite> CharacterSubWeapon = new List<Sprite>();

    public string userId ;

    public Transform content;
    public GameObject skillIconGBJ;
    public Transform skillIcon;
    public TMP_InputField playerName;
    public TextMeshProUGUI _Class;
    public TextMeshProUGUI _ClassTitle;
    public TextMeshProUGUI _Health;
    public TextMeshProUGUI _Mana;
    public TextMeshProUGUI _ATK;
    public TextMeshProUGUI _MATK;
    public TextMeshProUGUI _DEF;
    public TextMeshProUGUI _MDEF;
    public TextMeshProUGUI _ACC;
    public TextMeshProUGUI _FLEE;
    public TextMeshProUGUI _STR;
    public TextMeshProUGUI _VIT;
    public TextMeshProUGUI _AGI;
    public TextMeshProUGUI _DEX;
    public TextMeshProUGUI _INT;
    public TextMeshProUGUI _LCK;
    public TextMeshProUGUI _DES;

    public int _indexClass =0;
    public int _gender;

    void Start()
    {
        userId = UserStatus.Instance.UserId;
        StartCoroutine(dialogBox.TypeDialog("Please select your Character gender!!"));
    }

    // Update is called once per frame
    public void onHold(int _index){
        if(_index == 0){
            sceneAnimator.SetBool("onHold_M",true);
            sceneAnimator.SetBool("onHold_F",false);
            _gender=0;
        }else if(_index == 1){
            sceneAnimator.SetBool("onHold_M",false);
            sceneAnimator.SetBool("onHold_F",true);
            _gender=1;
        }else{
            sceneAnimator.SetBool("onHold_M",false);
            sceneAnimator.SetBool("onHold_F",false); 
        }
    }
    public void onBackToSelect(){
        panelStatus.SetActive(false);
        panelCharacter.SetActive(true);
        sceneAnimator.SetBool("onHold_M",false);
        sceneAnimator.SetBool("onHold_F",false);
        sceneAnimator.SetBool("onSelect_M",false);
        sceneAnimator.SetBool("onSelect_F",false);
        sceneAnimator.SetBool ("onSelectAlready",false);
        StartCoroutine(dialogBox.TypeDialog("Please select your Character gender!!"));

    }

    public void onSelectGender(int _index){

        if(_index == 0){
            // PlayerPrefs.SetString("PlayerGender","Male");
            sceneAnimator.SetBool("onSelect_M",true);
            sceneAnimator.SetBool("onSelect_F",false);
        }else if(_index == 1){
            // PlayerPrefs.SetString("PlayerGender","Female");
            sceneAnimator.SetBool("onSelect_M",false);
            sceneAnimator.SetBool("onSelect_F",true);
        }else{
            sceneAnimator.SetBool("onSelect_M",false);
            sceneAnimator.SetBool("onSelect_F",false); 
        }
        sceneAnimator.SetBool ("onSelectAlready",true);
        StartCoroutine(onDelay(1f));
    
    }

    public void onNextClass(){
        _indexClass++;
        if(_indexClass>1){
            //classDatabase.classData.Count-1
            _indexClass =0;
        }
        GenerateData();
    }
    public void onPreviousClass(){
        _indexClass--;
        if(_indexClass<0){
            _indexClass =0;
        }
        GenerateData();
    }

    void GenerateData(){
        int selectSetClass ;
        if(PlayerPrefs.GetString("PlayerGender") == "Female"){
            selectSetClass = 5;
        }else{
            selectSetClass = 0;
        }
        if(CharacterGenerate.Count>=1){
            Destroy(CharacterGenerate[0]);
            CharacterGenerate.Clear();
        }


        PlayerPrefs.SetString("PlayerClass",classDatabase.classData[_indexClass].Class);
        GameObject _Player = Instantiate(CharacterGender[_gender],content);
        CharacterGenerate.Add(_Player);
        player = _Player.GetComponent<CharacterAnimationController>();
        player.UpdateAnimClass();

        player.Weapon.sprite = CharacterWeapon[_indexClass+selectSetClass];
        player.OffHand.sprite = CharacterSubWeapon[_indexClass+selectSetClass];

        player.Top.sprite = _armor.armor[_indexClass+selectSetClass].Top;
        player.Shoulder_L.sprite = _armor.armor[_indexClass+selectSetClass].Shoulder_L;
        player.Shoulder_R.sprite = _armor.armor[_indexClass+selectSetClass].Shoulder_R;
        player.Arm_L.sprite = _armor.armor[_indexClass+selectSetClass].Arm_L;
        player.Arm_R.sprite = _armor.armor[_indexClass+selectSetClass].Arm_R;
        player.Hand_L.sprite = _armor.armor[_indexClass+selectSetClass].Hand_L;
        player.Hand_R.sprite = _armor.armor[_indexClass+selectSetClass].Hand_R;

        player.Bottom_L.sprite = _armor.armor[_indexClass+selectSetClass].Bottom_L;
        player.Bottom_R.sprite = _armor.armor[_indexClass+selectSetClass].Bottom_R;
        player.Boot_L.sprite = _armor.armor[_indexClass+selectSetClass].Boot_L;
        player.Boot_R.sprite = _armor.armor[_indexClass+selectSetClass].Boot_R;

        player.Helm.sprite = _armor.helm[_indexClass].ImageIcon;
        player.Cape.sprite = _armor.cape[_indexClass].ImageIcon;

        _Class.text =classDatabase.classData[_indexClass].Class;
        _ClassTitle.text = classDatabase.classData[_indexClass].Class;
        _Health.text ="Health :   "+ classDatabase.classData[_indexClass].Health;
        _Mana.text =  "Mana   :   "+ classDatabase.classData[_indexClass].Mana;
        _STR.text =   "STR    :   "+ classDatabase.classData[_indexClass].STR;
        _VIT.text =   "VIT    :   "+ classDatabase.classData[_indexClass].VIT;
        _AGI.text =   "AGI    :   "+ classDatabase.classData[_indexClass].AGI;
        _DEX.text =   "DEX    :   "+ classDatabase.classData[_indexClass].DEX;
        _INT.text =   "INT    :   "+ classDatabase.classData[_indexClass].INT;
        _LCK.text =   "LCK    :   "+ classDatabase.classData[_indexClass].LCK;

        _ATK.text =   "ATTACK     :   "+ classDatabase.classData[_indexClass].Attack;
        _MATK.text =   "M.ATTACK  :   "+ classDatabase.classData[_indexClass].MagicAttack;
        _DEF.text =   "DEF  :   "+ classDatabase.classData[_indexClass].Defense;
        _MDEF.text =   "MDEF :   "+ classDatabase.classData[_indexClass].MagicDefense;
        _ACC.text =   "ACC  :   "+ classDatabase.classData[_indexClass].Accuracy;
        _FLEE.text =   "FLEE :   "+ classDatabase.classData[_indexClass].Flee;

        _DES.text = classDatabase.classData[_indexClass].ClassDesScription;
        SetMoveName();
    }

    public void SetMoveName()
    {
        foreach(GameObject tartget in skillButton){
            Destroy(tartget);
        }
        skillButton.Clear();

        for (int i = 0; i< classDatabase.classData[_indexClass].moves.Count; i++){
            Skill skill =  MasterISkillData.masterSkillList.Find(skill => skill.skillCode ==  classDatabase.classData[_indexClass].moves[i].skillCode);
            if (skill != null && i>1)
            {
                GameObject _skillIcon = Instantiate(skillIconGBJ, skillIcon);
                SkillDescription _Description = _skillIcon.GetComponent<SkillDescription>();
                _Description.SetData(true,skill.skillName);
                // moveSprite[i].sprite = skill.skillIconPath ?? Resources.Load<Sprite>("Empty");
                skillButton.Add(_skillIcon);
                
            }
            else
            {

            }

        }

    }

    public async void onClickPlay(){
        UserStatus.Instance.IndexClass = _indexClass;
        PlayerPrefs.SetString("PlayerName",playerName.text);
        UserStatus.Instance.PName = PlayerPrefs.GetString("PlayerName");
        UserStatus.Instance.CName =  classDatabase.classData[_indexClass].Class;
        UserStatus.Instance.MAXHP =  classDatabase.classData[_indexClass].Health;
        UserStatus.Instance.HP =  classDatabase.classData[_indexClass].Health;
        UserStatus.Instance.MAXMP =  classDatabase.classData[_indexClass].Mana;
        UserStatus.Instance.MP =  classDatabase.classData[_indexClass].Mana;
        UserStatus.Instance.PHYSICALATTACK =  classDatabase.classData[_indexClass].Attack;
        UserStatus.Instance.DEFENSE =  classDatabase.classData[_indexClass].Defense;
        UserStatus.Instance.MAGICALATTACK =  classDatabase.classData[_indexClass].MagicAttack;
        UserStatus.Instance.MAGICDEFENSE =  classDatabase.classData[_indexClass].MagicDefense;
        UserStatus.Instance.ACCURACY =  classDatabase.classData[_indexClass].Accuracy;
        UserStatus.Instance.FLEE =  classDatabase.classData[_indexClass].Flee;
        UserStatus.Instance.CRIT =  classDatabase.classData[_indexClass].Crit;

        UserStatus.Instance.STR =  classDatabase.classData[_indexClass].STR;
        UserStatus.Instance.VIT =  classDatabase.classData[_indexClass].VIT;
        UserStatus.Instance.AGI =  classDatabase.classData[_indexClass].AGI;
        UserStatus.Instance.DEX =  classDatabase.classData[_indexClass].DEX;
        UserStatus.Instance.INT =  classDatabase.classData[_indexClass].INT;
        UserStatus.Instance.LCK =  classDatabase.classData[_indexClass].LCK;

        UserStatus.Instance.PLevel = 5;

        
        if(_gender == 0){
            UserStatus.Instance.Gender = "Male";
            UserStatus.Instance.ARMOR = "Set001_M";
        }else{
            UserStatus.Instance.Gender = "Female";
            UserStatus.Instance.ARMOR = "Set001_F";
        }

        if( UserStatus.Instance.CName == "WarriorClass" ){
            UserStatus.Instance.WEAPON = "WEAPON_003";
            UserStatus.Instance.OFFHAND = "OFFHAND_001";
            UserStatus.Instance.ARMOR = "SET004_M";
            UserStatus.Instance.CAPE = "ITM_CPE_001";
            UserStatus.Instance.HELM = "ITM_HLM_001";

        }else if (UserStatus.Instance.CName == "SorceressClass"){
            UserStatus.Instance.WEAPON = "WEAPON_008";
            UserStatus.Instance.OFFHAND = "OFFHAND_002";
            UserStatus.Instance.ARMOR = "SET005_M";
            UserStatus.Instance.CAPE = "ITM_CPE_002";
            UserStatus.Instance.HELM = "ITM_HLM_002";
        }
        Debug.Log("playerClassName = "+UserStatus.Instance.CName);
        UserStatus.Instance.onUpdateData();
        
        await OnLoadLevel();
    }

    public async Task OnLoadLevel(){
        //UserStatus.Instance.SendDataToFirestore(userId);
        PopupManager.Instance.OpenLoading("isFadeIn");
        bool result = await UserStatus.Instance.useCase.SendDataToFirestore(userId);

        if (result)
        {

            Debug.Log("Data Load successfully.");
            sceneChanger.ChangeScene("Dungeon");
        }
        else
        {
            sceneChanger.ChangeScene("Dungeon");
            Debug.Log("Failed to send data.");
        }


    }

    public IEnumerator onDelay(float _time){
        yield return new WaitForSeconds(_time);
        panelCharacter.SetActive(false);
        panelStatus.SetActive(true);
        StartCoroutine(dialogBox.TypeDialog("Please select your Class !!"));
        GenerateData(); 
    }

}
