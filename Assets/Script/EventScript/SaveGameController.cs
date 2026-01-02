using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameController : MonoBehaviour
{


    public void OnSave(){
        int classIndex = MatchClass();
        PlayerPrefs.SetString("PlayerName", PlayerData.Instance.PName);
        PlayerPrefs.SetString("PlayerClass", PlayerData.Instance.CName);
        PlayerPrefs.SetString("PlayerGender", PlayerData.Instance.Gender);
        PlayerPrefs.SetInt("PlayerPLevel", PlayerData.Instance.PLevel);
        PlayerPrefs.SetInt("PlayerMAXHP", PlayerData.Instance.MAXHP);
        PlayerPrefs.SetInt("PlayerHP", PlayerData.Instance.HP);
        PlayerPrefs.SetInt("PlayerMAXMP", PlayerData.Instance.MAXMP);
        PlayerPrefs.SetInt("PlayerMP", PlayerData.Instance.MP);
        PlayerPrefs.SetInt("PlayerPHYSICALATTACK", PlayerData.Instance.PHYSICALATTACK);
        PlayerPrefs.SetInt("PlayerDEFENSE", PlayerData.Instance.DEFENSE);
        PlayerPrefs.SetInt("PlayerMAGICALATTACK", PlayerData.Instance.MAGICALATTACK);
        PlayerPrefs.SetInt("PlayerMAGICDEFENSE", PlayerData.Instance.MAGICDEFENSE);
        PlayerPrefs.SetInt("PlayerACCURACY", PlayerData.Instance.ACCURACY);
        PlayerPrefs.SetInt("PlayerFLEE", PlayerData.Instance.FLEE);
        PlayerPrefs.SetInt("PlayerCRIT", PlayerData.Instance.CRIT);
        PlayerPrefs.SetInt("PlayerATTACKRANGE", PlayerData.Instance.ATTACKRANGE);
        PlayerPrefs.SetInt("PlayerSTR", PlayerData.Instance.STR);
        PlayerPrefs.SetInt("PlayerVIT", PlayerData.Instance.VIT);
        PlayerPrefs.SetInt("PlayerAGI", PlayerData.Instance.AGI);
        PlayerPrefs.SetInt("PlayerDEX", PlayerData.Instance.DEX);
        PlayerPrefs.SetInt("PlayerINT", PlayerData.Instance.INT);
        PlayerPrefs.SetInt("PlayerLCK", PlayerData.Instance.LCK);

        PlayerPrefs.SetInt("PlayerCOIN", PlayerData.Instance.COIN);
        PlayerPrefs.SetInt("PlayerEXP", PlayerData.Instance.EXP);

        PlayerPrefs.SetString("PlayerWEAPON", PlayerData.Instance.WEAPON);
        PlayerPrefs.SetString("PlayerARMOR", PlayerData.Instance.ARMOR);

        Debug.Log("Game Saved !!");

    }

    int MatchClass(){
        if(PlayerData.Instance.CName =="WarriorClass"){
            return 0;
        }else if(PlayerData.Instance.CName =="SorceressClass"){
            return 1;
        }
        return -1;
    }
}
