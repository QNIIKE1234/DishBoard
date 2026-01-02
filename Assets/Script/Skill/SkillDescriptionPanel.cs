using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillDescriptionPanel : MonoBehaviour
{
    public Image imageIcon;
    public TextMeshProUGUI text_Name;
    public TextMeshProUGUI text_Duration;
    public TextMeshProUGUI text_Description;

    public void SetData(string _skillID, int _skillDuration){
        Skill skill = MasterISkillData.masterSkillList.Find(s => s.skillCode == _skillID);

        if(skill!=null){
            imageIcon.sprite = Resources.Load<Sprite>(skill.skillIconPath);
            text_Name.text = skill.skillName;
            text_Description.text = skill.skillDescription;
        }else{
            BuffAndDebuff buff = MasterISkillData.masterBuffList.Find(b => b.BuffName == _skillID);

            imageIcon.sprite = Resources.Load<Sprite>(buff.BuffIconPath);
            text_Name.text = buff.BuffName;
            text_Description.text = buff.BuffDescription;
        }

        text_Duration.text = _skillDuration+" Turn remaining";

        if(_skillDuration<=0){
            text_Duration.gameObject.SetActive(false);
        }
    }
}
