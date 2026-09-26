using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameController : MonoBehaviour
{


    public void OnSave(){
        int classIndex = MatchClass();
        PlayerPrefs.SetString("PlayerName", UserStatus.Instance.PName);
        PlayerPrefs.SetString("PlayerPName", UserStatus.Instance.PName);
        PlayerPrefs.SetString("PlayerClass", UserStatus.Instance.CName);
        PlayerPrefs.SetString("PlayerGender", UserStatus.Instance.Gender);
        PlayerPrefs.SetInt("PlayerPLevel", UserStatus.Instance.PLevel);
        PlayerPrefs.SetInt("PlayerMAXHP", UserStatus.Instance.MAXHP);
        PlayerPrefs.SetInt("PlayerHP", UserStatus.Instance.HP);
        PlayerPrefs.SetInt("PlayerMAXMP", UserStatus.Instance.MAXMP);
        PlayerPrefs.SetInt("PlayerMP", UserStatus.Instance.MP);
        PlayerPrefs.SetInt("PlayerPHYSICALATTACK", UserStatus.Instance.PHYSICALATTACK);
        PlayerPrefs.SetInt("PlayerDEFENSE", UserStatus.Instance.DEFENSE);
        PlayerPrefs.SetInt("PlayerMAGICALATTACK", UserStatus.Instance.MAGICALATTACK);
        PlayerPrefs.SetInt("PlayerMAGICDEFENSE", UserStatus.Instance.MAGICDEFENSE);
        PlayerPrefs.SetInt("PlayerACCURACY", UserStatus.Instance.ACCURACY);
        PlayerPrefs.SetInt("PlayerFLEE", UserStatus.Instance.FLEE);
        PlayerPrefs.SetInt("PlayerCRIT", UserStatus.Instance.CRIT);
        PlayerPrefs.SetInt("PlayerATTACKRANGE", UserStatus.Instance.ATTACKRANGE);
        PlayerPrefs.SetInt("PlayerSTR", UserStatus.Instance.STR);
        PlayerPrefs.SetInt("PlayerVIT", UserStatus.Instance.VIT);
        PlayerPrefs.SetInt("PlayerAGI", UserStatus.Instance.AGI);
        PlayerPrefs.SetInt("PlayerDEX", UserStatus.Instance.DEX);
        PlayerPrefs.SetInt("PlayerINT", UserStatus.Instance.INT);
        PlayerPrefs.SetInt("PlayerLCK", UserStatus.Instance.LCK);

        PlayerPrefs.SetInt("PlayerCOIN", UserStatus.Instance.COIN);
        PlayerPrefs.SetInt("PlayerEXP", UserStatus.Instance.EXP);

        PlayerPrefs.SetString("PlayerWEAPON", UserStatus.Instance.WEAPON);
        PlayerPrefs.SetString("PlayerARMOR", UserStatus.Instance.ARMOR);
        PlayerPrefs.Save();

        Debug.Log("Game Saved !!");

    }

    int MatchClass(){
        if(UserStatus.Instance.CName =="WarriorClass"){
            return 0;
        }else if(UserStatus.Instance.CName =="SorceressClass"){
            return 1;
        }
        return -1;
    }
}
