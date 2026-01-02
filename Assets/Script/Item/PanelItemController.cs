using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelItemController : MonoBehaviour
{
    [SerializeField] ArmorController _armor;
    [SerializeField] WeaponController _Weapon;
    public GameObject playerObject;
    public Animator playerAnim;
    public GameObject itemPrefabs;
    public Transform contentItem;
    public Transform contentCharacter;
    public CharacterAnimationController player;


    public TextMeshProUGUI playerName;
    public TextMeshProUGUI _Class;
    public TextMeshProUGUI _Level;
    public TextMeshProUGUI _Health;
    public TextMeshProUGUI _Mana;
    public TextMeshProUGUI _ATK;
    public TextMeshProUGUI _MATK;
    public TextMeshProUGUI _DEF;
    public TextMeshProUGUI _MDEF;
    public TextMeshProUGUI _ACC;
    public TextMeshProUGUI _FLEE;
    public TextMeshProUGUI _CRIT;
    public TextMeshProUGUI _STR;
    public TextMeshProUGUI _VIT;
    public TextMeshProUGUI _AGI;
    public TextMeshProUGUI _DEX;
    public TextMeshProUGUI _INT;
    public TextMeshProUGUI _LCK;
    public TextMeshProUGUI _COIN;
    public TextMeshProUGUI _EXP;
    public Slider EXPbar;

	public List<Equipment> equipmentList = new List<Equipment>();

	[System.Serializable]
	public class Equipment
	{
    	public Image eq_Icon;
		public string eq_Type;
		public string eq_Name;
        public TextMeshProUGUI text_EQ_Name;


	}
    void Start()
    {
        OnCreateCharacter();
        SetData();
        onSetWeapon();
        onSetHelm();
        onSetArmor();
    }
    void SetData()
    {
        playerName.text = UserStatus.Instance.PName;
        _Class.text = UserStatus.Instance.CName;
        _Level.text = "LV : "+UserStatus.Instance.PLevel;
        _Health.text = "Health Point : "+" <color=#800000>"+UserStatus.Instance.HP+" / "+UserStatus.Instance.MAXHP;
        _Mana.text ="Mana Point : "+ " <color=#800000>"+UserStatus.Instance.MP+" / "+UserStatus.Instance.MAXMP;
        _ATK.text = "P.ATTACK : "+UserStatus.Instance.PHYSICALATTACK;
        _MATK.text = "M.ATTACK : "+UserStatus.Instance.MAGICALATTACK;
        _DEF.text = "P.DEFANSE : "+UserStatus.Instance.DEFENSE;
        _MDEF.text = "M.DEFANSE : "+UserStatus.Instance.MAGICDEFENSE;
        _ACC.text = "ACCURACY : "+UserStatus.Instance.ACCURACY;
        _FLEE.text = "FLEE : "+UserStatus.Instance.FLEE;
        _CRIT.text = "Critical : "+UserStatus.Instance.CRIT;
        _STR.text = "STR : "+UserStatus.Instance.STR;
        _VIT.text = "VIT : "+UserStatus.Instance.VIT;
        _AGI.text = "AGI : "+UserStatus.Instance.AGI;
        _DEX.text = "DEX : "+UserStatus.Instance.DEX;
        _INT.text = "INT : "+UserStatus.Instance.INT;
        _LCK.text = "LCK : "+UserStatus.Instance.LCK;
        _COIN.text = ""+UserStatus.Instance.COIN;
        _EXP.text = UserStatus.Instance.EXP+" / "+UserStatus.Instance.ExpLength[UserStatus.Instance.PLevel];
        EXPbar.maxValue = UserStatus.Instance.ExpLength[UserStatus.Instance.PLevel];
        EXPbar.value = UserStatus.Instance.EXP;
        SetItemData();
    }

    void SetItemData(){
        foreach(Item item in UserStatus.Instance.InventoryItem){
            GameObject _item = Instantiate(itemPrefabs, contentItem);
            ItemPrefabs _target = _item.GetComponent<ItemPrefabs>();
            _target.SetData(item);
        }

    }

    void onSetWeapon(){
        WeaponController.Weapon PlayerWeapon = UserStatus.Instance.Allweapon.weapon.Find(w => w.setID == UserStatus.Instance.WEAPON);
        if(PlayerWeapon!=null){
            equipmentList[4].eq_Icon.sprite = PlayerWeapon.WeaponIcon;
        }else{
            equipmentList[4].eq_Icon.sprite = Resources.Load<Sprite>("Empty");
        }
    }

    public void onSetHelm(){
        ArmorController.Helm PlayerHelm = _armor.helm.Find(a => a.setID == UserStatus.Instance.HELM);
        if(PlayerHelm!=null){
            equipmentList[1].eq_Icon.sprite = PlayerHelm.HelmIcon;
        }else{
            equipmentList[1].eq_Icon.sprite = Resources.Load<Sprite>("Empty");
        }
    }
    public void onSetArmor(){
        ArmorController.Armor PlayerArmor = UserStatus.Instance.AllArmor.armor.Find(a => a.setID == UserStatus.Instance.ARMOR);
        if(PlayerArmor!=null){
            equipmentList[3].eq_Icon.sprite = PlayerArmor.ArmorIcon;
        }else{
            equipmentList[3].eq_Icon.sprite = Resources.Load<Sprite>("Empty");
        }


    }
    void OnCreateCharacter()
    {
        playerAnim = playerObject.GetComponent<Animator>();
        player = playerObject.GetComponent<CharacterAnimationController>();
        playerObject.transform.localScale = new Vector3(0.55f, 0.55f, 1f);
        player.UpdateAnimClass();
        player.OnUpDateEquipment();
    }
    public void OnClosePanel(){
        Destroy(this.gameObject);
    }


}
