using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillDescription : MonoBehaviour
{
    public Color debuffColor;
    public Button buttonIcon;
    public Image imageIcon;
    public string buffName;
    public int skillDuaration;
    public TextMeshProUGUI text_Duration;


    public void SetData(bool _isBuff,string _skillName,int _skillDuration=0)
    {
        skillDuaration = _skillDuration;

        if(_isBuff){
            Skill skill = MasterISkillData.masterSkillList.Find(s => s.skillName == _skillName);
            imageIcon.sprite = Resources.Load<Sprite>(skill.skillIconPath);
            buffName = skill.skillCode;
        }
        else{
            BuffAndDebuff debuff = MasterISkillData.masterBuffList.Find(b => b.BuffName == _skillName);
            imageIcon.sprite = Resources.Load<Sprite>(debuff.BuffIconPath);
            buffName = _skillName;
        }

        if(text_Duration!=null){
            text_Duration.text =""+_skillDuration;
        }


    }

    public void OnClick(bool isBattleScene){
        if(isBattleScene){
            PopupManager.Instance.OpenSkillDescription(buffName,skillDuaration,this.transform,isBattleScene);
        }else{
            PopupManager.Instance.OpenSkillDescription(buffName,skillDuaration,this.transform,isBattleScene);
        }


    }

    public void OnDestroy(){
        GameObject obj = GameObject.Find("SkillDescriptionPrefabs(Clone)");
        if (obj != null)
        {
            Destroy(obj);
        }
        // Destroy(this.gameObject,0.25f);

    }
}
