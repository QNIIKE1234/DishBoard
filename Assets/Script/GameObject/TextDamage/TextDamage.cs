using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextDamage : MonoBehaviour
{
    
    public TextMeshProUGUI text_damage;
    public List<Color> textColor = new List<Color>();
    public void SetDamageData(string _damage)
    {
        text_damage.text = _damage;
    }
    public void SetLoginAlertData(string _Text,bool _isCompleted)
    {
        text_damage.text = _Text;
        if(_isCompleted){
            text_damage.color = textColor[2];
        }else{
            text_damage.color = textColor[1];
        }
    }

}
