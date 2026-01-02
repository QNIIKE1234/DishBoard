using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MasterItemData
{
 public static List<Item> masterItemList =  new List<Item>(){
    new Item(){itemID = "WEAPON_001" , itemName = "Kanata" , itemType = Item.ItemType.EQUIPMENT , itemDescription = "Beginner Katana is normally stat. ATK+25,ACC+5, CRIT +5 , STR+5, AGI+2 , DEX + 5.",itemIconPath = "EquipmentIcon/Weapon-icon-001"},
    new Item(){itemID = "WEAPON_003" , itemName = "Saw Sword" , itemType = Item.ItemType.EQUIPMENT , itemDescription = "Weapon is very shape with high damage. ATK+40, ACC+5 , CRIT +10, STR +5 , VIT + 5 , AGI -5 , DEX + 3 , LCK + 3.",itemIconPath = "EquipmentIcon/Weapon-icon-003"},
    new Item(){itemID = "WEAPON_004" , itemName = "Holy Sword" , itemType = Item.ItemType.EQUIPMENT , itemDescription = "This sword is weapon of Crusader with holy damage. ATK+25 , MATK +25 , ACC + 10 , CRIT +10 , STR +3 , INT + 3 , DEX + 5 , LCK + 5.",itemIconPath = "EquipmentIcon/Weapon-icon-004"},
    new Item(){itemID = "WEAPON_005" , itemName = "Theif Dagger" , itemType = Item.ItemType.EQUIPMENT , itemDescription = "Beginner Dagger . ATK +25, ACC +10 ,CRIT +10 , AGI +15 , DEX +10 , LCK + 10.",itemIconPath = "EquipmentIcon/Weapon-icon-005"},
    new Item(){itemID = "WEAPON_006" , itemName = "Chop Chop" , itemType = Item.ItemType.EQUIPMENT , itemDescription = "Chop Chop is weapon of High Orc, Too much heavy but deal more damage. ATK+60 , ACC -5 ,STR +10, VIT+2 , AGI -5.",itemIconPath = "EquipmentIcon/Weapon-icon-006"},
    new Item(){itemID = "WEAPON_008" , itemName = "Red Opal Wand" , itemType = Item.ItemType.EQUIPMENT , itemDescription = "Beginner wand with normally magic damage. MATK +30 , ACC +10 , CRIT + 5 , INT + 5 , DEX + 5.",itemIconPath = "EquipmentIcon/Weapon-icon-008"},
    new Item(){itemID = "WEAPON_009" , itemName = "Eys of Sphinx" , itemType = Item.ItemType.EQUIPMENT , itemDescription = "This wand has knowladge of Sphinx and deal damage with curse magical. MATK +45 , ACC +5 ,CRIT +5 , VIT -5 , INT + 15, DEX +5.",itemIconPath = "EquipmentIcon/Weapon-icon-009"},




    new Item(){itemID = "ITM_USE_001" , itemName = "Health Potion (S)" , itemType = Item.ItemType.UseItem , itemDescription = "Heal your little HP.",itemIconPath = "UI/ITEM/ITEM_001"},
    new Item(){itemID = "ITM_USE_002" , itemName = "Mana Potion (S)" , itemType = Item.ItemType.UseItem , itemDescription = "Heal your little MP.",itemIconPath = "UI/ITEM/ITEM_002"},                 
 };
}
