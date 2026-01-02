using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] ArmorController _armor;
    [SerializeField] WeaponController _Weapon;
    public Animator sceneAnimator;
    string ClassAnim;
    public Image Weapon;
    public Image OffHand;
    public Image Head;
    public Image Face;
    public Image Eyes;
    public Image Helm;
    public Image Cape;

    public Image Top;
    public Image Shoulder_L;
    public Image Shoulder_R;
    public Image Arm_L;
    public Image Arm_R;
    public Image Hand_L;
    public Image Hand_R;


    public Image Bottom_L;
    public Image Bottom_R;
    public Image Boot_L;
    public Image Boot_R;

    void Start()
    {
        UpdateAnimClass();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAnimClass(){
        ClassAnim = "WarriorClass";
        sceneAnimator.SetBool(ClassAnim,true);

    }

    public void OnUpDateEquipment(){
        onSetWeapon();
        onSetArmor();
        onSetOffHand();
        onSetHelm();
        onSetCape();
    }

    public void onSetWeapon(){
        WeaponController.Weapon PlayerWeapon =UserStatus.Instance.Allweapon.weapon.Find(w => w.setID ==UserStatus.Instance.WEAPON);
        if(PlayerWeapon!=null){
            Weapon.sprite = PlayerWeapon.WeaponImg;
        }else{
            Helm.sprite = Resources.Load<Sprite>("Empty");
        }

    }
    public void onSetArmor(){
        ArmorController.Armor PlayerArmor =UserStatus.Instance.AllArmor.armor.Find(a => a.setID ==UserStatus.Instance.ARMOR);
        if(PlayerArmor!=null){
            Top.sprite = PlayerArmor.Top;
            Shoulder_L.sprite = PlayerArmor.Shoulder_L;
            Shoulder_R.sprite = PlayerArmor.Shoulder_R;

            Arm_L.sprite = PlayerArmor.Arm_L;
            Arm_R.sprite = PlayerArmor.Arm_R;

            Hand_L.sprite = PlayerArmor.Hand_L;
            Hand_R.sprite = PlayerArmor.Hand_R;

            Bottom_L.sprite = PlayerArmor.Bottom_L;
            Bottom_R.sprite = PlayerArmor.Bottom_R;

            Boot_L.sprite = PlayerArmor.Boot_L;
            Boot_R.sprite = PlayerArmor.Boot_R;
        }else{

            Top.sprite = Resources.Load<Sprite>("Empty");
            Shoulder_L.sprite = Resources.Load<Sprite>("Empty");
            Shoulder_R.sprite = Resources.Load<Sprite>("Empty");

            Arm_L.sprite = Resources.Load<Sprite>("Empty");
            Arm_R.sprite = Resources.Load<Sprite>("Empty");

            Hand_L.sprite = Resources.Load<Sprite>("Empty");
            Hand_R.sprite = Resources.Load<Sprite>("Empty");

            Bottom_L.sprite = Resources.Load<Sprite>("Empty");
            Bottom_R.sprite = Resources.Load<Sprite>("Empty");

            Boot_L.sprite = Resources.Load<Sprite>("Empty");
            Boot_R.sprite = Resources.Load<Sprite>("Empty");

        }


    }
    public void onSetAccessory(){

    }

    public void onSetOffHand(){
        WeaponController.OffHand PlayerOffHand =UserStatus.Instance.Allweapon.offHand.Find(o => o.setID ==UserStatus.Instance.OFFHAND);
        if(PlayerOffHand!=null){
            OffHand.sprite = PlayerOffHand.ImageIcon;
        }else{
            Helm.sprite = Resources.Load<Sprite>("Empty");
        }

    }
    public void onSetCape(){
        ArmorController.Cape PlayerCape = _armor.cape.Find(a => a.setID ==UserStatus.Instance.CAPE);
        if(PlayerCape!=null){
            Cape.sprite = PlayerCape.ImageIcon;
        }else{
            Helm.sprite = Resources.Load<Sprite>("Empty");
        }
    }
    public void onSetHelm(){
        ArmorController.Helm PlayerHelm = _armor.helm.Find(a => a.setID ==UserStatus.Instance.HELM);
        if(PlayerHelm!=null){
            Helm.sprite = PlayerHelm.ImageIcon;
        }else{
            Helm.sprite = Resources.Load<Sprite>("Empty");
        }
    }
}
