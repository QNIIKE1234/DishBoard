using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using UnityEngine.Events;


public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }
    private void Awake()
     {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
		
		DontDestroyOnLoad(this);
    }
    public Transform content;
    public GameObject PopCam;

    public List<GameObject> PopUpPrefab;

    public void CreateTextDamage(string SetText,Transform _content)
    {

        GameObject Damage = Instantiate(PopUpPrefab[0], _content);
        TextDamage _notice = Damage.GetComponent<TextDamage>();
        _notice.SetDamageData(SetText);
		Destroy(Damage,0.5f);
        
    }

    public void OpenInventoryPanel(Transform _content){
        GameObject Invemtory = Instantiate(PopUpPrefab[1], _content);

    }

    public void OpenLoading(string _stage){
        GameObject loading = Instantiate(PopUpPrefab[2], content);
        LoadingController _Fade = loading.GetComponent<LoadingController>();
        _Fade.SetData(_stage);

    }
    public void OpenSkillDescription(string _skillID,int _skillDuraion,Transform _position = null,bool _isBattleScene = false){
        GameObject skillPanel = Instantiate(PopUpPrefab[3], content);
        SkillDescriptionPanel _Description = skillPanel.GetComponent<SkillDescriptionPanel>();
        _Description.SetData(_skillID,_skillDuraion);
        // Vector3 screenPosition = Input.mousePosition;
        if(_isBattleScene){
            skillPanel.transform.position = new Vector3(_position.position.x, _position.position.y+5, _position.position.z);
        }else{
            skillPanel.transform.position = new Vector3(_position.position.x+45, _position.position.y-4, _position.position.z);
        }


    }

    public void OnLoginAlert(string SetText,bool isCompleted)
    {

        GameObject Damage = Instantiate(PopUpPrefab[4], content);
        TextDamage _notice = Damage.GetComponent<TextDamage>();
        _notice.SetLoginAlertData(SetText,isCompleted);
		Destroy(Damage,3f);
        
    }


    public void RewardResult(UnityAction _function,int _EXP, int _coins)
    {
        GameObject Damage = Instantiate(PopUpPrefab[5], content);
        ResultPanel Object = Damage.GetComponent<ResultPanel>();
        Object.SetData(_function,_EXP,_coins);
    }
}
