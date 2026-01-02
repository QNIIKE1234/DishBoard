using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemPrefabs : MonoBehaviour
{
    [SerializeField] ItemSkillDatabase skillUser;
    public Image icon;
    public TextMeshProUGUI amount;
    Item item;
    public void SetData(Item _item)
    {
        item =_item;
        icon.sprite = _item.icon;
        amount.text = "x"+_item.itemAmount;
    }

    public void onUse(){
        foreach(Item target in PlayerData.Instance.InventoryItem){
            if(target.itemID == item.itemID){
                target.itemAmount--;
            }
        }
        skillUser.onUseItem(item,1);
    }
}
