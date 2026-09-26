using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkillDatabase : MonoBehaviour
{
    public List<ItemSkillType> itemSkillType = new List<ItemSkillType>();
    Item itemUse;
    int useSide;

    [System.Serializable]
    public class ItemSkillType{
        public string ID;
        public string type;
    }


    public void onUseItem(Item _item,int _side){
        itemUse = _item;
        useSide = _side;

        ItemSkillType itemType = itemSkillType.Find(i => i.ID == _item.itemID);

        if(itemType.type =="HPPotion"){

            onUseHPPotion();
        }
        else if (itemType.type =="MPPotion"){
            onUseMPPotion();
        }

    }

    public void onUseHPPotion(){
        if(itemUse.itemAmount>0 && itemUse.itemValue<=10){
            UserStatus.Instance.HP += (int)itemUse.itemValue*100;
        }else{
            UserStatus.Instance.HP += (int)itemUse.itemValue;
        }

        if(UserStatus.Instance.HP>UserStatus.Instance.MAXHP){
            UserStatus.Instance.HP = UserStatus.Instance.MAXHP;
        }
    }
    public void onUseMPPotion(){
        if(itemUse.itemAmount>0 && itemUse.itemValue<=10){
            UserStatus.Instance.MP += (int)itemUse.itemValue*100;
        }else{
            UserStatus.Instance.MP += (int)itemUse.itemValue;
        }

        if(UserStatus.Instance.MP>UserStatus.Instance.MAXMP){
            UserStatus.Instance.MP = UserStatus.Instance.MAXMP;
        }
    }
}
