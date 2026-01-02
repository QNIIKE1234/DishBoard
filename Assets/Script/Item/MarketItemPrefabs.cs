using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MarketItemPrefabs : MonoBehaviour
{
    public Image item_Icon;
    public TextMeshProUGUI item_name;
    public TextMeshProUGUI item_Price;
    public TextMeshProUGUI item_Description;
    public void SetData(string _item_ID,string _item_Price)
    {
        Debug.Log("ITEM : "+_item_ID);
        Item item = MasterItemData.masterItemList.Find(i => i.itemID == _item_ID);

        if(item != null){

            item_Icon.sprite = Resources.Load<Sprite>(item.itemIconPath);
            item_name.text = item.itemName;
            item_Price.text = _item_Price;
            item_Description.text = item.itemDescription;
        }

    }
}
