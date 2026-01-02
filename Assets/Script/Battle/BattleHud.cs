using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text_name;
    [SerializeField] TextMeshProUGUI Text_LEVEL;
    [SerializeField] HpBar hpBar;

    public float MaxHP;
    public float HP ;

    public float MaxMP;
    public float MP;
    // Character _character;
    public bool isPlayer = false;
    public IEnumerator SetData(float _MAXHP,float _HP,float _MAXMP,float _MP,string _Name,int _Level,bool _isPlayer)
    {

        MaxHP = _MAXMP;
        HP = _HP;
        MaxMP = _MAXMP;
        MP = _MP;
        Text_name.text = _Name;
        Text_LEVEL.text = " " + _Level;
        isPlayer =_isPlayer;

        if(isPlayer){
            hpBar.SetHP(UserStatus.Instance.HP,true);
            hpBar.SetMP(UserStatus.Instance.MP,true);
        }
        else{

            hpBar.SetHP(HP);
            hpBar.SetMP(MP);
        }
        yield return new WaitForSeconds(0.5f);

    }

    public IEnumerator UpDateHP(float _MAXHP,float _HP,float _MAXMP,float _MP,bool _isPlayer){
        isPlayer =_isPlayer;
        MaxHP = _MAXHP;
        HP = _HP;
        MaxMP = _MAXMP;
        MP = _MP;
        if(isPlayer){
            yield return hpBar.SetHPSmooth(_HP);
            yield return hpBar.SetMPSmooth(_MP);
        }
        else{
            yield return hpBar.SetHPSmooth(_HP);
            yield return hpBar.SetMPSmooth(_MP);
        }

      
    }

    public IEnumerator UpDateEXP(){
        yield return hpBar.SetEXPSmooth(UserStatus.Instance.EXP);
        Text_LEVEL.text =""+UserStatus.Instance.PLevel;
   
    }
    
}
