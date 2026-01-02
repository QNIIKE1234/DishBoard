using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem {
    public string itemID;
    public string itemName;
    public string itemDescription;
    public ItemType itemType;

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
