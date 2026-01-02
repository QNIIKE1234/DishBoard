using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item {
    public string itemID ;
    public string itemName;
    public Sprite icon;
    public string itemDescription;
    public string itemIconPath;
    public ItemType itemType;
    public int itemAmount;

    public int itemPAttack;
    public int itemMAttack;
    public int itemPDef;
    public int itemMDef;
    public int itemMaxHP;
    public int itemMaxMP;
    public int itemAccuracy;
    public int itemFlee;
    public float itemValue;

    

    public enum ItemType{
        EQUIPMENT,
        UseItem,
        ETC,
        
    }

}
